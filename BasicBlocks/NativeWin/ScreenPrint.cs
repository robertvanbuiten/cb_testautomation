using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace CoreBank
{
    public static class ScreenPrint
    {
        public static Bitmap Create(IntPtr hDesk)
        {
            Bitmap bmp = null;

            if (!string.IsNullOrEmpty(hDesk.ToString()))
            {
                // Get window info: size, x, y
                WINDOWINFO info = new WINDOWINFO();
                info.cbSize = (uint)Marshal.SizeOf(info);
                NativeWin32.GetWindowInfo(hDesk, ref info);
                Rectangle rect = new Rectangle((int)info.rcWindow.Left, (int)info.rcWindow.Top, Math.Abs((int)info.rcWindow.Left - (int)info.rcWindow.Right), Math.Abs((int)info.rcWindow.Top - (int)info.rcWindow.Bottom));

                // Set pointers
                IntPtr hSrce = NativeWin32.GetWindowDC(hDesk);
                IntPtr hDest = NativeWin32.CreateCompatibleDC(hSrce);
                IntPtr hBmp = NativeWin32.CreateCompatibleBitmap(hSrce, rect.Width, rect.Height);
                IntPtr hOldBmp = NativeWin32.SelectObject(hDest, hBmp);

                //  Create bitmap
                bmp = new Bitmap(rect.Width, rect.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                Graphics gfxScreenShot = Graphics.FromImage(bmp);
                gfxScreenShot.CopyFromScreen(rect.X, rect.Y, 0, 0, rect.Size, CopyPixelOperation.SourceCopy);

            }
            else
            {
                // logging    
            }

            return bmp;
        }
    }
}
