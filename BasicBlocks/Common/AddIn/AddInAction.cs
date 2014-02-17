using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

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
            if (Framework.Ready)
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

        public int iTotal;
        public decimal Total;
        public decimal Current;
        public int Index;
        private decimal percentage;
        
        public ProgressPercentage(string action, decimal total)
        {
            Action = action;
            Total = total;
            Message = "";
            percentage = (decimal)100 / total;
            iTotal = (int)Math.Round(total);
            Index = 0;
            Current = 0;
        }

        public void Continue()
        {
            Index++;
            Current = Current + percentage;
            int status = (int)Math.Round(Current);
            Framework.Status.bw.ReportProgress(status);
        }

    }
}
