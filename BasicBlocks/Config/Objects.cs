using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CoreBank
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    [XmlType("screen")]
    public class Screen
    {
        [XmlAttribute("id", DataType = "string")]
        public string Name;

        [XmlIgnore]
        public string ID;
                
        [XmlArray("screenobjects")]
        [XmlArrayItem("screenobject")]
        public List<Screenobject> Screenobjects;
        
        public Screen()
        {
            this.Screenobjects = new List<Screenobject>();
        }
    }

    /// <summary>
    /// 
    /// </summary>

    [XmlType("screenobject")]
    public class Screenobject
    {
        [XmlAttribute("id", DataType = "string")]
        public string Type;

        [XmlIgnore]
        public string ID;
        
        [XmlElement("name")]
        public string Name;

        [XmlElement("technical")]
        public string Technical;

        [XmlElement("value")]
        public string Value;

        [XmlElement("index")]
        public string Index;

        [XmlElement("description")]
        public string Description;

        [XmlArray("childs")]
        [XmlArrayItem("screenobject")]
        public List<Screenobject> Childs;

        public Screenobject()
        {
            this.Type = "";
            this.Name = "";
            this.Technical = "";
            this.Value = "";
            this.Index = "";
            this.Description = "";
            this.Childs = new List<Screenobject>();
        }
    }

    /// <summary>
    /// 
    /// </summary>

    [XmlType("function")]
    public class Function
    {
        [XmlAttribute("id", DataType = "string")]
        public string Name;

        [XmlIgnore]
        public string ID;
        
        [XmlArray("functions")]
        [XmlArrayItem("function")]
        public List<Customobject> Customobjects;
           
        public Function()
        {
            this.Customobjects = new List<Customobject>();
        }
    }

    /// <summary>
    /// 
    /// </summary>

    [XmlType("customobject")]
    public class Customobject
    {
        [XmlIgnore]
        public string ID;

        [XmlElement("name")]
        public string Name;

        [XmlElement("description")]
        public string Description;

        
        public Customobject()
        {
            this.Name = "";
            this.Description = "";
        }
    }

   
}
