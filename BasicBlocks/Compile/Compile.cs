using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;

namespace CoreBank
{
    public class Compile
    {

        public bool Valid;
        public List<string> Messages;
        
        public Compile() 
        {
            this.Messages = new List<string>();
            this.Valid = false;
        }

        public bool DoWork()
        {
            bool blnResult = false;
            return blnResult;
        }

        protected virtual bool Convert()
        {
            bool blnResult = false;
            return blnResult;
        }

        protected virtual bool Prepare()
        {
            bool blnResult = false;
            return blnResult;
        }

        protected virtual bool Finish()
        {
            bool blnResult = false;
            return blnResult;
        }

    }

    public class ProcessCompileMultipleTests: Compile
    {
        public ProcessCompileMultipleTests() : base() { }

        protected override bool Prepare()
        {
            return base.Prepare();
        }

        protected override bool Convert()
        {
            return base.Convert();
        }

        protected override bool Finish()
        {
            return base.Finish();
        }

    }

    public class ProcessCompileTest : Compile
    {
        public ProcessCompileTest(ProcessWorkbook wb) : base() { }

        // Sources
        private ProcessWorkbook _WB;
        private ExcelTest _Test;

        // New compiled test
        public CompiledTest CompiledTest;

        public bool DoWork(ExcelTest test)
        {
            bool blnResult = false;

            // Read flow
            // Read test data
            if (Prepare())
            {
                // Convert flow
                if (Convert())
                {
                    blnResult = true;
                }
            }

            Finish();

            return blnResult;
        }

        /// <summary>
        /// PREPARE EXCEL TEST
        /// Upgrade the labels of flow and testdata with actual data
        /// 1. Read flow (values, interior and font color)
        /// 2. Test data 
        /// for report purpose.
        /// </summary>
        /// <returns></returns>

        protected override bool Prepare()
        {
            bool blnResult = false;

            if (ReadFlow())
            {
                if (ReadData())
                {
                    blnResult = true;
                }
            }

            return blnResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        private bool ReadFlow()
        {
            bool blnResult = false;

            long col = this._Test.Flow.Column;
            long ncol = FLOW_COLUMNS.Name;
            Excel.Range name;
            Excel.Range value;

            for (long row = PROCESS_ROWS.Testcase; row <= this._WB.shtFlows.RowMax; row++)
            {
                name = (Excel.Range)this._WB.shtTestcases.Base.Cells[row, col];
                value = (Excel.Range)this._WB.shtTestcases.Base.Cells[row, ncol];

                if (!name.MergeCells)
                {
                    TemplateCell Name = new TemplateCell(name.Value, name.Font.ColorIndex, name.Interior.ColorIndex);
                    TemplateCell Flow = new TemplateCell(value.Value, value.Font.ColorIndex, value.Interior.ColorIndex);

                    TemplateRow Row = new TemplateRow(Name, Flow);
                    Row.Check();

                    this._Test.Flow.Rows.Add(Row);

                    if (Row.Stop)
                    {
                        break;
                    }
                }
            }

            return blnResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        private bool ReadData()
        {
            bool blnResult = false;
            long row = this._Test.Row;

            for (long col = PROCESS_COLUMNS.Data; col <= this._WB.shtTestcases.ColMax; col++)
            {
                if (!string.IsNullOrWhiteSpace(this._WB.shtTestcases.Values[row, col]))
                {
                    ExcelData exceldata = new ExcelData();

                    exceldata.Name = this._WB.shtTestcases.Values[PROCESS_ROWS.Header, col];
                    exceldata.Value = this._WB.shtTestcases.Values[row, col];
                    exceldata.Column = col;

                    this._Test.Data.Add(exceldata);
                }
            }

            return blnResult;
        }

        /// <summary>
        /// Convert 
        /// </summary>
        /// <returns></returns>

        protected override bool Convert()
        {
            bool blnResult = false;
            
            
            foreach(TemplateRow Row in this._Test.Flow.Rows)
            {
                if (Row.Type == BASE_TYPE.SCREEN)
                {

                }

            }

            return blnResult;
        }

        protected override bool Finish()
        {
            return true;
        }

    }
}
