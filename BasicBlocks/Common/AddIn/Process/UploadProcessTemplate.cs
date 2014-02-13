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

        protected override bool Prepare()
        {
            bool blnResult = false;

            if (Template == TEMPLATES.PROCESS)
            {
                if (ReadProcess())
                {
                    if (PrepareProcess())
                    {
                        blnResult = true;
                    }
                }
            }

            return blnResult;
        }

        /// <summary>
        /// Low-level actions
        /// </summary>
        /// <returns></returns>

        private bool PrepareProcess()
        {
            bool blnResult = false;

            // 1. Copy process to temp
            // 2. Read / Check Proces
            this.Progress.Continue();

            if (Framework.Process.CopyToTemp())
            {
                blnResult = true;
            }

            return blnResult;
        }

        private bool ReadProcess()
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

            return blnResult;
        }
    }
}
