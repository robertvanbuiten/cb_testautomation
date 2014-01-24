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

            if (Directory.Exists(Framework.Paths.ResourcePath))
            {
                blnResult = true;
            }

            return blnResult;
        }

        public override bool GetResource(string source, string name)
        {
            bool blnResult = false;

            string resource = "";
            string destination = "";

            resource = Path.Combine(source, name);
            destination = Path.Combine(Framework.Paths.TempPath,name);
            
            if (File.Exists(resource))
            {
                File.Delete(destination);
                File.Copy(resource, destination);

                if (File.Exists(destination))
                {
                    blnResult = true;
                }
                
            }

            return blnResult;
        }
    }
}
