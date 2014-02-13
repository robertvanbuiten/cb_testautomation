using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreBank
{
    public enum LogResult
    {
        ERROR,
        ISSUE,
        NOT,
        CORRECT
    }
    
    public class ErrorLine : LogLine
    {
        public string ErrorMessage;
        public string ErrorStackTrace;
        
        public ErrorLine() : base() { }

        public ErrorLine(string Description, string errorMessage) : base()
        {
            this.Description = Description;
            this.ErrorMessage = errorMessage;
            this.Result = LogResult.ERROR;
        }
       
    }

    public class IssueLine : LogLine
    {
        public IssueLine() : base() { }

        public IssueLine(string Description)
            : base()
        {
            this.Description = Description;
            this.Result = LogResult.ISSUE;
        }

    }

    public class CorrectLine : LogLine
    {

        public CorrectLine() : base() { }

        public CorrectLine(string Description) : base()
        {
            this.Description = Description;
            this.Result = LogResult.CORRECT;

        }

    }

    
    public class LogLine
    {
        public int LineNumber;
        public string DateTime;
        public LogResult Result;
        public string Description;
        public string ShortMessage;

        public LogLine()
        {
            Init();
        }

        protected void Init()
        {
            System.DateTime thisDateTime = System.DateTime.Now;
            this.DateTime = thisDateTime.ToString("dd-MM-yyyy [hh:mm:ss:fff]");

            this.LineNumber++;
            this.Result = LogResult.NOT;
            this.Description = "";
            this.ShortMessage = "";
        }


    }
}
