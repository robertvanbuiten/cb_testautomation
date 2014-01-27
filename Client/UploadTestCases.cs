using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Excel;
using System.ComponentModel;
using System.Windows.Forms;

namespace CoreBank.Client
{
    public partial class ThisAddIn
    {
        /// <summary>
        /// Upload testcases and workbook
        /// </summary>
        /// <param name="Ctrl"></param>
        /// <param name="CancelDefault"></param>

        void btnUpload_Click(Office.CommandBarButton Ctrl, ref bool CancelDefault)
        {

            if (background.IsBusy != true)
            {
                statusform = new Status();
                //statusform.Canceled += new EventHandler<EventArgs>(buttonCancel_Click);
                statusform.Message = "Prepare upload.";
                statusform.Show();
                statusform.Refresh();
                // Start the asynchronous operation.
                background.RunWorkerAsync();
            }
        }

        // This event handler cancels the backgroundworker, fired from Cancel button in AlertForm.
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (background.WorkerSupportsCancellation == true)
            {
                // Cancel the asynchronous operation.
                background.CancelAsync();
                // Close the AlertForm
                statusform.Close();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>


        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            CurrentWorkbook = new ExcelWorkbook();

            if (CurrentWorkbook.Valid)
            {
                if (Framework.ReadProcess())
                {
                    int percentage = 100 / Framework.Process.TestCases.Count;
                    Framework.Percentage = 0;

                    Framework.TestCaseIndex = 1;
                    worker.ReportProgress(Framework.Percentage);

                    foreach (ExcelTest test in Framework.Process.TestCases)
                    {
                        if (worker.CancellationPending == true)
                        {
                            e.Cancel = true;
                            break;
                        }
                        else
                        {
                            if (Framework.PutTestCase(test))
                            {
                                Framework.TestCaseIndex++;
                            }
                            else
                            {
                                worker.CancelAsync();
                                statusform.Close();
                                System.Windows.Forms.MessageBox.Show("Error uploading testcase " + test.Name);
                            }

                            Framework.Percentage = Framework.Percentage + percentage;
                            worker.ReportProgress(Framework.Percentage);
                        }
                    }
                }
            }

            if (Framework.Percentage < 100)
            {
                worker.CancelAsync();
                statusform.Close();
                System.Windows.Forms.MessageBox.Show("Error preparing upload.\n");
            }
        }

        // This event handler updates the progress.
        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string strMessage = "";
            // Show the progress in main form (GUI)
            strMessage = (Framework.Percentage.ToString() + "%");
            // Pass the progress to AlertForm label and progressbar
            statusform.Message = "Uploading in progress, please wait... " + Framework.Percentage.ToString() + "%";
            statusform.ProgressValue = Framework.Percentage;
            statusform.Refresh();
        }

        // This event handler deals with the results of the background operation.
        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            if (e.Cancelled == true)
            {
                System.Windows.Forms.MessageBox.Show("Uploading canceled.");
            }
            else if (e.Error != null)
            {
                System.Windows.Forms.MessageBox.Show("Error occured during uploading.\n" + e.Result);
            }
            else
            {
                if (Framework.UploadTemplate())
                {
                    statusform.Message = "Finished uploading " + Framework.Process.TestCases.Count.ToString() + "";
                }
                else
                {
                    statusform.Message = "Finished uploading " + Framework.Process.TestCases.Count.ToString() + " Could not upload current workbook.";
                }


                statusform.btnCancel.Enabled = false;
                statusform.ProgressValue = 100;
                statusform.btnOK.Enabled = true;
                statusform.Refresh();
            }
        }




        public bool UploadTestCases()
        {
            bool blnResult = false;

            return blnResult;
        }

        /// </summary>
        /// <param name="testcase"></param>
        /// <returns></returns>

        public bool UploadTestCase(ExcelTest test)
        {
            bool blnResult = false;
                                   
            //if (Framework.CreateDescription(testdata))
            //{
                if (Framework.UploadTestCase(test))
                {
                    blnResult = true;
                }
            //}

            return blnResult;
        }
        
      
    }
}
