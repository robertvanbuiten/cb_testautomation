using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CoreBank
{
    public class GUI
    {
        [XmlAttribute("id", DataType="string")]
        public string Application;

        [XmlIgnore]
        public string ID;
        
        [XmlArray("screens")]
        [XmlArrayItem("screen")]
        public List<Screen> Screens;

        public GUI()
        {

        }

    }

    public class Custom
    {
        [XmlIgnore]
        public string ID;

        [XmlArray("functions")]
        [XmlArrayItem("function")]
        public List<Function> Functions;

        public Custom()
        {

        }

    }
}
