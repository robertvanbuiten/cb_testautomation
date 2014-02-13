using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreBank
{
    public class AddInUploadProcess: UploadProcessTemplate
    {
        // comment
        
        public AddInUploadProcess(TEMPLATES template):base(template)
        {
           this.Action = ADDIN_ACTION.PROCESS_UPLOADTEMPLATE;
           this.Progress = new ProgressPercentage("Uploading Template", 4);
           this.Title = "Upload process template";
        }

        protected override bool Execute()
        {
            bool blnResult = false;
            this.Progress.Continue();
            
            // comment
            
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

        protected override bool Finish()
        {
            bool blnResult = false;

            if (Framework.Process.CloseWorkbook())
            {
                blnResult = true;
            }

            return blnResult;
        }

      
    }
}
