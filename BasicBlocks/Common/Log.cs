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
        public bool Correct;

        //private static iLog singleton;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fileName"></param>
        
        public Log()
        {
            this.Lines = new List<LogLine>();
            this.LineNumber = 1;
            this.Correct = false;
        }

        /// <summary>
        /// Error occured
        /// </summary>
        /// <param name="description"></param>
        /// <param name="result"></param>
        /// <param name="type"></param>
        /// <param name="errorMessage"></param>
        /// <param name="StackTrace"></param>

        public void AddCorrect(string description)
        {
            CorrectLine line = new CorrectLine();
            line.Description = description;
            
            this.Lines.Add(line);
        }

        public void AddIssue(string description)
        {
            IssueLine line = new IssueLine();
            line.Description = description;

            this.Lines.Add(line);
        }

        public void AddError(string description, string error, string stack)
        {
            ErrorLine line = new ErrorLine();
            line.Description = description;
            line.ErrorMessage = error;
            line.ErrorStackTrace = stack;

            Lines.Add(line);
        }
    }
}
