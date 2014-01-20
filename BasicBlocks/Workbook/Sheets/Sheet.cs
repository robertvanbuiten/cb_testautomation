using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;

namespace CoreBank
{
    public class Sheet
    {

        public bool Valid = false;

        // Stores values of sheet
        public object[,] shtValues = null;
        public string[,] Values = null;

        // Row and column boundaries
        public int ColMax;
        public int RowMax;

        // Current row and column, used to locate
        public long CurrentRow;
        public long CurrentCol;

        // Actual Excel worksheet 
        public Excel.Worksheet Base;

        public Sheet(Excel.Worksheet _worksheet)
        {
            this.Base = _worksheet;
            this.CurrentRow = 1;
            this.CurrentCol = 1;
            this.Valid = ReadValues();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        public bool ReadValues()
        {
            bool blnResult = false;

            if (SetObjectArray())
            {
                if (ResetToString())
                {
                    blnResult = true;
                }
            }

            return blnResult;

        }


        private bool SetObjectArray()
        {
            bool blnResult = true;

            ColMax = 0;
            RowMax = 0;

            try
            {
                shtValues = this.Base.UsedRange.get_Value(Excel.XlRangeValueDataType.xlRangeValueDefault) as object[,];
                RowMax = shtValues.GetUpperBound(0);
                ColMax = shtValues.GetUpperBound(1)+1;
            }
            catch (Exception ex)
            {
                shtValues = null;
                blnResult = false;
                ColMax = 0;
                RowMax = 0;
            }
            return blnResult;
        }

        private bool ResetToString()
        {
            ///
            bool blnResult = true;

            Values = new string[RowMax, ColMax];

            for (long nRow = 1; nRow <= RowMax; nRow++)
            {
                for (long nCol = 1; nCol <= ColMax; nCol++)
                {
                    try
                    {
                        Values[nRow, nCol] = Convert.ToString(shtValues[nRow, nCol]);
                    }
                    catch (Exception ex)
                    {
                        blnResult = false;
                        return blnResult;
                    }
                }
            }

            RowMax++;
            ColMax++;
            return blnResult;
        }

    }
}
