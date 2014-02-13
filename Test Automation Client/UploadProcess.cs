using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Excel;
using System.ComponentModel;
using System.Windows.Forms;

namespace CoreBank.Client
{
    public partial class ThisAddIn
    {
        protected ExcelWorkbook CurrentWorkbook;


        /// <summary>
        /// Activate upload process functionality
        /// </summary>
        /// <param name="Ctrl"></param>
        /// <param name="CancelDefault"></param>
       
        void btnUploadProcess_Click(Office.CommandBarButton Ctrl, ref bool CancelDefault)
        {
            if (UploadProcess())
            {
                System.Windows.Forms.MessageBox.Show("Finished uploading current workbook to test resource " + Framework.ActiveProcess.Name);
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Could not upload current workbook.");
            }
        }

        public bool UploadProcess()
        {
            bool blnResult = false;

            CurrentWorkbook = new ExcelWorkbook();

            if (CurrentWorkbook.Valid)
            {  
                if (Framework.UploadTemplate())
                {
                    blnResult = true;
                }
            }

            return blnResult;
        }



    }
}
