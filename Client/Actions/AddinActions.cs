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

using CoreBank.Common.AddIn.Forms;

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
            CurrentWorkbook = new ExcelWorkbook();
            
            Framework.AddInAction = new AddInUploadProcess(CurrentWorkbook.Template);
            AddInUploadProcess upload = Framework.AddInAction as AddInUploadProcess;
            Framework.Status.Start();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Ctrl"></param>
        /// <param name="CancelDefault"></param>

        void btnConnect_Click(Office.CommandBarButton Ctrl, ref bool CancelDefault)
        {
            if (btnConnect.Caption == "Connect")
            {
                Framework.AddInAction = new AddInConnect();
                AddInConnect connect = Framework.AddInAction as AddInConnect;
                Framework.Status.Start();
            }
            else
            {
                InitiateFramework();
            }

            // Refresh menu.
            RemoveMenu();
            AddMenu();
        }

        void btnUpload_Click(Office.CommandBarButton Ctrl, ref bool CancelDefault)
        {
            CurrentWorkbook = new ExcelWorkbook();

            Framework.AddInAction = new AddInUploadTestCases(CurrentWorkbook.Template);
            AddInUploadTestCases upload = Framework.AddInAction as AddInUploadTestCases;
            
            Framework.Status.Start();
        }



    }
}
