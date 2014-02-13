using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Office = Microsoft.Office.Core;
using Excel=Microsoft.Office.Interop.Excel;
using System.Data.OleDb;
using System.Data;

namespace CoreBank
{
   
    public class ChainWorkbook : CoreBankWorkbook
    {       

        public ChainWorkbook(Excel.Workbook wb): base(wb)
        {

        }

        protected override void Reset()
        {

        }

        public void Init()
        {
        }

        public override bool Check()
        {
            bool blnResult = false;

           
            return blnResult;
        }

           
    }
}
