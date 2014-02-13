using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CoreBank
{
    [XmlType("process")]
    public class Process
    {
        [XmlAttribute("id", DataType = "string")]
        public string Name;

        [XmlElement("file")]
        public string File;

        [XmlElement("resourcepath")]
        public string QCTestResource;

        [XmlElement("testcasepath")]
        public string QCTestPlan;

        [XmlElement("testlabpath")]
        public string QCTestSet;

        [XmlElement("application")]
        public string Application;

        [XmlElement("description")]
        public string Description;

        public Process()
        {
            this.Name = "";
            this.Description = "";
            this.Application = "";
            this.QCTestResource = "";
            this.QCTestPlan = "";
        }
    }
}
