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
            this.Progress = new ProgressPercentage("Prepare upload", 4);
            this.Title = "Upload test cases";
        }

        protected override bool Execute()
        {
            bool blnResult = true;

            this.Progress = new ProgressPercentage("Upload", Framework.Process.TestCases.Count);

            foreach (ExcelTest test in Framework.Process.TestCases)
            {
                if (Framework.PutTestCase(test))
                {
                    Framework.TestCaseIndex++;
                }
                else
                {
                    break;
                }

                this.Progress.Continue();
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
