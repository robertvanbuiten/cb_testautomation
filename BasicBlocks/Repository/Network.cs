using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CoreBank
{
    public class Network : Repository
    {
        public Network(ConnectionSettings settings):base(settings)
        {

        }

        public override bool Connect()
        {
            bool blnResult = false;
            string configpath = "";

            configpath = Path.Combine(Settings.Address, Settings.ConfigFile);

            if (File.Exists(configpath))
            {
                File.Copy(configpath, Framework.Paths.ConfigPath);
            }

            return blnResult;
        }

        public override bool GetResource(string source, string name)
        {
            bool blnResult = false;

            return blnResult;
        }
    }
}
