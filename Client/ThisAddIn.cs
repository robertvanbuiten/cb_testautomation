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
using CoreBank.IBAN;

namespace CoreBank.Client
{
    public partial class ThisAddIn
    {
        // Menu
        private Office.CommandBar cb;
        private Office.CommandBarPopup menu;

        private Office.CommandBarPopup menu_config;
        private Office.CommandBarPopup menu_alm;
        private Office.CommandBarPopup menu_iban;
        private Office.CommandBarPopup menu_process;
        private Office.CommandBarPopup menu_chain;

        // Buttons
        private Office.CommandBarButton btnIban;
        private Office.CommandBarButton btnNLBank;
        private string lblIban = "Convert to ING IBAN";
        private string lblBanks = "Convert to other NL Bank IBAN";

        private Office.CommandBarButton btnConfig;
        private Office.CommandBarButton btnExport;
        private Office.CommandBarButton btnConnect;
        private Office.CommandBarButton btnUploadConfig;
        private Office.CommandBarButton btnUpload;
        private Office.CommandBarButton btnManage;
        private Office.CommandBarButton btnUploadProcess;

        private Office.CommandBarButton btnChainRead;
        private Office.CommandBarButton btnChainUpload;

        private Office.CommandBarButton btnCheck;
        private Office.CommandBarButton btnFill;
        private Office.CommandBarButton btnText;
        private Office.CommandBarButton btnClick;
        private Office.CommandBarButton btnScreen;

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

                        if (popup.Caption == "Test Automation" || popup.Caption == "Object repository" || popup.Caption == "QTP Framework" || popup.Caption == "IBAN")
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
            btnConnect.TooltipText = "Connect";
            btnConnect.Click += new Office._CommandBarButtonEvents_ClickEventHandler(btnConnect_Click);

            menu_process = (Office.CommandBarPopup)menu_alm.Controls.Add(Office.MsoControlType.msoControlPopup, missing, missing, missing, missing);
            menu_process.Caption = "Process";

            menu_chain = (Office.CommandBarPopup)menu_alm.Controls.Add(Office.MsoControlType.msoControlPopup, missing, missing, missing, missing);
            menu_chain.Caption = "Chain";

            //menu_iban = (Office.CommandBarPopup)menu.Controls.Add(Office.MsoControlType.msoControlPopup, missing, missing, missing, missing);
            //menu_iban.Caption = "IBAN";

            //btnIban = (Office.CommandBarButton)menu_iban.Controls.Add(1, missing, missing, missing, missing);
            //btnIban.TooltipText = "Convert ING";
            //btnIban.Caption = lblIban;
            //btnIban.Click += btnIban_Click;

           // btnNLBank = (Office.CommandBarButton)menu_iban.Controls.Add(1, missing, missing, missing, missing);
            //btnNLBank.TooltipText = "Convert other NL Bank";
           // btnNLBank.Caption = lblBanks;
            //btnNLBank.Click += btnNLBank_Click;

            

            if (Framework.Connected)
            {
                btnConnect.Caption = "Disconnect";
            }
            else
            {
                btnConnect.Caption = "Connect";
            }

                //btnUploadProcess = (Office.CommandBarButton)menu_process.Controls.Add(1, missing, missing, missing, missing);
                //btnUploadProcess.TooltipText = "Upload Workbook";
                //btnUploadProcess.Caption = "Upload Workbook";
                //btnUploadProcess.Click += new Office._CommandBarButtonEvents_ClickEventHandler(btnUploadProcess_Click);

                btnUpload = (Office.CommandBarButton)menu_process.Controls.Add(1, missing, missing, missing, missing);
                btnUpload.TooltipText = "Upload Testcases";
                btnUpload.Caption = "Upload Testcase";
                btnUpload.Click += btnUpload_Click;

                btnChainRead = (Office.CommandBarButton)menu_chain.Controls.Add(1, missing, missing, missing, missing);
                btnChainRead.Caption = "Read Basicflows / testcases";
                btnChainRead.Click += btnChainRead_Click;

                btnChainUpload = (Office.CommandBarButton)menu_chain.Controls.Add(1, missing, missing, missing, missing);
                btnChainUpload.Caption = "Upload test set";
                btnChainUpload.Click += btnChainUpload_Click;

                btnUploadConfig = (Office.CommandBarButton)menu.Controls.Add(1, missing, missing, missing, missing);
                btnUploadConfig.TooltipText = "Repository Manager";
                btnUploadConfig.Caption = "Repository Manager";
                btnUploadConfig.Click += new Office._CommandBarButtonEvents_ClickEventHandler(btnConfig_Click);
            
            ///
            btnCheck = (Office.CommandBarButton)menu.Controls.Add(1, missing, missing, missing, missing);
            btnCheck.TooltipText = "check a value of a screen object";
            btnCheck.Caption = ACTION.CHECK.ToString().ToLower();
            btnCheck.Click += button_Click;

            ///
            btnFill = (Office.CommandBarButton)menu.Controls.Add(1, missing, missing, missing, missing);
            btnFill.TooltipText = "fill a value in a screen object";
            btnFill.Caption = ACTION.FILL.ToString().ToLower();
            btnFill.Click += button_Click;

            ///
            btnText = (Office.CommandBarButton)menu.Controls.Add(1, missing, missing, missing, missing);
            btnText.TooltipText = "Check text on a screen";
            btnText.Caption = ACTION.TEXT.ToString().ToLower();
            btnText.Click += button_Click;

            ///
            btnClick = (Office.CommandBarButton)menu.Controls.Add(1, missing, missing, missing, missing);
            btnClick.TooltipText = "Click on a screen object";
            btnClick.Caption = ACTION.CLICK.ToString().ToLower();
            btnClick.Click += button_Click;
        }

        void btnChainUpload_Click(Office.CommandBarButton Ctrl, ref bool CancelDefault)
        {
            throw new NotImplementedException();
        }

        void btnChainRead_Click(Office.CommandBarButton Ctrl, ref bool CancelDefault)
        {
            throw new NotImplementedException();
        }

        void btnNLBank_Click(Office.CommandBarButton Ctrl, ref bool CancelDefault)
        {
            Excel.Range selection = (Excel.Range)Application.Selection;
            AccountNumber.Range = selection;

            frmBanks forms = new frmBanks();
            forms.Show();
        }

        void btnIban_Click(Office.CommandBarButton Ctrl, ref bool CancelDefault)
        {
     
            Excel.Range selection = (Excel.Range)Application.Selection;
            AccountNumber.Range = selection;

            AccountNumber.BIC = new BIC("INGBNL2A", "INGB", "ING BANK");
            AccountNumber.ConvertToIBAN();    
        }

        void btnExport_Click(Office.CommandBarButton Ctrl, ref bool CancelDefault)
        {

        }


        void btnConfig_Click(Office.CommandBarButton Ctrl, ref bool CancelDefault)
        {

        }


        private void InitiateFramework()
        {
            Framework.Factory();
            Framework.Start();
            AccountNumber.Factory();
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
