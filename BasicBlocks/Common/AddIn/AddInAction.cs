using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CoreBank.Common.AddIn.Forms;

namespace CoreBank
{
    public class AddInAction
    {
        public string Title;
        public ProgressPercentage Progress;
        public ADDIN_ACTION Action = ADDIN_ACTION.UNKNOWN;
        public TEMPLATES Template;
        public bool Result;

        public AddInAction()
        {
            Init();
            Template = TEMPLATES.UNKNOWN;
        }
        
        public AddInAction(TEMPLATES template)
        {
            Init();
            Template = template;
        }

         private void Init()
        {
            Result = false;
            Framework.Log = new Log();
            Framework.Status = new Status();
        }

        public void DoWork()
        {
            Framework.Status.Start();

            if (Framework.Connected)
            {

                if (Prepare())
                {
                    if (Execute())
                    {
                        this.Result = true;
                    }
                }

                if (Finish())
                {
                    this.Result = true;
                }
            }
            else
            {
                Framework.Log.AddError("Not connected with repository. Cannot execute function.", "", "");
            }

            Stop();
        }

        private void Stop()
        {
            LogLine line = Framework.Log.Lines.Last<LogLine>();

            if (line is ErrorLine)
            {
                ErrorLine err = line as ErrorLine;
                //Form.CreateErrorMessage(err);
            }
            else if (line is CorrectLine)
            {
                CorrectLine correct = line as CorrectLine;
                //Form.CreateCorrectMessage(correct);
            }

        }

        protected virtual bool Prepare()
        {
            return false;
        }

        protected virtual bool Execute()
        {
            return false;
        }

        protected virtual bool Finish()
        {
            return false;
        }


    }

    public class ProgressPercentage
    {
        public string Action;
        public string Message;

        public int Total;
        public int Current;
        public int Index;
        private int percentage;
        
        public ProgressPercentage(string action, int total)
        {
            Action = action;
            Total = total;
            Message = "";
            percentage = 100 / total;
            Current = 0;
        }

        public void Continue()
        {
            Index++;
            Current = Current + percentage;
            Message = Current.ToString() + "%";  
        }

    }
}
