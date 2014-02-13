using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CoreBank
{
    [XmlType("Actions")]
    public class ActionStatistics
    {
        [XmlElement("NotTested")]
        public int nNotTested = 0;

        [XmlElement("Errors")]
        public int nErrors = 0;

        [XmlElement("Issues")]
        public int nIssues = 0;

        [XmlElement("Correct")]
        public int nCorrect = 0;

        [XmlElement("Total")]
        public int nTotal = 0;

        public ActionStatistics()
        {
            this.nTotal = 0;
            this.nErrors = 0;
            this.nNotTested = 0;
            this.nCorrect = 0;
            this.nIssues = 0;
        }
    
    }

    

}
