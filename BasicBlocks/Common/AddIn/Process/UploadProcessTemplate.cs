using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.ComponentModel;

namespace CoreBank
{
    public class UploadProcessTemplate: AddInAction
    {
       
        public UploadProcessTemplate(TEMPLATES template):base(template)
        {
            
        }
               
        /// <summary>
        /// Low-level actions
        /// </summary>
        /// <returns></returns>

        protected bool PrepareProcess()
        {
            bool blnResult = false;

            // 1. Copy process to temp
            // 2. Read / Check Proces
            this.Progress.Continue();

            if (Framework.Process.CopyToTemp())
            {
                blnResult = true;
            }
            else
            {
                Framework.Log.AddError("Cannot copy workbook to process file.", "", "");
            }

            return blnResult;
        }

        protected bool ReadProcess()
        {
            bool blnResult = false;

            // 1. Check process in config 
            // 2. Check Application
            // 3. Read test cases
            // 4. Read flows.
            this.Progress.Continue();

            if (Framework.Process.Check())
            {
                this.Progress.Continue();

                if (Framework.Process.Read())
                {
                    blnResult = true;
                }
            }
            else
            {

            }

            return blnResult;
        }

        protected bool ReadRepository()
        {

            bool blnResult = false;

            if (Framework._type == REPOSITORY_TYPE.ALM)
            {
                blnResult = Framework._qc.PrepareUpload();
            }
            else if (Framework._type == REPOSITORY_TYPE.NETWORK)
            {
                blnResult = false;
            }

            return blnResult;

        }

        protected bool UploadProcessToResource()
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

            return blnResult;
        }

    }
}
