using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CoreBank
{
    public enum BASE_TYPE
    {
        SCREEN,
        FILE,
        DATABASE,
        ATTRIBUTE,
        CUSTOM,
        UNKNOWN
    }

    public enum REPOSITORY_TYPE
    {
        ALM,
        NETWORK,
        JIRA,
        UNKNOWN
    }

    public enum FUNCTION_CUSTOM
    {
        START_APPLICATION,
        STOP_APPLICATION,
        GET_NUMBER,
        GET_SYSTEMDATE,
        RUN_DAYEND,
        MAKE_SCREENPRINT,
        UNKNOWN
    }

    public enum PLATFORM_SCREEN
    {
        IE,
        FIREFOX,
        CHROME,
        SAFARI,
        WINDOWS,
        JAVA,
        MAINFRAME,
        SSH,
        UNKNOWN
    }

    public enum PLATFORM_FILE
    {
        FTP,
        SFTP,
        HTTP,
        HTTPS,
        SOAP,
        FILE,
        UNKNOWN
    }

    public enum ADDIN_ACTION
    {
        CONNECT,
        DISCONNECT,
        PROCESS_UPLOADTEMPLATE,
        PROCESS_UPLOADTESTCASE,
        UNKNOWN
    }
   
    public enum FASE
    {
        INIT,
        EXECUTE,
        STOP
    }

    public enum ACTION
    {
        CHECK,
        FILL,
        CLICK,
        PUT,
        GET,
        DO,
        TEXT,
        UNKNOWN
    }

    public enum RESULT
    {
        FAIL,
        PASS,
        NOT,
        TRUE,
        FALSE,
        UNKNOWN
    }

    public enum TEMPLATES
    {
        PROCESS,
        DASHBOARD,
        CHAIN,
        UNKNOWN
    }
    
    public static class COLORS
    {
        public static long lngClBlue = 16711680;
        public static long lngClBlack = 0;
        public static long lngClRed = 255;
        public static long lngClPink = 16711935;
        public static long lngClYellow = 65535;
        public static long lngClPurple = 10053222;
        public static long lngClGrey = 12566463;
    }

    public static class PROCESS_COLUMNS
    {
        public static long ID = 1;
        public static long RepositoryID = 2;
        public static long Flow = 3;
        public static long Description = 4;
        public static long ExpectedResult = 5;
        public static long Date = 6;
        public static long Select = 7;
        public static long Data = 6;
    }

    public static class FLOW_COLUMNS
    {
        public static long Name = 1;
        public static long Flow = 2;

    }

    public static class PROCESS_ROWS
    {
        public static long Testcase  = 2;
        public static long Header = 1;

    }

    public static class KEYWORDS
    {
        public static string glnEmpty = "empty";
        public static string glnNotEmpty = "notempty";
        public static string glnKeep = "keep";
        public static string glnNothing = "na";
        public static string glnPrefix = "&";
        public static string glnGet = "get";
        public static string glnPrefixOpen = "[";
        public static string glnPrefixClose = "]";
        public static string glnCustomPrefix = "_";
        public static string glnTablePrefix = "|";
        public static string glnDataSeperator = "|";
        public static string glnStopTestPrefix = "_stop";
        public static string glnOn = "on";
        public static string glnOff = "off";
    }


    [XmlType("setting")]
    public class Setting
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("value")]
        public string Value { get; set; }
        
        public Setting()
        {
            this.Name = "";
            this.Value = "";
        }

       
    }

    [XmlType("user")]
    public class User
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("login")]
        public string Login { get; set; }

        [XmlElement("password")]
        public string Password { get; set; }

        public User()
        {
            this.Name = "";
            this.Login = "";
            this.Password = "";
        }
    }
}
