using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using CoreBank.Common.AddIn.Forms;

namespace CoreBank
{
    public class AddInUploadTestCases : UploadProcessTemplate
    {
        public AddInUploadTestCases(TEMPLATES template)
            : base(template)
        {
            this.Action = ADDIN_ACTION.PROCESS_UPLOADTESTCASE;
            this.Progress = new ProgressPercentage("Prepare upload", 10);
            this.Title = "Upload test cases";
            this.Progress.Message = "Prepare uploading test cases";
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
                        if (ReadRepository())
                        {
                            blnResult = true;
                        }
                    }
                }
            }
            else
            {
                Framework.Log.AddError("Current workbook is not an process template.", "", "");
            }

            return blnResult;
        }

        protected override bool Execute()
        {
            bool blnResult = true;

            this.Progress = new ProgressPercentage("Upload", Framework.Process.TestCases.Count + 1);
            this.Title = "Uploading " + Framework.Process.TestCases.Count + " test cases";
            this.Progress.Message = "Uploading test cases";
            this.Progress.Continue();
            
            foreach (ExcelTest test in Framework.Process.TestCases)
            {
                if (Framework.PutTestCase(test))
                {
                    Framework.TestCaseIndex++;
                }
                else
                {
                    blnResult = false;
                    break;
                }
                this.Progress.Message = "Uploading test cases " + this.Progress.Index.ToString() + " / " + this.Progress.iTotal.ToString();
                this.Progress.Continue();
            }
           
            if (blnResult)
            {
                Framework.Log.AddCorrect("Uploaded " + Framework.Process.TestCases.Count + " test cases.");
            }

            return blnResult;
        }

        protected override bool Finish()
        {
            bool blnResult = false;

            this.Progress.Message = "Uploading process " + Framework.Process.Name + " to resource.";

            if (UploadProcessToResource())
            {
                this.Progress.Continue();

                if (Framework.Process.CloseWorkbook())
                {
                    blnResult = true;
                }
            }

            return blnResult;
        }

    }
}
