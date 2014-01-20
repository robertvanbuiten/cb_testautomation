using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreBank
{
    
    public class ExcelTest
    {
        /// <summary>
        /// Excel Representation
        /// </summary>
       
        public string Name;
        public string Message;
        
        // Properties of a test case

        public string ID;
        public string ResourceID;
        public string Description;
        public string ExpectedResult;
        public long Row;
        public DateTime ExecutionDate;
        public bool Select;
        public string BasicFlow;

        public List<ExcelData> Data;
        public ExcelFlow Flow;

        public ExcelTest()
        {
            Init();
        }

        public void Init()
        {
            Data = new List<ExcelData>();
            Flow = null;
        }

        public void CreateMessage()
        {
            string message = "";
            string name = "";

            name = Framework.ActiveProcess.Name + "." + this.ID + "." + this.Description;

            message = message + @"PROPERTIES" + "\n";
            message = message + "ID=" + this.ID + "\n";
            message = message + "Title=" + this + "\n";
            message = message + "Description=" + this.Description + "\n";
            message = message + "Expected Result=" + this.ExpectedResult + "\n\n";

            message = message + "ACTIONS" + "<br />";

            // Create actions

            this.Name = name;
            this.Message = message;

        }
    }

    public class ExcelFlow
    {
        public string Name { get; set;}
        public List<TemplateRow> Rows;
        public long Column;

        public ExcelFlow()
        {

        }



    }

    public class ExcelData
    {
        public string Name { get; set;}
        public string Value {get;set;}
        public long Column {get;set;}

        public ExcelData()
        {


        }

    }

    public class TemplateRow
    {
        public BASE_TYPE Type;
        public bool Valid = false;
        public bool Stop = false;

        public long Number;
        public TemplateCell Name;
        public TemplateCell Flow;

        public TemplateRow()
        {
            this.Name = new TemplateCell();
            this.Flow = new TemplateCell();
        }

        public TemplateRow(TemplateCell name, TemplateCell value)
        {
            this.Name = name;
            this.Flow = value;
        }

        public void Check()
        {
            bool blnResult = false;

            if (CheckType())
            {
                if (CheckValue())
                {
                    blnResult = true;
                }
            }

            Valid = blnResult;
        }

        public bool CheckValue()
        {
            bool blnResult = true;

            if (this.Flow.Value.IndexOf(KEYWORDS.glnStopTestPrefix, 1) >= 1)
            {
                this.Stop = true;
                this.Flow.Value.Replace(KEYWORDS.glnStopTestPrefix, KEYWORDS.glnNothing);
            }

            if (this.Flow.Value.IndexOf(KEYWORDS.glnGet,1) == 1)
            {
                this.Flow.Value.Replace(KEYWORDS.glnGet, KEYWORDS.glnNothing);

                ExcelData data = GetTestData(this.Flow.Value);

                if (data != null)
                    this.Flow.Value = data.Value;
            }

            return blnResult;
        }

        public bool CheckType()
        {
            bool blnResult = true;

            if (Name.BackGroundColor == COLORS.lngClYellow)
            {
                Type = BASE_TYPE.SCREEN;
            }
            else if (Name.BackGroundColor == COLORS.lngClPurple)
            {
                Type = BASE_TYPE.FILE;
            }
            else
            {
                if (Name.Value.IndexOf(KEYWORDS.glnCustomPrefix, 1) == 1)
                {
                    Type = BASE_TYPE.CUSTOM;
                    Name.Value.Replace(KEYWORDS.glnCustomPrefix, KEYWORDS.glnNothing);
                }
                else
                {
                    Type = BASE_TYPE.ATTRIBUTE;
                }
            }

            return blnResult;

        }

        public ExcelData GetTestData(string value)
        {
            ExcelData result = null;
            List<ExcelData> data = new List<ExcelData>();
            try
            {
                data = (from d in Framework.Test.Data where String.Compare(d.Name, value, true) == 0 select d).ToList<ExcelData>();
            }
            catch { }

            if (data.Count == 1)
            {
                result = data[0];
            }

            return result;
        }
    }

    public class TemplateCell
    {
        public BASE_TYPE Type;

        public string Value;
        public long FontColor;
        public long BackGroundColor;

        public TemplateCell(string val, long fontcolor, long backcolor)
        {
            this.Type = BASE_TYPE.UNKNOWN;
            this.Value = val;
            this.FontColor = fontcolor;
            this.BackGroundColor = backcolor;
        }

        public TemplateCell()
        {

        }

    }

    /// <summary>
    /// Compiled test
    /// Serializable
    /// 
    /// </summary>

    public class CompiledTest
    {
        public ExcelTest Base;
        public Flow Flow;

        public CompiledTest(ExcelTest test)
        {
            this.Base = test;
        }

        public bool ReadFlow()
        {
            bool blnResult = false;

            Flow = new Flow(Base.Name, Base.BasicFlow);
            
            return blnResult;
        }


    }
    

    public class Flow
    {
        public string Name;
        public string Process;
        public Result Result;   

        // List of Actions
        public List<Action> Actions;
       
        public Flow(string name, string process)
        {
            this.Name = name;
            this.Process = process;
            this.Actions = new List<Action>();
        }

    }

   

}
