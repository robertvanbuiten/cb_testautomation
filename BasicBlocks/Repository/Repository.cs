using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CoreBank
{

    
    
    
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

        public virtual bool GetConfig()
        {
            bool blnResult = false;
            
            return blnResult;
        }

        public virtual bool SaveConfig()
        {
            bool blnResult = false;

            return blnResult;
        }

        public virtual bool GetProcess()
        {
            bool blnResult = false;
            
            return blnResult;
        }

        public virtual bool SaveProcess()
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

        public virtual bool PrepareUploadTest()
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
