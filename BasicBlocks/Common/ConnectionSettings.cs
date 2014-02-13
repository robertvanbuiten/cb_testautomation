using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreBank
{
    public class ConnectionSettings
    {
        public string Address;
        public string User;
        public string Password;
        public string Port;
        public string Database;
        public string Project;
        public string Domain;
        public string Repository;
        public string ConfigFile;

        public ConnectionSettings()
        {
            Address = Global.Default._Address;
            User = Global.Default._User;
            Password = Global.Default._Password;
            Port = Global.Default._Database;
            Project = Global.Default._Project;
            Repository = Global.Default._Repository;
            Domain = Global.Default._Domain;
            Database = Global.Default._Database;
        }

       

    }
}
