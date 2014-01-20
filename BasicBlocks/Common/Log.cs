using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CoreBank
{
    /// <summary>
    /// </summary>
 
      
    public class Log
    {
        public List<LogLine> Lines; 
        public int LineNumber;

        //private static iLog singleton;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fileName"></param>
        
        public Log()
        {
            this.Lines = new List<LogLine>();
            this.LineNumber = 1;
        }

        /// <summary>
        /// Error occured
        /// </summary>
        /// <param name="description"></param>
        /// <param name="result"></param>
        /// <param name="type"></param>
        /// <param name="errorMessage"></param>
        /// <param name="StackTrace"></param>

        public void AddLine(string description, RESULT result, string errorMessage, string StackTrace)
        {
            LogLine line = new LogLine();

            line.Result = result.ToString();
            line.Description = description;
            line.ErrorMessage = errorMessage;
            line.ErrorStackTrace = StackTrace;
    
            this.Lines.Add(line);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="description"></param>
        /// <param name="result"></param>
        /// <param name="type"></param>

        public void AddLine(string description,RESULT result)
        {
            LogLine line = new LogLine();

            line.Result = result.ToString();
            line.Description = description;
            line.ErrorMessage = "";
            line.ErrorStackTrace = "";

            this.Lines.Add(line);
        }

      
    }
}
