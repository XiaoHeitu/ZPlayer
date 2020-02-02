using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Win32Api
    {
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;

        /// <summary> 
        /// Enumeration for the raster operations used in BitBlt. 
        /// In C++ these are actually #define. But to use these 
        /// constants with C#, a new enumeration type is defined. 
        /// </summary> 
        public enum TernaryRasterOperations
        {
            SRCCOPY = 0x00CC0020, /* dest = source */
            SRCPAINT = 0x00EE0086, /* dest = source OR dest */
            SRCAND = 0x008800C6, /* dest = source AND dest */
            SRCINVERT = 0x00660046, /* dest = source XOR dest */
            SRCERASE = 0x00440328, /* dest = source AND (NOT dest ) */
            NOTSRCCOPY = 0x00330008, /* dest = (NOT source) */
            NOTSRCERASE = 0x001100A6, /* dest = (NOT src) AND (NOT dest) */
            MERGECOPY = 0x00C000CA, /* dest = (source AND pattern) */
            MERGEPAINT = 0x00BB0226, /* dest = (NOT source) OR dest */
            PATCOPY = 0x00F00021, /* dest = pattern */
            PATPAINT = 0x00FB0A09, /* dest = DPSnoo */
            PATINVERT = 0x005A0049, /* dest = pattern XOR dest */
            DSTINVERT = 0x00550009, /* dest = (NOT dest) */
            BLACKNESS = 0x00000042, /* dest = BLACK */
            WHITENESS = 0x00FF0062, /* dest = WHITE */
        };

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        [DllImport("GDI32.dll")]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        [DllImport("GDI32.dll")]
        public static extern bool DeleteObject(IntPtr objectHandle);

        [DllImport("gdi32.dll", ExactSpelling = true)]
        public static extern bool BitBlt(
            IntPtr hdcDest, // 目标设备的句柄
            int nXDest, // 目标对象的左上角的X坐标
            int nYDest, // 目标对象的左上角的Y坐标
            int nWidth, // 目标对象的矩形的宽度
            int nHeight, // 目标对象的矩形的长度
            IntPtr hdcSrc, // 源设备的句柄
            int nXSrc, // 源对象的左上角的X坐标
            int nYSrc, // 源对象的左上角的X坐标
            TernaryRasterOperations dwRop // 光栅的操作值
            );
    }
}
