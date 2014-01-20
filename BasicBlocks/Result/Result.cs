using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace CoreBank
{
    public class Result
    {
        public ActionStatistics Action;
        public TimeStatistics Time;
        public LogStatistics Log;

        public Bitmap ScreenShot;
        
        public Result()
        {
            this.Action = new ActionStatistics();
            this.Time = new TimeStatistics();
            this.Log = new LogStatistics();
            this.ScreenShot = null;
        }
    }
}
