using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Excel;

using CoreBank.IBAN;

namespace CoreBank
{
    public partial class frmBanks : Form
    {
        public List<BIC> Banks = new List<BIC>();
        
        public frmBanks()
        {
            ReadBIC();
            InitializeComponent();
            SetList();
        }

        private void cbBanks_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void SetList()
        {           
            this.cbBanks.DataSource = Banks;

            //this.cbBanks.Items.AddRange(Banks.ToArray<string>());
            this.cbBanks.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.cbBanks.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.cbBanks.DisplayMember = "Name";
            this.cbBanks.SelectedText = "ING BANK";
            this.cbBanks.ValueMember = "IDNumber";
        }

        private void ReadBIC()
        {
            Assembly _assembly;
            StreamReader _textStreamReader;
            this.Banks = new List<BIC>();

            try
            {
                _assembly = Assembly.GetExecutingAssembly();
                _textStreamReader = new StreamReader(_assembly.GetManifestResourceStream("CoreBank.BIC.txt"));

                while (_textStreamReader.Peek() >= 0)
                {
                    string strBIC = (_textStreamReader.ReadLine());
                    char cSplit = ';';
                    string[] arrBIC = strBIC.Split(cSplit);

                    if (arrBIC.Length == 3)
                    {
                        BIC bic = new BIC(arrBIC[0], arrBIC[1], arrBIC[2]);
                        this.Banks.Add(bic);
                    }
                }

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error reading BIC.txt");
            }

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();

            AccountNumber.Convert = true;
            AccountNumber.BIC = (BIC)this.cbBanks.SelectedItem;
            AccountNumber.BankNumber = (string)this.cbBanks.SelectedValue;
            AccountNumber.BankCode = "code";
            AccountNumber.ConvertToIBAN();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            AccountNumber.Convert = false;
            this.Close();
        }

        private void frmBanks_Load(object sender, EventArgs e)
        {

        }

    }
}
