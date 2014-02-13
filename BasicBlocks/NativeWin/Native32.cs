using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections;
using System.Drawing;

namespace CoreBank
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    [StructLayout(LayoutKind.Sequential)]
    public struct WINDOWINFO
    {
        public uint cbSize;
        public RECT rcWindow;
        public RECT rcClient;
        public uint dwStyle;
        public uint dwExStyle;
        public uint dwWindowStatus;
        public uint cxWindowBorders;
        public uint cyWindowBorders;
        public ushort atomWindowType;
        public ushort wCreatorVersion;

        public WINDOWINFO(Boolean? filler)
            : this()   // Allows automatic initialization of "cbSize" with "new WINDOWINFO(null/true/false)".
        {
            cbSize = (UInt32)(Marshal.SizeOf(typeof(WINDOWINFO)));
        }

    }

    /// <summary>
    /// 
    /// </summary>

    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {

        private int _Left;
        private int _Top;
        private int _Right;
        private int _Bottom;

        public RECT(RECT Rectangle) : this(Rectangle.Left, Rectangle.Top, Rectangle.Right, Rectangle.Bottom)
        {
        }

        public RECT(int Left, int Top, int Right, int Bottom)
        {
            _Left = Left;
            _Top = Top;
            _Right = Right;
            _Bottom = Bottom;
        }

        public int X {
            get { return _Left; }
            set { _Left = value; }
        }
        public int Y {
            get { return _Top; }
            set { _Top = value; }
        }
        public int Left {
            get { return _Left; }
            set { _Left = value; }
        }
        public int Top {
            get { return _Top; }
            set { _Top = value; }
        }
        public int Right {
            get { return _Right; }
            set { _Right = value; }
        }
        public int Bottom {
            get { return _Bottom; }
            set { _Bottom = value; }
        }
        public int Height {
            get { return _Bottom - _Top; }
            set { _Bottom = value - _Top; }
        }
        public int Width {
            get { return _Right - _Left; }
            set { _Right = value + _Left; }
        }
        public Point Location {
            get { return new System.Drawing.Point(Left, Top); }
            set {
                _Left = value.X;
                _Top = value.Y;
            }
        }
        public System.Drawing.Size Size {
            get { return new System.Drawing.Size(Width, Height); }
            set {
                _Right = value.Width + _Left;
                _Bottom = value.Height + _Top;
            }
        }

        public static implicit operator Rectangle(RECT Rectangle)
        {
            return new Rectangle(Rectangle.Left, Rectangle.Top, Rectangle.Width, Rectangle.Height);
        }
        public static implicit operator RECT(Rectangle Rectangle)
        {
            return new RECT(Rectangle.Left, Rectangle.Top, Rectangle.Right, Rectangle.Bottom);
        }
        public static bool operator ==(RECT Rectangle1, RECT Rectangle2)
        {
            return Rectangle1.Equals(Rectangle2);
        }
        public static bool operator !=(RECT Rectangle1, RECT Rectangle2)
        {
            return !Rectangle1.Equals(Rectangle2);
        }

        public override string ToString()
        {
            return "{Left: " + _Left + "; " + "Top: " + _Top + "; Right: " + _Right + "; Bottom: " + _Bottom + "}";
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public bool Equals(RECT Rectangle)
        {
            return Rectangle.Left == _Left && Rectangle.Top == _Top && Rectangle.Right == _Right && Rectangle.Bottom == _Bottom;
        }

        public override bool Equals(object Object)
        {
            if (Object is RECT) {
                return Equals((RECT)Object);
            } else if (Object is Rectangle) {
                return Equals(new RECT((Rectangle)Object));
            }

            return false;
        }

    }

        
     public class NativeWin32
     {
          public const int WM_SYSCOMMAND = 0x0112;
          public const int SC_CLOSE = 0xF060;
  
          [DllImport("user32.dll")]
          public static extern int FindWindow(
              string lpClassName, // class name 
              string lpWindowName // window name 
          );
  
          [DllImport("user32.dll")]
          public static extern int SendMessage(
              int hWnd, // handle to destination window 
              uint Msg, // message 
              int wParam, // first message parameter 
              int lParam // second message parameter 
          );
 
         [DllImport("user32.dll")]
          public static extern int SetForegroundWindow(
               IntPtr hWnd // handle to window
               );
   
           private const int GWL_EXSTYLE = (-20);
           private const int WS_EX_TOOLWINDOW = 0x80;
           private const int WS_EX_APPWINDOW = 0x40000;
                 
           public const int GW_HWNDFIRST = 0;
           public const int GW_HWNDLAST  = 1;
           public const int GW_HWNDNEXT  = 2;
           public const int GW_HWNDPREV  = 3;
           public const int GW_OWNER     = 4;
           public const int GW_CHILD     = 5;

           public delegate int EnumWindowsProcDelegate(IntPtr hwnd, int lParam);
   
           [DllImport("user32")]
           public static extern int EnumWindows(EnumWindowsProcDelegate lpEnumFunc, int lParam);
   
           [DllImport("User32.Dll")]
           public static extern void GetWindowText(int h, StringBuilder s, int nMaxCount);
   
           [DllImport("user32", EntryPoint = "GetWindowLongA")]
           public static extern int GetWindowLongPtr(IntPtr hwnd, int nIndex);
   
           [DllImport("user32")]
           public static extern IntPtr GetParent(IntPtr hwnd);
   
           [DllImport("user32")]
           public static extern IntPtr GetWindow(IntPtr hwnd, int wCmd);

           [DllImport("user32.dll")]
           public static extern IntPtr GetTopWindow(IntPtr hWnd);
   
           [DllImport("user32")]
           public static extern bool IsWindowVisible(IntPtr hwnd);
   
           [DllImport("user32")]
           public static extern IntPtr GetDesktopWindow();

           [DllImport("gdi32.dll")]
           public static extern IntPtr SelectObject(IntPtr hdc, IntPtr bmp);

           [DllImport("user32.dll")]
           public static extern IntPtr GetActiveWindow();

           [DllImport("user32.dll")]
           public static extern IntPtr GetWindowDC(IntPtr ptr);

           [return: MarshalAs(UnmanagedType.Bool)]
           [DllImport("user32.dll", SetLastError = true)]
           public static extern bool GetWindowInfo(IntPtr hwnd, ref WINDOWINFO pwi);
        
           [DllImport("gdi32.dll")]
           public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);
 
           [DllImport("gdi32.dll", SetLastError = true)]
           public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

           [DllImport("gdi32.dll")]
           public static extern bool BitBlt(IntPtr hdcDest, int xDest, int yDest, int wDest, int hDest, IntPtr hdcSource, int xSrc, int ySrc, CopyPixelOperation rop);

           [DllImport("user32.dll")]
           public static extern bool CloseWindow(IntPtr hWnd);

           [DllImport("user32.dll")]
           public static extern IntPtr GetForegroundWindow();

           [DllImport("user32.dll")]
           public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

           [DllImport("user32.dll")]
           public static extern int GetWindowTextLength(IntPtr hWnd);

           [DllImport("user32.dll")]
           public static extern IntPtr GetFocus();

           [DllImport("user32.dll")]
           [return: MarshalAs(UnmanagedType.Bool)]
           public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);


           [return: MarshalAs(UnmanagedType.Bool)]
           [DllImport("user32.dll", SetLastError = true)]
           public static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

           [DllImport("user32.dll", CharSet = CharSet.Auto)]
           public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

           [DllImport("user32.dll")]
           public static extern IntPtr SetFocus(IntPtr hWnd);
           
              
       }

     class NativeWindows
     {
         [DllImport("user32")]
         [return: MarshalAs(UnmanagedType.Bool)]
         public static extern bool EnumChildWindows(IntPtr window, EnumWindowProc callback, IntPtr i);

         /// <summary>
         /// Returns a list of child windows
         /// </summary>
         /// <param name="parent">Parent of the windows to return</param>
         /// <returns>List of child windows</returns>
         public static List<IntPtr> GetChildWindows(IntPtr parent)
         {
             List<IntPtr> result = new List<IntPtr>();
             GCHandle listHandle = GCHandle.Alloc(result);
             try
             {
                 EnumWindowProc childProc = new EnumWindowProc(EnumWindow);
                 EnumChildWindows(parent, childProc, GCHandle.ToIntPtr(listHandle));
             }
             finally
             {
                 if (listHandle.IsAllocated)
                     listHandle.Free();
             }
             return result;
         }

         /// <summary>
         /// Callback method to be used when enumerating windows.
         /// </summary>
         /// <param name="handle">Handle of the next window</param>
         /// <param name="pointer">Pointer to a GCHandle that holds a reference to the list to fill</param>
         /// <returns>True to continue the enumeration, false to bail</returns>
         private static bool EnumWindow(IntPtr handle, IntPtr pointer)
         {
             GCHandle gch = GCHandle.FromIntPtr(pointer);
             List<IntPtr> list = gch.Target as List<IntPtr>;
             if (list == null)
             {
                 throw new InvalidCastException("GCHandle Target could not be cast as List<IntPtr>");
             }
             list.Add(handle);
             //  You can modify this to check to see if you want to cancel the operation, then return a null here
             return true;
         }

         /// <summary>
         /// Delegate for the EnumChildWindows method
         /// </summary>
         /// <param name="hWnd">Window handle</param>
         /// <param name="parameter">Caller-defined variable; we use it for a pointer to our list</param>
         /// <returns>True to continue enumerating, false to bail.</returns>
         public delegate bool EnumWindowProc(IntPtr hWnd, IntPtr parameter);

     }

     public class EnumDesktopWindowsDemo
     {
         const int MAXTITLE = 255;

         private static ArrayList mTitlesList;

         private delegate bool EnumDelegate(IntPtr hWnd, int lParam);

         [DllImport("user32.dll", EntryPoint = "EnumDesktopWindows",
         ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
         private static extern bool _EnumDesktopWindows(IntPtr hDesktop,
         EnumDelegate lpEnumCallbackFunction, IntPtr lParam);

         [DllImport("user32.dll", EntryPoint = "GetWindowText",
         ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
         private static extern int _GetWindowText(IntPtr hWnd,
         StringBuilder lpWindowText, int nMaxCount);

         private static bool EnumWindowsProc(IntPtr hWnd, int lParam)
         {
             string title = GetWindowText(hWnd);
             mTitlesList.Add(title);
             return true;
         }

         /// <summary>
         /// Returns the caption of a windows by given HWND identifier.
         /// </summary>
         public static string GetWindowText(IntPtr hWnd)
         {
             StringBuilder title = new StringBuilder(MAXTITLE);
             int titleLength = _GetWindowText(hWnd, title, title.Capacity + 1);
             title.Length = titleLength;

             return title.ToString();
         }

         /// <summary>
         /// Returns the caption of all desktop windows.
         /// </summary>
         public static string[] GetDesktopWindowsCaptions()
         {
             mTitlesList = new ArrayList();
             EnumDelegate enumfunc = new EnumDelegate(EnumWindowsProc);
             IntPtr hDesktop = IntPtr.Zero; // current desktop
             bool success = _EnumDesktopWindows(hDesktop, enumfunc, IntPtr.Zero);

             if (success)
             {
                 // Copy the result to string array
                 string[] titles = new string[mTitlesList.Count];
                 mTitlesList.CopyTo(titles);
                 return titles;
             }
             else
             {
                 // Get the last Win32 error code
                 int errorCode = Marshal.GetLastWin32Error();

                 string errorMessage = String.Format(
                 "EnumDesktopWindows failed with code {0}.", errorCode);
                 throw new Exception(errorMessage);
             }
         }
     }

}
