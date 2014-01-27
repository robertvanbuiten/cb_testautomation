using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
using System.IO;
using System.Data;
using Excel=Microsoft.Office.Interop.Excel;

namespace CoreBank
{
    public class Framework
    {
        // Logging
        public static Log Log;

        // Repository objects
        private static REPOSITORY_TYPE _type = REPOSITORY_TYPE.UNKNOWN;
        private static QC _qc;
        private static Network _nw;

        //
        public static bool Connected;
        public static bool Ready;

        // Object representation of config.xml
        public static Config Config;

        // Environment Settings and Paths
        public static Paths Paths;
        public static ConnectionSettings Connection;

        // Test representation 
        public static ExcelTest Test;

        // Set current work as process workbook if tabs are found
        public static ProcessWorkbook Process;

        /// <summary>
        /// 
        /// </summaryActive Process from config
        public static Process ActiveProcess;
        public static Application ActiveApplication;
        public static int TestCaseIndex;

        /// <summary>
        /// 
        /// </summary>
        private static string xmlstring;
        private static XmlDocument doc;

        public static int Percentage;

        private static Framework singleton;

        private Framework()
        {

        }

        public static Framework Factory()
        {
            if (Framework.singleton == null)
            {
                Framework.singleton = new Framework();
            }

            return Framework.singleton;
        }

        /// <summary>
        /// 
        /// </summary>

        public static void Start(ConnectionSettings conn, Paths paths)
        {
            Framework.Config = new Config();
            Framework.Log = new Log();

            try
            {
                Framework.Connection = conn;
                Framework.Paths = paths;
                Framework.Init();
            }
            catch { }

           
       }

        private static void Init()
        {
            Framework.Ready = true;

            Framework._qc = null;
            Framework._nw = null;
            Framework._type = REPOSITORY_TYPE.UNKNOWN;
            Framework.Connected = false;

            if (Framework.Connection.Repository == "ALM")
            {
                Framework._qc = new QC(Framework.Connection);
                Framework._type = REPOSITORY_TYPE.ALM;
            }
            else if (Framework.Connection.Repository == "NETWORK")
            {
                Framework._nw = new Network(Framework.Connection);
                Framework._type = REPOSITORY_TYPE.NETWORK;
            }
            else
            {
                Framework.Ready = false;
            }
        }

        /// <summary>
        /// Start Framework.
        /// Create connection and get resource from ALM
        /// Read config in memory
        /// </summary>

        public static void Connect()
        {
            Framework.Connected = true;
            
            if (Framework._type == REPOSITORY_TYPE.ALM)
            {
                Framework.Connected = Framework._qc.Connect();
            }
            else if (Framework._type == REPOSITORY_TYPE.NETWORK)
            {
                Framework.Connected = Framework._nw.Connect();
            }
            else
            {
                Framework.Connected = false;
            }
        }

        public static void Stop()
        {
            
        }

        /// <summary>
        /// Reset reposoitory. 
        /// </summary>
        /// <returns></returns>
        public static bool ReadRepository()
        {
            bool blnResult = false;

            if (Framework.GetConfig())
            {
                if (Framework.ReadConfig())
                {
                    blnResult = true;
                }
            }

            return blnResult;
        }

        public static bool UploadRepository()
        {
            bool blnResult = false;

            if (Framework.WriteConfig())
            {
                if (Framework.PutConfig())
                {
                    blnResult = true;
                }
            }

            return blnResult;
        }

        public static bool ResetRepository()
        {
            bool blnResult = false;

            if (Framework.GetConfig())
            {
                if (Framework.WriteConfig())
                {
                    if (Framework.PutConfig())
                    {
                        blnResult = true;
                    }
                }
            }

            return blnResult;
        }


        public static bool UploadTemplate()
        {
            bool blnResult = false;

            // 1. Prepare process
            // 2. Put process
            // 3. optional (clean process)

            if (ReadProcess())
            {
                if (PrepareProcess())
                {
                    if (PutProcess())
                    {
                        if (CleanProcess())
                        {
                            blnResult = true;
                        }
                    }

                }
            }

            return blnResult;
        }

        public static bool UploadTestCase(ExcelTest test)
        {
            bool blnResult = false;

            if (PutTestCase(test))
            {
                blnResult = true;
            }

            return blnResult;
        }

        ///=================================================================================================================
        /// Config files
        ///=================================================================================================================

        /// <summary>
        /// Read config file / Deserialize config.xml
        /// </summary>
        /// <returns></returns>

        public static bool GetConfig()
        {
            bool blnResult = false;

            if (Framework._type == REPOSITORY_TYPE.ALM)
            {
                blnResult = Framework._qc.GetConfig();
            }
            else if (Framework._type == REPOSITORY_TYPE.NETWORK)
            {
                blnResult = Framework._nw.GetConfig();
            }
            else
            {
                // log
            }

            return blnResult;
        }

        protected internal static bool PutConfig()
        {
            bool blnResult = false;

            if (Framework._type == REPOSITORY_TYPE.ALM)
            {
                blnResult = Framework._qc.SaveConfig();
            }
            else if (Framework._type == REPOSITORY_TYPE.NETWORK)
            {
                blnResult = Framework._nw.SaveConfig();
            }
            else
            {
                // log
            }

            return blnResult;

        }

        private static bool ReadConfig()
        {
            bool blnResult = false;

            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(Config));
                FileStream reader = new FileStream(Framework.Paths.ConfigPath, FileMode.Open);
                Framework.Config = (Config)xs.Deserialize(reader);
                Framework.Config.Init();
                reader.Close();
                blnResult = true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("error reading config" + ex.Message + "\n" + ex.InnerException + "\n" + ex.StackTrace + "\n" + ex.Source + "\n");
            }

            return blnResult;

        }

        /// <summary>
        /// write config object to config.xml
        /// </summary>
        /// <returns></returns>

        private static bool WriteConfig()
        {
            bool blnResult = false;

            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(Config));
                XmlTextWriter writer = new XmlTextWriter(Framework.Paths.ConfigPath, Encoding.UTF8);
                xs.Serialize(writer, Framework.Config);
                writer.Close();
                blnResult = true;
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Cannot deserialize " + ConfigPath + ".");
            }

            return blnResult;
        }

        ///=================================================================================================================
        /// Template files
        ///=================================================================================================================

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// 
        /// <summary>
        /// Upload process file to ALM (TestPlan)
        /// </summary>
        /// <returns></returns>

        private static bool PrepareProcess()
        {
            bool blnResult = false;

            // 1. Copy process to temp
            // 2. Read / Check Proces

            if (Framework.Process.CopyToTemp())
            {
                blnResult = true;
            }

            return blnResult;
        }

        private static bool CleanProcess()
        {
            bool blnResult = false;

            if (Framework.Process.CloseWorkbook())
            {
                blnResult = true;
            }

            return blnResult;
        }

        public static bool ReadProcess()
        {
            bool blnResult = false;

            if (Framework.Process.Check())
            {
                if (Framework.Process.Read())
                {
                    blnResult = true;
                }
            }

            return blnResult;
        }

        public static bool PutProcess()
        {
            bool blnResult = false;

            if (Framework._type == REPOSITORY_TYPE.ALM)
            {
                blnResult = Framework._qc.SaveProcess();
            }
            else if (Framework._type == REPOSITORY_TYPE.NETWORK)
            {
                blnResult = Framework._nw.SaveProcess();
            }
            else
            {
                // log
            }

            return blnResult;
        }

        ///=================================================================================================================
        /// Test cases
        ///=================================================================================================================

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// 

        public static bool PutTestCase(ExcelTest test)
        {
            bool blnResult = false;

            if (Framework._type == REPOSITORY_TYPE.ALM)
            {
                blnResult = Framework._qc.SaveTest(test);
            }
            else if (Framework._type == REPOSITORY_TYPE.NETWORK)
            {
                blnResult = Framework._nw.SaveTest(test);
            }
            else
            {
                // log
            }


            return blnResult;
        }

    }
}
