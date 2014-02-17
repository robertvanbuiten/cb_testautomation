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
            this.Progress = new ProgressPercentage("Connecting", 2);
            this.Title = "Connecting with " + Framework.Connection.Repository.ToString();
            this.Action = ADDIN_ACTION.CONNECT;
            this.Progress.Message = "Connecting";
        }


        protected override bool Prepare()
        {
            return true;
        }

        protected override bool Execute()
        {
            Framework.Connected = false;

            Framework.AddInAction.Progress.Continue();

            if (Framework._type == REPOSITORY_TYPE.ALM)
            {
                Framework.Connected = Framework._qc.Connect();
            }
            else if (Framework._type == REPOSITORY_TYPE.NETWORK)
            {
                Framework.Connected = Framework._nw.Connect();
            }

            Framework.AddInAction.Progress.Continue();

            return Framework.Connected;
        }

        protected override bool Finish()
        {
            bool blnResult = false;

            if (Framework.ReadRepository())
            {
                Framework.Log.AddCorrect("Read config.xml.");
                blnResult = true;
            }

            return blnResult;
        }
    }
}
