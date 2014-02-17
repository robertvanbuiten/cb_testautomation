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
           this.Progress.Message = "Uploading";
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

        protected override bool Execute()
        {
            bool blnResult = false;
            this.Progress.Continue();
            
            // comment

            blnResult = UploadProcessToResource();

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
