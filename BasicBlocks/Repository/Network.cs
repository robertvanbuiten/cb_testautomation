using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

            return blnResult;
        }

        public override bool GetResource(string source, string name)
        {
            bool blnResult = false;

            return blnResult;
        }
    }
}
