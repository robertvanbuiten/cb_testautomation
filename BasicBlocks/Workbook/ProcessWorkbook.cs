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
    public class CoreBankWorkbook
    {
        public string Name { get; set; }
        public bool Valid { get; set; }
        public string FullPath { get; set; }

        public Excel.Workbook Base; 

        public CoreBankWorkbook(Excel.Workbook wb)
        {
            this.Base = wb;
            this.Name = wb.Name;
            this.FullPath = wb.FullName;
        }

        protected virtual void Reset()
        {


        }
          

        public virtual bool Check()
        {
            bool blnResult = false;


            return blnResult;
        }

        public virtual bool Read()
        {
            bool blnResult = false;

          
            return blnResult;
        }

        public bool CloseWorkbook()
        {
            bool blnResult = true;

            try
            {
                this.Base.Close();
            }
            catch (Exception ex)
            {
                blnResult = false;
            }

            return blnResult;
        }

        /// <summary>
        /// Process to temp
        /// </summary>
        /// <returns></returns>


        public bool CopyToTemp()
        {
            bool blnResult = true;

            string destination = System.IO.Path.Combine(Framework.Paths.TempPath, Framework.Process.Name);

            try
            {
                this.Base.Save();

                if (File.Exists(destination))
                {
                    try
                    {
                        File.Delete(destination);
                    }
                    catch (Exception ex) { }
                }

                //File.Copy(Framework.CurrentWorkbook.FullName, destination);
                this.Base.SaveAs(destination, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Microsoft.Office.Interop.Excel.XlSaveConflictResolution.xlUserResolution, Type.Missing, Type.Missing);
                //System.IO.File.Copy(Framework.CurrentWorkbook, destination,true);
            }
            catch (System.IO.IOException ex)
            {
                blnResult = false;
            }
            return blnResult;
        }

    }

    public class ProcessWorkbook : CoreBankWorkbook
    {       
        private GUI _activegui;
        
        // Excel 
        public ShtGeneral shtGeneral;
        public ShtFlows shtFlows;
        public ShtTestcases shtTestcases;
 
        // Excel values
        public List<ExcelTest> TestCases;
        public List<ExcelFlow> BasicFlows;

        public ProcessWorkbook(Excel.Workbook wb, Excel.Worksheet flows, Excel.Worksheet tc): base(wb)
        {
            this.Reset();
            this.shtTestcases = new ShtTestcases(tc);
            this.shtFlows = new ShtFlows(tc);
            this.Init();
        }

        protected override void Reset()
        {
            this.shtGeneral = null;
            this.shtFlows = null;
            this.shtTestcases = null;
            this.TestCases = new List<ExcelTest>();
            this.BasicFlows = new List<ExcelFlow>();
        }

        public void Init()
        {
            this.shtTestcases.ReadValues();
            this.shtFlows.ReadValues();
        }

        public override bool Check()
        {
            bool blnResult = false;

            if (CheckProcess())
            {
                if (CheckApplication())
                {
                    blnResult = true;
                }
            }

            return blnResult;
        }

        private bool CheckProcess()
        {
            bool blnResult = false;

            List<Process> processes = new List<Process>();
            try
            {
                processes = (from process in Framework.Config.Processes where String.Compare(process.File, this.Name, true) == 0 select process).ToList<Process>();
            }
            catch { }

            if (processes.Count == 1)
            {
                Framework.ActiveProcess = processes[0];
                blnResult = true;
            }
            else if (processes.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("Cannot find process " + Framework.Process.Name + " check ALM and config.xml in Test Resources.");
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Found multiple processes " + Framework.Process.Name + " check ALM and config.xml in Test Resources.");
            }

            return blnResult;
        }

        private bool CheckApplication()
        {
            bool blnResult = false;

            List<Application> apps = new List<Application>();
            try
            {
                apps = (from app in Framework.Config.Applications where String.Compare(app.Name, Framework.ActiveProcess.Application, true) == 0 select app).ToList<Application>();
            }
            catch { }

            if (apps.Count == 1)
            {
                Framework.ActiveApplication = apps[0];
                blnResult = true;
            }
            else if (apps.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("Cannot find application " + Framework.ActiveProcess.Application + " check ALM and config.xml in Test Resources.");
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Found multiple processes " + Framework.ActiveProcess.Application + " check ALM and config.xml in Test Resources.");
            }

            return blnResult;
        }  

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// 
       public override bool Read()
       {
           bool blnResult = false;
           
           if (ReadTestCases())
           {
               if (ReadFlows())
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

        private bool ReadTestCases()
        {
            bool blnResult = true;

            for (long row = PROCESS_ROWS.Testcase; row <= shtTestcases.RowMax; row++)
            {
                if (!string.IsNullOrEmpty(shtTestcases.Values[row,PROCESS_COLUMNS.ID]))
                {
                    ExcelTest exceltest = new ExcelTest();

                    exceltest.ID = shtTestcases.Values[row, PROCESS_COLUMNS.ID];
                    exceltest.Description = shtTestcases.Values[row, PROCESS_COLUMNS.Description];
                    exceltest.ExpectedResult = shtTestcases.Values[row, PROCESS_COLUMNS.ExpectedResult];
                    exceltest.ResourceID = shtTestcases.Values[row, PROCESS_COLUMNS.RepositoryID];
                    exceltest.Flow = DetermineBasicFlow(shtTestcases.Values[row, PROCESS_COLUMNS.Flow]);

                    for (long col = PROCESS_COLUMNS.Data; col <= shtTestcases.ColMax; col++)
                    {
                        if (!string.IsNullOrEmpty(shtTestcases.Values[row, col]))
                        {
                            ExcelData data = new ExcelData();
                            data.Name = shtTestcases.Values[PROCESS_ROWS.Header,col];
                            data.Value = shtTestcases.Values[row,col];
                            exceltest.Data.Add(data);
                        }
                    }

                    exceltest.Row = row;
                    this.TestCases.Add(exceltest);
                }
            }

            return blnResult;
        }

      
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// 
        private ExcelFlow DetermineBasicFlow(string flow)
        {
            ExcelFlow Flow = null;
            List<ExcelFlow> flows = new List<ExcelFlow>();

            try
            {
                flows = (from f in BasicFlows where string.Compare(f.Name, flow, true) == 0 select f).ToList<ExcelFlow>();
            }
            catch { }

            if (flows.Count == 0)
            {

            }
            else if (flows.Count > 1)
            {
                
            }
            else
            {
                Flow = flows[0];
            }
        
            return Flow;
        }

        private bool ReadFlows()
        {
            bool blnResult = true;

            for (long col = PROCESS_COLUMNS.Data; col <= shtFlows.ColMax; col++ )
            {
                if (!string.IsNullOrWhiteSpace(shtTestcases.Values[PROCESS_ROWS.Header, col]))
                {
                    ExcelFlow excelflow = new ExcelFlow();
                    excelflow.Name = shtTestcases.Values[PROCESS_ROWS.Header, col];
                    excelflow.Column = col;

                    this.BasicFlows.Add(excelflow);
                }
            }
            
            return blnResult;
        }



  
       
        //{
        //    //bool blnResult = true;
        //    //ParentObject parent = null;
        //    //ChildObject child = null;
        //    //Excel.Range firstCell = null;

        //    //for (long nRow = 1; nRow <= .shtValuesRowBound; nRow++)
        //    //{
        //    //    firstCell = null;
        //    //    try
        //    //    {
        //    //        firstCell = (Excel.Range)Flows.Base.Cells[nRow, 1];
        //    //    }
        //    //    catch (Exception ex)
        //    //    {
        //    //        blnResult = false;
        //    //        break;
        //    //    }

        //    //    if (firstCell is Excel.Range)
        //    //    {
        //    //        if (firstCell.MergeCells = false)
        //    //        {
        //    //            string strConvert = Convert.ToString(firstCell.get_Value());
        //    //            int colorNumber = Convert.ToInt32(firstCell.Interior.Color);

        //    //            if (colorNumber == COLORS.lngClYellow || colorNumber == COLORS.lngClPurple)
        //    //            {
        //    //                parent = new ParentObject(nRow);
        //    //                parent.Name = strConvert.ToLower().Trim();
        //    //                parent.Base = BASE.SCREEN;
        //    //            }
        //    //            else if (strConvert.IndexOf("_") == 0)
        //    //            {
        //    //                parent = new ParentObject(nRow);
        //    //                parent.Name = strConvert.ToLower().Trim().Replace("_", "");
        //    //                parent.Base = BASE.CUSTOM;
        //    //            }
        //    //            else
        //    //            {
        //    //                if (parent != null)
        //    //                {
        //    //                    child = new ChildObject(nRow);
        //    //                    child.Name = strConvert.ToLower().Trim();
        //    //                    child.Parent = parent;
        //    //                }
        //    //            }
        //    //        }
        //    //    }
        //    //}

        //    //return blnResult;
        //}

        public bool DetermineGUI()
        {
            bool blnResult = false;
            List<GUI> guis = new List<GUI>();

            guis = (from g in Framework.Config.GUIS where string.Compare(g.Application, Framework.ActiveProcess.Name) >= 1 select g).ToList<GUI>();

            if (guis.Count == 1)
            {
                _activegui = guis[0];
                blnResult = true;
            }

            return blnResult;
        }

        //public bool CheckObjects()
        //{
        //    bool blnResult = false;

        //    foreach (ChildObject child in this.Objects)
        //    {
        //        bool blnFound = false;
        //        bool blnValid = false;
        //        string Message = "";
                
        //        Screen screen = null;
        //        Screenobject screenobject = null;
        //        screen = (Screen)(from p in _activegui.Screens where string.Compare(p.Name, child.Parent.Name, true) >= 1 select p).Distinct<Screen>();

        //        if (screen != null)
        //        {
        //            screenobject = (Screenobject)(from so in screen.Screenobjects where string.Compare(screenobject.Name, child.Name, true) >= 1 select screenobject).Distinct<Screenobject>();

        //            if (screenobject != null)
        //            {
        //                child.Technical = screenobject;
        //                blnFound = true;
        //            }
        //            else
        //            {
        //                Message = "Cannot find screenobject with screen " + screen.Name;
        //            }
        //        }
        //        else
        //        {
        //            Message = "Cannot find base.";
        //        }

        //        if (blnFound)
        //        {
                    
        //        }
        //        else
        //        {
        //            this.Flows.Base.Cells[child.row, 2].Interior.Color = COLORS.lngClRed;
        //            this.Flows.Base.Cells[child.row, 2].Value = Message;
        //        }
        //    }


        //    return blnResult;
        //}
    }
}
