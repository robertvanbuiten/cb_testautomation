using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CoreBank
{
    [XmlType("TechnicalLog")]
    public class LogStatistics
    {
        [XmlArray("Log")]
        [XmlArrayItem("Line")] 
        public List<LogLine> Lines;

        public LogStatistics()
        {
            this.Lines = new List<LogLine>();
        }

        public void Start()
        {
           
        }

        public void Stop()
        {

        }

        

    }
}
