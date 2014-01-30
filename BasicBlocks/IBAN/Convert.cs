using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace CoreBank.IBAN
{
    public class Convert
    {
        public string BBAN;
        protected string strTemp;
        public string IBAN;
        protected string CellValue;
        protected string BankIDNumber;

        public bool Valid;
        public string Message;

        public Convert()
        {
            this.Valid = false;
        }

        public Convert(string strValue)
        {
            this.CellValue = strValue;
            this.Valid = false;
        }

        public bool Check()
        {
            bool blnResult = true;

            if (CellValue.Length > 10)
            {
                blnResult = false;
                Message = Message + "Cell value is longer than 10 characters." + "\n";
            }

            try
            {
                double num = double.Parse(CellValue);
            }
            catch (Exception ex)
            {
                Message = Message + "Cell value is not numeric." + "\n";
                blnResult = false;
            }

            return blnResult;
        }

        //
        //
        //

        public bool ToIBAN()
        {
            bool blnResult = true;
            
            char pad = '0';
            string strControl = "";

            BBAN = CellValue.PadLeft(10, pad);
            //strTemp = "18231611" + BBAN + "2321" + "00";
            strTemp = Common.BIC.IDNumber + BBAN + "2321" + "00";

            BigInteger number = BigInteger.Parse(strTemp);
            BigInteger outcome = number % 97;  
            BigInteger control = 98 - outcome;

            if (control <= 9)
            {
                strControl = control.ToString().PadLeft(2, pad);
            }
            else
            {
                strControl = control.ToString();
            }
            
            //IBAN = "NL" + strControl + "INGB" + BBAN;
            IBAN = "NL" + strControl + Common.BIC.ID + BBAN;

            Message = "BBAN " + BBAN + " is converted.";

            return blnResult;

        }


        


        
    }
}
