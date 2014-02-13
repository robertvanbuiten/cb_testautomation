using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
using System.IO;
using System.Data;
using Excel=Microsoft.Office.Interop.Excel;
using CoreBank.Common.AddIn.Forms;

namespace CoreBank
{
    public partial class Framework
    {
        // Logging
        public static Log Log;
        public static AddInAction AddInAction;
        public static Status Status;

        // Repository objects
        public static REPOSITORY_TYPE _type = REPOSITORY_TYPE.UNKNOWN;
        public static QC _qc;
        public static Network _nw;

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
        public static TEMPLATES Template = TEMPLATES.UNKNOWN;
        public static ProcessWorkbook Process;
        public static ChainWorkbook Chain;

        /// <summary>
        /// Active Process 
        /// Active Application
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
        /// Create a new log
        /// </summary>

        public static void NewAction()
        {
            Framework.Log = new Log();
        }

        /// <summary>
        /// 
        /// </summary>

        public static bool Start()
        {
            bool blnResult = false;
            Framework.Config = new Config();

            try
            {
                Framework.Connection = new ConnectionSettings();
                Framework.Paths = new Paths();
            }
            catch (Exception ex)
            {
                Framework.Log.AddError("Cannot read connection / paths. Check xml.", ex.Message, ex.StackTrace);
                return blnResult;
            }

            if (Framework.Init())
            {
                blnResult = true;
                Framework.Log.AddCorrect("Connection / Paths set.");
            }
            else
            {
                Framework.Log.AddIssue("Unknown repository found: " + Framework.Connection.Repository.ToString());
            }

            Framework.Ready = blnResult;
            return blnResult;
       }

        private static bool Init()
        {
            bool blnResult = false;
            Framework.Ready = false;

            Framework._qc = null;
            Framework._nw = null;
            Framework._type = REPOSITORY_TYPE.UNKNOWN;
            Framework.Connected = false;

                if (Framework.Connection.Repository == "ALM")
                {
                    Framework._qc = new QC(Framework.Connection);
                    Framework._type = REPOSITORY_TYPE.ALM;
                    blnResult = true;
                }
                else if (Framework.Connection.Repository == "NETWORK")
                {
                    Framework._nw = new Network(Framework.Connection);
                    Framework._type = REPOSITORY_TYPE.NETWORK;
                    blnResult = true;
                }

                
                return blnResult;
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
