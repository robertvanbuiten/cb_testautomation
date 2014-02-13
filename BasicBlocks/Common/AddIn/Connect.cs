using System.Xml.Serialization;
using System.Text;
using System.IO;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;

namespace CoreBank
{
    public class AddInConnect: AddInAction
    {
        public AddInConnect(): base()
        {
            this.Progress = new ProgressPercentage("Connecting", 1);
            this.Title = "Connecting with " + Framework.Connection.Repository.ToString();
            this.Action = ADDIN_ACTION.CONNECT;
            //test
            //
            ///

        }


        protected override bool Prepare()
        {
            bool blnResult = false;

            if (Framework.Start())
            {
                blnResult = true;   
            }

            return blnResult;
        }

        protected override bool Execute()
        {
            Framework.Connected = false;

            if (Framework._type == REPOSITORY_TYPE.ALM)
            {
                Framework.Connected = Framework._qc.Connect();
            }
            else if (Framework._type == REPOSITORY_TYPE.NETWORK)
            {
                Framework.Connected = Framework._nw.Connect();
            }

            return Framework.Connected;
        }

        protected override bool Finish()
        {
            bool blnResult = false;

            if (Framework.ReadRepository())
            {
                blnResult = true;
            }

            return blnResult;
        }
    }
}
