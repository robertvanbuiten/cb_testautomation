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

using CoreBank;

namespace CoreBank.Client
{
    public partial class ThisAddIn
    {
        // Menu
        private Office.CommandBar cb;
        private Office.CommandBarPopup menu;

        private Office.CommandBarPopup menu_config;
        private Office.CommandBarPopup menu_alm;

        // Buttons
        private Office.CommandBarButton btnConfig;
        private Office.CommandBarButton btnExport;
        private Office.CommandBarButton btnConnect;
        private Office.CommandBarButton btnUploadConfig;
        private Office.CommandBarButton btnUpload;
        private Office.CommandBarButton btnManage;
        private Office.CommandBarButton btnUploadProcess;

        private Office.CommandBarButton btnCheck;
        private Office.CommandBarButton btnFill;
        private Office.CommandBarButton btnText;
        private Office.CommandBarButton btnClick;
        private Office.CommandBarButton btnScreen;

        private Status statusform;
        private BackgroundWorker background;

        private ConnectionSettings conn;
        private Paths paths;

        private bool connected;

        private void RemoveMenu()
        {
            try
            {
                cb = Application.CommandBars["Cell"];

                foreach (Office.CommandBarControl ctrl in cb.Controls)
                {
                    if (ctrl is Office.CommandBarPopup)
                    {
                        Office.CommandBarPopup popup = ctrl as Office.CommandBarPopup;

                        if (popup.Caption == "Test Automation" || popup.Caption == "Object repository" || popup.Caption == "QTP Framework")
                        {
                            popup.Delete();
                        }
                    }
                    if (ctrl is Office.CommandBarButton)
                    {
                        Office.CommandBarButton btn = ctrl as Office.CommandBarButton;

                        if (btn.Caption == ACTION.CHECK.ToString() || btn.Caption == ACTION.FILL.ToString() || btn.Caption == ACTION.CLICK.ToString() || btn.Caption == ACTION.TEXT.ToString())
                        {
                            btn.Delete();
                        }
                        else if (btn.Caption == "Upload testcases to ALM" || btn.Caption == "Upload Framework Configuration")
                        {
                            btn.Delete();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private void AddMenu()
        {
            cb = Application.CommandBars["Cell"];

            menu = (Office.CommandBarPopup)cb.Controls.Add(Office.MsoControlType.msoControlPopup, missing, missing, missing, missing);
            menu.Caption = "Test Automation";

            menu_alm = (Office.CommandBarPopup)menu.Controls.Add(Office.MsoControlType.msoControlPopup, missing, missing, missing, missing);
            menu_alm.Caption = "Repository";

            btnConnect = (Office.CommandBarButton)menu_alm.Controls.Add(1, missing, missing, missing, missing);
            btnConnect.TooltipText = "Connect to " + conn.Repository;
            btnConnect.Click += new Office._CommandBarButtonEvents_ClickEventHandler(btnConnect_Click);

            if (Framework.Connected)
            {
                btnConnect.Caption = "Disconnect";

                btnUploadProcess = (Office.CommandBarButton)menu_alm.Controls.Add(1, missing, missing, missing, missing);
                btnUploadProcess.TooltipText = "Upload Workbook";
                btnUploadProcess.Caption = "Upload Workbook";
                btnUploadProcess.Click += new Office._CommandBarButtonEvents_ClickEventHandler(btnUploadProcess_Click);

                btnUpload = (Office.CommandBarButton)menu_alm.Controls.Add(1, missing, missing, missing, missing);
                btnUpload.TooltipText = "Upload Testcases";
                btnUpload.Caption = "Upload Testcase";
                btnUpload.Click += btnUpload_Click;

                btnUploadConfig = (Office.CommandBarButton)menu_alm.Controls.Add(1, missing, missing, missing, missing);
                btnUploadConfig.TooltipText = "Repository Manager";
                btnUploadConfig.Caption = "Repository Manager";
                btnUploadConfig.Click += new Office._CommandBarButtonEvents_ClickEventHandler(btnConfig_Click);
            
            }
            else
            {
                btnConnect.Caption = "Connect to " + conn.Repository;
            }

            ///
            btnCheck = (Office.CommandBarButton)menu.Controls.Add(1, missing, missing, missing, missing);
            btnCheck.TooltipText = "check a value of a screen object";
            btnCheck.Caption = ACTION.CHECK.ToString();
            btnCheck.Click += button_Click;

            ///
            btnFill = (Office.CommandBarButton)menu.Controls.Add(1, missing, missing, missing, missing);
            btnFill.TooltipText = "fill a value in a screen object";
            btnFill.Caption = ACTION.FILL.ToString();
            btnFill.Click += button_Click;

            ///
            btnText = (Office.CommandBarButton)menu.Controls.Add(1, missing, missing, missing, missing);
            btnText.TooltipText = "Check text on a screen";
            btnText.Caption = ACTION.TEXT.ToString(); ;
            btnText.Click += button_Click;

            ///
            btnClick = (Office.CommandBarButton)menu.Controls.Add(1, missing, missing, missing, missing);
            btnClick.TooltipText = "Click on a screen object";
            btnClick.Caption = ACTION.CLICK.ToString(); ;
            btnClick.Click += button_Click;

            ///
            btnScreen = (Office.CommandBarButton)menu.Controls.Add(1, missing, missing, missing, missing);
            btnScreen.TooltipText = "Screen";
            btnScreen.Caption = "Screen";
            btnScreen.Click += button_Click;
        }

        void btnExport_Click(Office.CommandBarButton Ctrl, ref bool CancelDefault)
        {

        }

        void btnManage_Click(Office.CommandBarButton Ctrl, ref bool CancelDefault)
        {

        }

        void btnConnect_Click(Office.CommandBarButton Ctrl, ref bool CancelDefault)
        {
            Framework.Start(conn, paths);

            if (Framework.Ready)
            {
                Framework.Connect();
                
                if (Framework.Connected)
                {
                    Framework.ReadRepository();
                }
            }

            RemoveMenu();
            AddMenu();
        }

        void btnConfig_Click(Office.CommandBarButton Ctrl, ref bool CancelDefault)
        {
            // show repository manager GUI

        }



        /// <summary>
        /// 
        /// </summary>

        private void ShowSplashScreen()
        {

        }

        private void InitiateFramework()
        {
            conn = new ConnectionSettings();
            paths = new Paths();

            Framework.Factory();
        }

        void button_Click(Office.CommandBarButton Ctrl, ref bool CancelDefault)
        {
            try
            {
                if (Ctrl.Caption.ToString() == ACTION.CHECK.ToString())
                {
                    Application.Selection.Font.Color = COLORS.lngClBlack;
                }
                else if (Ctrl.Caption.ToString() == ACTION.CLICK.ToString())
                {
                    Application.Selection.Font.Color = COLORS.lngClRed;
                }
                else if (Ctrl.Caption.ToString() == ACTION.FILL.ToString())
                {
                    Application.Selection.Font.Color = COLORS.lngClBlue;
                }
                else if (Ctrl.Caption.ToString() == ACTION.TEXT.ToString())
                {
                    Application.Selection.Font.Color = COLORS.lngClPink;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            // show splash screen
            // add right mouse button
            // add ribbon

            this.background = new BackgroundWorker();
            this.background.WorkerReportsProgress = true;
            this.background.WorkerSupportsCancellation = true;
            this.background.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.background.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            this.background.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);

            ShowSplashScreen();
            InitiateFramework();
            AddMenu();
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
            RemoveMenu();
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }

        #endregion
    }
}
