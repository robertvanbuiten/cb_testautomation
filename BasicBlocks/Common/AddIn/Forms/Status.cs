using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CoreBank.Common.AddIn.Forms
{
    public partial class Status : Form
    {
        public TEMPLATES Template;
        public bool Result;
        
        public Status()
        {
            InitializeComponent();
        }

        public void Start()
        {
            this.bw.RunWorkerAsync();
            this.Text = Framework.AddInAction.Title;
            this.tbDetails.Visible = false;
            this.Show();
        }
       
 
        private void button1_Click(object sender, EventArgs e)
        {
            if (bw.WorkerSupportsCancellation)
            {
                bw.CancelAsync();
            }
        }

        public void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            if (Framework.AddInAction is AddInConnect)
            {
                AddInConnect connect = Framework.AddInAction as AddInConnect;
                connect.DoWork();
            }
            else if (Framework.Template == TEMPLATES.PROCESS && Framework.AddInAction is AddInUploadProcess)
            {
                AddInUploadProcess process = Framework.AddInAction as AddInUploadProcess;
                process.DoWork();
            }
            else if (Framework.Template == TEMPLATES.PROCESS && Framework.AddInAction is AddInUploadTestCases)
            {
                AddInUploadTestCases testcases = Framework.AddInAction as AddInUploadTestCases;
                testcases.DoWork();
            }
        }

        public void bw_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                this.tbResult.Text = "CANCELED";
                this.tbShortMessage.Text = Framework.AddInAction.Progress.Message;
                this.tbDetails.Visible = false;
            }
            else if (e.Error != null)
            {
                this.tbResult.Text = "ERROR";
                this.tbShortMessage.Text = Framework.AddInAction.Progress.Message;
                this.tbDetails.Visible = false;
                this.tbDetails.Text = e.Result.ToString();
            }
            else
            {
                LogLine line = Framework.Log.Lines.Last<LogLine>();

                if (line is ErrorLine)
                {
                    ErrorLine err = line as ErrorLine;
                    this.CreateErrorMessage(err);
                }
                else if (line is CorrectLine)
                {
                    CorrectLine correct = line as CorrectLine;
                    this.CreateCorrectMessage(correct);
                }

            }
        }

        public void bw_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            pb.Value = e.ProgressPercentage;
            this.tbResult.Text = "BUSY";
            this.tbShortMessage.Text = Framework.AddInAction.Progress.Message;
            this.tbDetails.Visible = false;
            Refresh();
        }

        public void CreateErrorMessage(ErrorLine err)
        {
            this.tbResult.Text = "ERROR";
            this.tbShortMessage.Text = err.ShortMessage;
            this.tbDetails.Visible = false;
            this.tbDetails.AppendText(err.Description);
            this.tbDetails.AppendText(err.ErrorMessage);
        }

        public void CreateCorrectMessage(CorrectLine correct)
        {
            this.tbResult.Text = "FINISHED";
            this.tbShortMessage.Text = correct.ShortMessage;
            this.tbDetails.Visible = false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
