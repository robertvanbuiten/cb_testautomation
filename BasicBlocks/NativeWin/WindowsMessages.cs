using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS._NativeWin
{
    public class WindowsMessages
    {
         private static WindowsMessages singleton;

         public static Dictionary<string,uint> messages = new Dictionary<string,uint>();

        /// <summary>
        /// 
        /// </summary>

        private WindowsMessages()
        {
            this.setMessages();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        public static WindowsMessages factory()
        {
            if (WindowsMessages.singleton == null)
            {
                WindowsMessages.singleton = new WindowsMessages();
            }
            return WindowsMessages.singleton;
        }

        private void setMessages()
        {
            WindowsMessages.messages.Add("WM_NULL", 0x00);
            WindowsMessages.messages.Add("WM_CREATE",0x01);
            WindowsMessages.messages.Add("WM_DESTROY",0x02);
            WindowsMessages.messages.Add("WM_MOVE",0x03);
            WindowsMessages.messages.Add("WM_SIZE",0x05);
            WindowsMessages.messages.Add("WM_ACTIVATE",0x06);
            WindowsMessages.messages.Add("WM_SETFOCUS",0x07);
            WindowsMessages.messages.Add("WM_KILLFOCUS",0x08);
            WindowsMessages.messages.Add("WM_ENABLE",0x0A);
            WindowsMessages.messages.Add("WM_SETREDRAW",0x0B);
            WindowsMessages.messages.Add("WM_SETTEXT",0x0C);
            WindowsMessages.messages.Add("WM_GETTEXT",0x0D);
            WindowsMessages.messages.Add("WM_GETTEXTLENGTH",0x0E);
            WindowsMessages.messages.Add("WM_PAINT",0x0F);
            WindowsMessages.messages.Add("WM_CLOSE",0x10);
            WindowsMessages.messages.Add("WM_QUERYENDSESSION",0x11);
            WindowsMessages.messages.Add("WM_QUIT",0x12);
            WindowsMessages.messages.Add("WM_QUERYOPEN",0x13);
            WindowsMessages.messages.Add("WM_ERASEBKGND",0x14);
            WindowsMessages.messages.Add("WM_SYSCOLORCHANGE",0x15);
            WindowsMessages.messages.Add("WM_ENDSESSION",0x16);
            WindowsMessages.messages.Add("WM_SYSTEMERROR",0x17);
            WindowsMessages.messages.Add("WM_SHOWWINDOW",0x18);
            WindowsMessages.messages.Add("WM_CTLCOLOR",0x19);
            WindowsMessages.messages.Add("WM_WININICHANGE",0x1A);
            WindowsMessages.messages.Add("WM_SETTINGCHANGE",0x1A);
            WindowsMessages.messages.Add("WM_DEVMODECHANGE",0x1B);
            WindowsMessages.messages.Add("WM_ACTIVATEAPP",0x1C);
            WindowsMessages.messages.Add("WM_FONTCHANGE",0x1D);
            WindowsMessages.messages.Add("WM_TIMECHANGE",0x1E);
            WindowsMessages.messages.Add("WM_CANCELMODE",0x1F);
            WindowsMessages.messages.Add("WM_SETCURSOR",0x20);
            WindowsMessages.messages.Add("WM_MOUSEACTIVATE",0x21);
            WindowsMessages.messages.Add("WM_CHILDACTIVATE",0x22);
            WindowsMessages.messages.Add("WM_QUEUESYNC",0x23);
            WindowsMessages.messages.Add("WM_GETMINMAXINFO",0x24);
            WindowsMessages.messages.Add("WM_PAINTICON",0x26);
            WindowsMessages.messages.Add("WM_ICONERASEBKGND",0x27);
            WindowsMessages.messages.Add("WM_NEXTDLGCTL",0x28);
            WindowsMessages.messages.Add("WM_SPOOLERSTATUS",0x2A);
            WindowsMessages.messages.Add("WM_DRAWITEM",0x2B);
            WindowsMessages.messages.Add("WM_MEASUREITEM",0x2C);
            WindowsMessages.messages.Add("WM_DELETEITEM",0x2D);
            WindowsMessages.messages.Add("WM_VKEYTOITEM",0x2E);
            WindowsMessages.messages.Add("WM_CHARTOITEM",0x2F);
            WindowsMessages.messages.Add("WM_SETFONT",0x30);
            WindowsMessages.messages.Add("WM_GETFONT",0x31);
            WindowsMessages.messages.Add("WM_SETHOTKEY",0x32);
            WindowsMessages.messages.Add("WM_GETHOTKEY",0x33);
            WindowsMessages.messages.Add("WM_QUERYDRAGICON",0x37);
            WindowsMessages.messages.Add("WM_COMPAREITEM",0x39);
            WindowsMessages.messages.Add("WM_COMPACTING",0x41);
            WindowsMessages.messages.Add("WM_WINDOWPOSCHANGING",0x46);
            WindowsMessages.messages.Add("WM_WINDOWPOSCHANGED",0x47);
            WindowsMessages.messages.Add("WM_POWER",0x48);
            WindowsMessages.messages.Add("WM_COPYDATA",0x4A);
            WindowsMessages.messages.Add("WM_CANCELJOURNAL",0x4B);
            WindowsMessages.messages.Add("WM_NOTIFY",0x4E);
            WindowsMessages.messages.Add("WM_INPUTLANGCHANGEREQUEST",0x50);
            WindowsMessages.messages.Add("WM_INPUTLANGCHANGE",0x51);
            WindowsMessages.messages.Add("WM_TCARD",0x52);
            WindowsMessages.messages.Add("WM_HELP",0x53);
            WindowsMessages.messages.Add("WM_USERCHANGED",0x54);
            WindowsMessages.messages.Add("WM_NOTIFYFORMAT",0x55);
            WindowsMessages.messages.Add("WM_CONTEXTMENU",0x7B);
            WindowsMessages.messages.Add("WM_STYLECHANGING",0x7C);
            WindowsMessages.messages.Add("WM_STYLECHANGED",0x7D);
            WindowsMessages.messages.Add("WM_DISPLAYCHANGE",0x7E);
            WindowsMessages.messages.Add("WM_GETICON",0x7F);
            WindowsMessages.messages.Add("WM_SETICON",0x80);
            
            WindowsMessages.messages.Add("WM_NCCREATE",0x81);
            WindowsMessages.messages.Add("WM_NCDESTROY",0x82);
            WindowsMessages.messages.Add("WM_NCCALCSIZE",0x83);
            WindowsMessages.messages.Add("WM_NCHITTEST",0x84);
            WindowsMessages.messages.Add("WM_NCPAINT",0x85);
            WindowsMessages.messages.Add("WM_NCACTIVATE",0x86);
            WindowsMessages.messages.Add("WM_GETDLGCODE",0x87);
            WindowsMessages.messages.Add("WM_NCMOUSEMOVE",0xA0);
            WindowsMessages.messages.Add("WM_NCLBUTTONDOWN",0xA1);
            WindowsMessages.messages.Add("WM_NCLBUTTONUP",0xA2);
            WindowsMessages.messages.Add("WM_NCLBUTTONDBLCLK",0xA3);
            WindowsMessages.messages.Add("WM_NCRBUTTONDOWN",0xA4);
            WindowsMessages.messages.Add("WM_NCRBUTTONUP",0xA5);
            WindowsMessages.messages.Add("WM_NCRBUTTONDBLCLK",0xA6);
            WindowsMessages.messages.Add("WM_NCMBUTTONDOWN",0xA7);
            WindowsMessages.messages.Add("WM_NCMBUTTONUP",0xA8);
            WindowsMessages.messages.Add("WM_NCMBUTTONDBLCLK",0xA9);

            WindowsMessages.messages.Add("WM_KEYFIRST",0x100);
            WindowsMessages.messages.Add("WM_KEYDOWN",0x100);
            WindowsMessages.messages.Add("WM_KEYUP",0x101);
            WindowsMessages.messages.Add("WM_CHAR",0x102);
            WindowsMessages.messages.Add("WM_DEADCHAR",0x103);
            WindowsMessages.messages.Add("WM_SYSKEYDOWN",0x104);
            WindowsMessages.messages.Add("WM_SYSKEYUP",0x105);
            WindowsMessages.messages.Add("WM_SYSCHAR",0x106);
            WindowsMessages.messages.Add("WM_SYSDEADCHAR",0x107);
            WindowsMessages.messages.Add("WM_KEYLAST",0x108);

            WindowsMessages.messages.Add("WM_IME_STARTCOMPOSITION",0x10D);
            WindowsMessages.messages.Add("WM_IME_ENDCOMPOSITION",0x10E);
            WindowsMessages.messages.Add("WM_IME_COMPOSITION",0x10F);
            WindowsMessages.messages.Add("WM_IME_KEYLAST",0x10F);

            WindowsMessages.messages.Add("WM_INITDIALOG",0x110);
            WindowsMessages.messages.Add("WM_COMMAND",0x111);
            WindowsMessages.messages.Add("WM_SYSCOMMAND",0x112);
            WindowsMessages.messages.Add("WM_TIMER",0x113);
            WindowsMessages.messages.Add("WM_HSCROLL",0x114);
            WindowsMessages.messages.Add("WM_VSCROLL",0x115);
            WindowsMessages.messages.Add("WM_INITMENU",0x116);
            WindowsMessages.messages.Add("WM_INITMENUPOPUP",0x117);
            WindowsMessages.messages.Add("WM_MENUSELECT",0x11F);
            WindowsMessages.messages.Add("WM_MENUCHAR",0x120);
            WindowsMessages.messages.Add("WM_ENTERIDLE",0x121);

            WindowsMessages.messages.Add("WM_CTLCOLORMSGBOX",0x132);
            WindowsMessages.messages.Add("WM_CTLCOLOREDIT",0x133);
            WindowsMessages.messages.Add("WM_CTLCOLORLISTBOX",0x134);
            WindowsMessages.messages.Add("WM_CTLCOLORBTN",0x135);
            WindowsMessages.messages.Add("WM_CTLCOLORDLG",0x136);
            WindowsMessages.messages.Add("WM_CTLCOLORSCROLLBAR",0x137);
            WindowsMessages.messages.Add("WM_CTLCOLORSTATIC",0x138);

            WindowsMessages.messages.Add("WM_MOUSEFIRST",0x200);
            WindowsMessages.messages.Add("WM_MOUSEMOVE",0x200);
            WindowsMessages.messages.Add("WM_LBUTTONDOWN",0x201);
            WindowsMessages.messages.Add("WM_LBUTTONUP",0x202);
            WindowsMessages.messages.Add("WM_LBUTTONDBLCLK",0x203);
            WindowsMessages.messages.Add("WM_RBUTTONDOWN",0x204);
            WindowsMessages.messages.Add("WM_RBUTTONUP",0x205);
            WindowsMessages.messages.Add("WM_RBUTTONDBLCLK",0x206);
            WindowsMessages.messages.Add("WM_MBUTTONDOWN",0x207);
            WindowsMessages.messages.Add("WM_MBUTTONUP",0x208);
            WindowsMessages.messages.Add("WM_MBUTTONDBLCLK",0x209);
            WindowsMessages.messages.Add("WM_MOUSEWHEEL",0x20A);
            WindowsMessages.messages.Add("WM_MOUSEHWHEEL",0x20E);

            WindowsMessages.messages.Add("WM_PARENTNOTIFY",0x210);
            WindowsMessages.messages.Add("WM_ENTERMENULOOP",0x211);
            WindowsMessages.messages.Add("WM_EXITMENULOOP",0x212);
            WindowsMessages.messages.Add("WM_NEXTMENU",0x213);
            WindowsMessages.messages.Add("WM_SIZING",0x214);
            WindowsMessages.messages.Add("WM_CAPTURECHANGED",0x215);
            WindowsMessages.messages.Add("WM_MOVING",0x216);
            WindowsMessages.messages.Add("WM_POWERBROADCAST",0x218);
            WindowsMessages.messages.Add("WM_DEVICECHANGE",0x219);

            WindowsMessages.messages.Add("WM_MDICREATE",0x220);
            WindowsMessages.messages.Add("WM_MDIDESTROY",0x221);
            WindowsMessages.messages.Add("WM_MDIACTIVATE",0x222);
            WindowsMessages.messages.Add("WM_MDIRESTORE",0x223);
            WindowsMessages.messages.Add("WM_MDINEXT",0x224);
            WindowsMessages.messages.Add("WM_MDIMAXIMIZE",0x225);
            WindowsMessages.messages.Add("WM_MDITILE",0x226);
            WindowsMessages.messages.Add("WM_MDICASCADE",0x227);
            WindowsMessages.messages.Add("WM_MDIICONARRANGE",0x228);
            WindowsMessages.messages.Add("WM_MDIGETACTIVE",0x229);
            WindowsMessages.messages.Add("WM_MDISETMENU",0x230);
            WindowsMessages.messages.Add("WM_ENTERSIZEMOVE",0x231);
            WindowsMessages.messages.Add("WM_EXITSIZEMOVE",0x232);
            WindowsMessages.messages.Add("WM_DROPFILES",0x233);
            WindowsMessages.messages.Add("WM_MDIREFRESHMENU",0x234);

            WindowsMessages.messages.Add("WM_IME_SETCONTEXT",0x281);
            WindowsMessages.messages.Add("WM_IME_NOTIFY",0x282);
            WindowsMessages.messages.Add("WM_IME_CONTROL",0x283);
            WindowsMessages.messages.Add("WM_IME_COMPOSITIONFULL",0x284);
            WindowsMessages.messages.Add("WM_IME_SELECT",0x285);
            WindowsMessages.messages.Add("WM_IME_CHAR",0x286);
            WindowsMessages.messages.Add("WM_IME_KEYDOWN",0x290);
            WindowsMessages.messages.Add("WM_IME_KEYUP",0x291);

            WindowsMessages.messages.Add("WM_MOUSEHOVER",0x2A1);
            WindowsMessages.messages.Add("WM_NCMOUSELEAVE",0x2A2);
            WindowsMessages.messages.Add("WM_MOUSELEAVE",0x2A3);

            WindowsMessages.messages.Add("WM_CUT",0x300);
            WindowsMessages.messages.Add("WM_COPY",0x301);
            WindowsMessages.messages.Add("WM_PASTE",0x302);
            WindowsMessages.messages.Add("WM_CLEAR",0x303);
            WindowsMessages.messages.Add("WM_UNDO",0x304);

            WindowsMessages.messages.Add("WM_RENDERFORMAT",0x305);
            WindowsMessages.messages.Add("WM_RENDERALLFORMATS",0x306);
            WindowsMessages.messages.Add("WM_DESTROYCLIPBOARD",0x307);
            WindowsMessages.messages.Add("WM_DRAWCLIPBOARD",0x308);
            WindowsMessages.messages.Add("WM_PAINTCLIPBOARD",0x309);
            WindowsMessages.messages.Add("WM_VSCROLLCLIPBOARD",0x30A);
            WindowsMessages.messages.Add("WM_SIZECLIPBOARD",0x30B);
            WindowsMessages.messages.Add("WM_ASKCBFORMATNAME",0x30C);
            WindowsMessages.messages.Add("WM_CHANGECBCHAIN",0x30D);
            WindowsMessages.messages.Add("WM_HSCROLLCLIPBOARD",0x30E);
            WindowsMessages.messages.Add("WM_QUERYNEWPALETTE",0x30F);
            WindowsMessages.messages.Add("WM_PALETTEISCHANGING",0x310);
            WindowsMessages.messages.Add("WM_PALETTECHANGED",0x311);

            WindowsMessages.messages.Add("WM_HOTKEY",0x312);
            WindowsMessages.messages.Add("WM_PRINT",0x317);
            WindowsMessages.messages.Add("WM_PRINTCLIENT",0x318);

            WindowsMessages.messages.Add("WM_HANDHELDFIRST",0x358);
            WindowsMessages.messages.Add("WM_HANDHELDLAST",0x35F);
            WindowsMessages.messages.Add("WM_PENWINFIRST",0x380);
            WindowsMessages.messages.Add("WM_PENWINLAST",0x38F);
            WindowsMessages.messages.Add("WM_COALESCE_FIRST",0x390);
            WindowsMessages.messages.Add("WM_COALESCE_LAST",0x39F);
            WindowsMessages.messages.Add("WM_DDE_FIRST",0x3E0);
            WindowsMessages.messages.Add("WM_DDE_INITIATE",0x3E0);
            WindowsMessages.messages.Add("WM_DDE_TERMINATE",0x3E1);
            WindowsMessages.messages.Add("WM_DDE_ADVISE",0x3E2);
            WindowsMessages.messages.Add("WM_DDE_UNADVISE",0x3E3);
            WindowsMessages.messages.Add("WM_DDE_ACK",0x3E4);
            WindowsMessages.messages.Add("WM_DDE_DATA",0x3E5);
            WindowsMessages.messages.Add("WM_DDE_REQUEST",0x3E6);
            WindowsMessages.messages.Add("WM_DDE_POKE",0x3E7);
            WindowsMessages.messages.Add("WM_DDE_EXECUTE",0x3E8);
            WindowsMessages.messages.Add("WM_DDE_LAST",0x3E8);

            WindowsMessages.messages.Add("WM_USER",0x400);
            WindowsMessages.messages.Add("WM_APP",0x8000);

        }

        /// <summary>
        /// Check windows message
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>

        public static uint checkMessage(string message)
        {
            
            foreach(string key in messages.Keys)
            {
                try
                {
                    if (key.ToUpper() == message.ToUpper())
                    {
                        return messages[key];
                    }
                }
                catch
                {
                    return 0;
                }
            }

            return 0;
        }

    }
}
