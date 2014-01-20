using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CoreBank
{
    [XmlType("application")]
    [XmlInclude(typeof(Environment))]
    public partial class Application
    {
        [XmlAttribute("id", DataType = "string")]
        public string Name { get; set; }

        [XmlElement("qctestplan")]
        public string testplan { get; set; }
                
        [XmlElement("qctestplanroot")]
        public string root { get; set; }

        [XmlIgnore]
        public string QCTestPlan { get; set; }

        [XmlArray("environments")]
        [XmlArrayItem("environment")]
        public List<Environment> Environments;
        
        public Application()
        {
            this.Environments = new List<Environment>();
        }
    }

    [XmlType("environment")]
    public class Environment
    {
        [XmlAttribute("id", DataType = "string")]
        public string Name { get; set; }

        [XmlElement("platform")]
        public string Platform { get; set; }

        [XmlElement("process")]
        public string Process { get; set; }

        [XmlElement("parameter")]
        public string Parameter { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        public Environment()
        {

        }
    }

    
}
