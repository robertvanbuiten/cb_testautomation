using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;

namespace CoreBank.IBAN
{
    public class Common
    {
        private static Common singleton;
        public static string BankNumber;
        public static string BankCode;
        public static bool Convert;
        public static Excel.Range Range;

        public static BIC BIC;
        private static Convert convert;

        private Common()
        {
            Common.convert = new Convert();
        }

        public static Common Factory()
        {
            if (Common.singleton == null)
            {
                Common.singleton = new Common();
            }

            return Common.singleton;
        }

        public static void ConvertToIBAN()
        {
            foreach (Excel.Range cell in Common.Range.Cells)
            {
                bool blnContinue = true;
                string strValue = "";

                try
                {
                    double num = (double)cell.Value;
                    strValue = num.ToString();
                }
                catch (Exception ex)
                {
                    try
                    {
                        cell.AddComment("Cell value is not a numeric value / accountnumber");
                    }
                    catch (Exception) { }
                    blnContinue = false;
                }

                if (blnContinue)
                {
                    convert = new Convert(strValue);

                    if (convert.Check())
                    {
                        if (convert.ToIBAN())
                        {
                            cell.Value = convert.IBAN;
                        }
                    }
                }

            }
        }


    }
    
    public class BIC
    {
        public string BankCode { get; set; }
        public string ID { get; set; }
        public string IDNumber { get; set; }
        public string Name { get; set; }

        private static string[] alphabetArray = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
                
        public BIC()
        {

        }

        public BIC(string bankcode, string id, string name)
        {
            this.BankCode = bankcode;
            if (id == "INGB")
                this.ID = id;
            this.ID = id;
            this.SetNumber();
            this.Name = name;
        }

        private void SetNumber()
        {
            this.IDNumber = "";

            foreach (char c in ID)
            {
                string letter = char.ToString(c);
                int index = Array.IndexOf(alphabetArray,letter.ToUpper()) + 10;
                this.IDNumber = this.IDNumber + index.ToString();
            }
        }
    }
}
