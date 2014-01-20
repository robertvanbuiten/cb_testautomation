using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreBank
{
    public class LogLine
    {
        public int LineNumber;
        public string DateTime;
        public string Result;
        public string Description;

        public string ErrorMessage;
        public string ErrorStackTrace;

        public LogLine()
        {
            Init();
            this.Description = "";
            this.Result = "";
            this.ErrorMessage = "";
            this.ErrorStackTrace = "";
        }

        public LogLine(string Description, RESULT result)
        {
            Init();
            this.Description = Description;
            this.Result = result.ToString();
            this.ErrorMessage = "";
            this.ErrorStackTrace = "";
        }

        public LogLine(string Description, RESULT result, string errorMessage, string stacktrace)
        {
            Init();
            this.Description = Description;
            this.Result = result.ToString();
            this.ErrorMessage = errorMessage;
            this.ErrorStackTrace = stacktrace;
        }

        private void Init()
        {
            System.DateTime thisDateTime = System.DateTime.Now;
            this.DateTime = thisDateTime.ToString("dd-MM-yyyy [hh:mm:ss:fff]");

            this.LineNumber++;
        }


    }
}
