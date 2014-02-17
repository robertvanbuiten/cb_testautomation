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
            this.tbResult.Text = "BUSY";
            this.tbShortMessage.Text = Framework.AddInAction.Progress.Message;
            this.tbDetails.Visible = false;
            this.Show();
            this.Refresh();
        }
       
 
        private void button1_Click(object sender, EventArgs e)
        {
            if (bw.WorkerSupportsCancellation)
            {
                if (bw.IsBusy)
                {
                    bw.CancelAsync();
                }
                else
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }

        public void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            if (Framework.AddInAction is AddInConnect)
            {
                AddInConnect connect = Framework.AddInAction as AddInConnect;
                connect.DoWork();
            }
            else if (Framework.AddInAction is AddInUploadProcess)
            {
                AddInUploadProcess process = Framework.AddInAction as AddInUploadProcess;
                process.DoWork();
            }
            else if (Framework.AddInAction is AddInUploadTestCases)
            {
                AddInUploadTestCases testcases = Framework.AddInAction as AddInUploadTestCases;
                testcases.DoWork();
            }
            else
            {
                Framework.Log.AddError("Action unknown.", "", "");
            }
        }

        public void bw_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            this.tbDetails.Visible = true;
            this.btnCancel.Text = "OK";
     
            if (e.Cancelled == true)
            {
                this.tbResult.Text = "CANCELED";
                this.tbShortMessage.Text = Framework.AddInAction.Progress.Message;
            }
            else if (e.Error != null)
            {
                this.tbResult.Text = "ERROR";
                this.tbShortMessage.Text = Framework.AddInAction.Progress.Message;
                this.tbDetails.Text = e.Result.ToString();
            }
            else
            {
                if (Framework.Log.Lines.Count >= 1)
                {
                    //LogLine line = Framework.Log.Lines.Last<LogLine>();

                    foreach (LogLine line in Framework.Log.Lines)
                    {
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
            }

            Refresh();
        }

        public void bw_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage <= 100)
            {
                pb.Value = e.ProgressPercentage;
            }
            else
            {
                pb.Value = 100;
            }
            this.tbShortMessage.Text = Framework.AddInAction.Progress.Message;
            this.tbResult.Text = "BUSY";
        }

        public void CreateErrorMessage(ErrorLine err)
        {
            this.tbResult.Text = "ERROR";
            this.tbDetails.AppendText(err.Description);
            this.tbDetails.AppendText(err.ErrorMessage);
        }

        public void CreateCorrectMessage(CorrectLine correct)
        {
            this.tbResult.Text = "FINISHED";
            this.tbDetails.Text = correct.Description;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbShortMessage_TextChanged(object sender, EventArgs e)
        {

        }


    }
}
