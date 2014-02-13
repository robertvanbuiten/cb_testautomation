using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
using System.IO;
using Excel=Microsoft.Office.Interop.Excel;

using CoreBank;

namespace CoreBank.Client
{
    public class ExcelWorkbook
    {
        public Excel.Workbook Base;
        private Excel.Application Application;
        public bool Valid;
        public TEMPLATES Template;

        /// ----------------------------------------------------------------------------------------------------------
        /// <summary>
        /// EXCEL / Workbook functions
        /// </summary>
        /// <returns></returns>
        /// ----------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Check current workbook; set process workbook
        /// Check if process is found in config.xml
        /// Check if Application of active process is found in config.xml
        /// </summary>
        /// <returns></returns>

        public ExcelWorkbook()
        {
            this.Valid = this.Read();
        }

        public bool Read()
        {
            bool blnResult;
            blnResult = false;

            if (UseCurrentActiveApplication())
            {
                if (ActivateWorkbook())
                {
                    Template = ReadSheets();
                    if (Template != TEMPLATES.UNKNOWN)
                    {
                        blnResult = true;
                    }
                }
            }

            return blnResult;

        }

        private TEMPLATES ReadSheets()
        {
            bool blnWorksheet = true;

            Excel.Worksheet general = null;
            Excel.Worksheet tc = null;
            Excel.Worksheet flows = null;

            Template = TEMPLATES.UNKNOWN;

            foreach (Excel.Worksheet worksheet in Base.Worksheets)
            {
                blnWorksheet = true;
                string codeName = "";

                try
                {
                    codeName = worksheet.Name.ToString().ToLower();
                }
                catch (Exception ex)
                {
                    blnWorksheet = false;
                }

                if (blnWorksheet == true)
                {
                    if (string.Compare(codeName, "general", true) == 0)
                    {
                        general = worksheet;
                    }
                    else if (string.Compare(codeName, "flows", true) == 0)
                    {
                        flows = worksheet;
                    }
                    else if (string.Compare(codeName, "testcases", true) == 0)
                    {
                        tc = worksheet;
                    }
                }
            }

            if (general != null && tc != null && flows != null)
            {
                Template = TEMPLATES.PROCESS;
                Framework.Process = new ProcessWorkbook(Base, flows, tc);
            }

            return Template;
        }


        private bool UseCurrentActiveApplication()
        {
            bool blnResult = false;

            try
            {
                this.Application = Globals.ThisAddIn.Application;
                blnResult = true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Could not activate application. \n" + ex.Message);
                blnResult = false;
            }

            return blnResult;
        }

        /// <summary>
        /// Activate workbook in current application
        /// </summary>
        /// <returns></returns>
    
        private bool ActivateWorkbook()
        {
            bool blnResult = true;

            try
            {
                this.Base = (Excel.Workbook)this.Application.ActiveWorkbook;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Could not activate current workbook. \n" + ex.Message);
                blnResult = false;
            }

            return blnResult;
        } 
      
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

     

    }
}
