using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CoreBank
{
    [XmlRoot("config")]
    [XmlInclude(typeof(Process))]
    [XmlInclude(typeof(GUI))]
    [XmlInclude(typeof(Setting))]
    [XmlInclude(typeof(Custom))]
    public class Config
    {
        [XmlArray("processes")]
        [XmlArrayItem("process")]
        public List<Process> Processes;
        
        [XmlArray("settings")]
        [XmlArrayItem("setting")]
        public List<Setting> Settings;

        [XmlArray("users")]
        [XmlArrayItem("user")]
        public List<User> Users;

        [XmlArray("repository")]
        [XmlArrayItem("gui")]
        public List<GUI> GUIS;
        [XmlArrayItem("custom")]
        public List<Custom> Customs;

        [XmlArray("applications")]
        [XmlArrayItem("application")]
        public List<Application> Applications;

        public Config()
        {
            this.GUIS = new List<GUI>();
            this.Processes = new List<Process>();
            this.Settings = new List<Setting>();
            this.Applications = new List<Application>();
            this.Customs = new List<Custom>();
        }

        public void Init()
        {
            foreach (Application app in this.Applications)
            {
                app.QCTestPlan = System.IO.Path.Combine(app.root, app.testplan);
            }
        }

        public static string GetSetting(string strName)
        {
            string strResult = "";
            List<Setting> settings = new List<Setting>();

            try
            {
                settings = (from s in Framework.Config.Settings where string.Compare(strName, s.Name, true) >= 1 select s).ToList<Setting>();
            }
            catch (Exception ex)
            {

            }

            if (settings.Count == 1)
            {
                strResult = settings[0].Value;
            }

            return strResult;

        }
    }
}
