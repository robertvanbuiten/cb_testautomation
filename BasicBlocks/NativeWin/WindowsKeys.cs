using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreBank
{
    public class WindowsKeys
    {
        /// <summary>
        /// 
        /// </summary>

        private static WindowsKeys singleton;

        public static List<string> keys = new List<string>();

        /// <summary>
        /// 
        /// </summary>

        private WindowsKeys()
        {
            this.setKeys();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        public static WindowsKeys factory()
        {
            if (WindowsKeys.singleton == null)
            {
                WindowsKeys.singleton = new WindowsKeys();
            }
            return WindowsKeys.singleton;
        }


        private void setKeys()
        {
            //Adding capital alphbets in the 'All Keys' listbox
            for (int i = 65; i < 91; i++)
            {
                keys.Add(Convert.ToChar(i).ToString());
            }
            
            //Adding all SendKeys class supported keys in the 'All Keys' listbox
            keys.Add("{BACKSPACE}");
            keys.Add("{BREAK}");
            keys.Add("{CAPSLOCK}");
            keys.Add("{DELETE}");
            keys.Add("{DOWN}");
            keys.Add("{END}");
            keys.Add("{ENTER}");
            keys.Add("{ESC}");
            keys.Add("{HELP}");
            keys.Add("{HOME}");
            keys.Add("{INSERT}");
            keys.Add("{LEFT}");
            keys.Add("{NUMLOCK}");
            keys.Add("PGDN}");
            keys.Add("{PGUP}");
            keys.Add("{PRTSC}");
            keys.Add("{RIGHT}");
            keys.Add("{SCROLLLOCK}");
            keys.Add("{SPACE}");
            keys.Add("{TAB}");
            keys.Add("{UP}");
            keys.Add("{F1}");
            keys.Add("{F2}");
            keys.Add("{F3}");
            keys.Add("{F4}");
            keys.Add("{F5}");
            keys.Add("{F6}");
            keys.Add("{F7}");
            keys.Add("{F8}");
            keys.Add("{F9}");
            keys.Add("{F10}");
            keys.Add("{F11}");
            keys.Add("{F12}");
            keys.Add("{F13}");
            keys.Add("{F14}");
            keys.Add("{F15}");
            keys.Add("{F16}");
            keys.Add("{ADD}");
            keys.Add("{SUBTRACT}");
            keys.Add("{MULTIPLY}");
            keys.Add("{DIVIDE}");
            keys.Add("+");
            keys.Add("^");
            keys.Add("%");

            //Adding number keys in the 'All Keys' listbox
            for (int i = 0; i < 10; i++)
                keys.Add(Convert.ToString(i));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>

        public static List<string> checkKeys(string attributeName)
        {
            List<string> keys = new List<string>();
            

            return keys;
        }
    
    }


}
