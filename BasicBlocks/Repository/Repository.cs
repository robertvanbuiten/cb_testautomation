using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CoreBank
{
    public class ConnectionSettings
    {
        public string Address;
        public string User;
        public string Password;
        public string Port;
        public string Database;
        public string Project;
        public string Domain;
        public string Repository;
        public string ConfigFile;
        
        public ConnectionSettings()
        {
            Init();
        }

        public void Init()
        {
            Address = Global.Default._Address;
            User = Global.Default._User;
            Password = Global.Default._Password;
            Port = Global.Default._Database;
            Project = Global.Default._Project;
            Repository = Global.Default._Repository;
            Domain = Global.Default._Domain;
            Database = Global.Default._Database;
        }

    }
    
    
    
    public class Repository
    {
        protected ConnectionSettings Settings;
        public string Message;
        
        public Repository(ConnectionSettings settings)
        {
            Settings = settings;
        }

        public virtual bool Connect()
        {
            bool blnResult = false;

            return blnResult;
        }

        public virtual bool GetResource(string source, string name)
        {
            bool blnResult = false;
            
            return blnResult;
        }

        public virtual bool SaveResource(string source, string destination)
        {
            bool blnResult = false;

            return blnResult;
        }

        public virtual bool GetProcess(string source, string name)
        {
            bool blnResult = false;

            return blnResult;
        }

        public virtual bool SaveProcess(string source, string destination)
        {
            bool blnResult = false;

            return blnResult;
        }


        public virtual bool GetTest(string name)
        {
            bool blnResult = false;

            return blnResult;
        }

        public virtual bool SaveTest(ExcelTest source)
        {
            bool blnResult = false;

            return blnResult;

        }

        public virtual bool CreateSet(string name)
        {
            bool blnResult = false;

            return blnResult;
        }

        public virtual bool ClearSet(string name)
        {
            bool blnResult = false;

            return false;

        }

       

    }
}
