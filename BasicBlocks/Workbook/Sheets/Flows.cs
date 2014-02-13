using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;

namespace CoreBank
{
    public class ShtFlows : Sheet
    {

        public ShtFlows(Excel.Worksheet sheet) : base(sheet)
        { }
    }

    public class ShtTestcases : Sheet
    {

        public ShtTestcases(Excel.Worksheet sheet)
            : base(sheet)
        {
            
        }
    }

    public class ShtGeneral : Sheet
    {

        public ShtGeneral(Excel.Worksheet sheet)
            : base(sheet)
        { }
    }
}
