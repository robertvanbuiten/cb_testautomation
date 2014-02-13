using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Xml.Serialization;

namespace CoreBank
{
    [XmlType("TimeSummary")]
    public class TimeStatistics
    {
        [XmlIgnore]
        public Stopwatch Time;
                
        [XmlElement("Seconds")]
        public string strSeconds;
        
        [XmlElement("MiliSeconds")]
        public string strMiliSeconds;

        [XmlElement("StartTime")]
        public string StartTime;

        [XmlElement("EndTime")]
        public string EndTime;
                        
        public TimeStatistics()
        {
            this.strSeconds = "";
            this.strMiliSeconds = "";
            this.StartTime = "";
            this.EndTime = "";
            this.Time = new Stopwatch();
        }

        public void Start()
        {
            DateTime timeNow = DateTime.Now;

            this.Time.Start();
            this.StartTime = timeNow.ToString("dd-MM-yyyy [hh:mm:ss.fff]");
        }

        public void Stop()
        {
            DateTime timeNow = DateTime.Now;
            this.EndTime = timeNow.ToString("dd-MM-yyyy [hh:mm:ss.fff]");

            this.Time.Stop();
            this.strMiliSeconds = this.Time.ElapsedMilliseconds.ToString();
            this.strSeconds = this.Time.Elapsed.Seconds.ToString();


        
        }



    }
}
