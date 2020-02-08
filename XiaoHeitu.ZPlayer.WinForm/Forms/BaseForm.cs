using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using XiaoHeitu.ZPlayer.WinForm.Apis;
using XiaoHeitu.ZPlayer.WinForm.Properties;

namespace XiaoHeitu.ZPlayer.WinForm.Forms
{
    public class BaseForm : Form
    {
        public BaseForm()
        {
            this.DoubleBuffered = true;//设置本窗体
            this.ResizeRedraw = true;

            this.SetStyle(ControlStyles.Opaque, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            this.SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲
            this.SetStyle(ControlStyles.Opaque, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
          
        }

        protected override void WndProc(ref Message m)
        {
            //Console.WriteLine($"Window Message: 0x{m.Msg.ToString("X4")},HWnd: {m.HWnd}");
            switch (m.Msg)
            {
                case Win32Api.WM_NCHITTEST:
                    {
                        base.WndProc(ref m);
                        Point vPoint = new Point((int)m.LParam & 0xFFFF,
                            (int)m.LParam >> 16 & 0xFFFF);
                        vPoint = this.PointToClient(vPoint);
                        if (vPoint.X <= 5)
                            if (vPoint.Y <= 5)
                                m.Result = (IntPtr)Win32Api.HTTOPLEFT;
                            else if (vPoint.Y >= this.ClientSize.Height - 5)
                                m.Result = (IntPtr)Win32Api.HTBOTTOMLEFT;
                            else m.Result = (IntPtr)Win32Api.HTLEFT;
                        else if (vPoint.X >= this.ClientSize.Width - 5)
                            if (vPoint.Y <= 5)
                                m.Result = (IntPtr)Win32Api.HTTOPRIGHT;
                            else if (vPoint.Y >= this.ClientSize.Height - 5)
                                m.Result = (IntPtr)Win32Api.HTBOTTOMRIGHT;
                            else m.Result = (IntPtr)Win32Api.HTRIGHT;
                        else if (vPoint.Y <= 5)
                            m.Result = (IntPtr)Win32Api.HTTOP;
                        else if (vPoint.Y >= this.ClientSize.Height - 5)
                            m.Result = (IntPtr)Win32Api.HTBOTTOM;

                        break;
                    }
                case Win32Api.WM_LBUTTONDOWN://鼠标左键按下的消息 
                    {
                        //m.Msg = 0x00A1;//更改消息为非客户区按下鼠标 
                        //m.LParam = IntPtr.Zero;//默认值 
                        //m.WParam = new IntPtr(2);//鼠标放在标题栏内 
                        //以下做了一些修正，确保放大缩小按钮区域可以正常使用

                        //Point point = Control.MousePosition;
                        //point = this.PointToClient(point);
                        //if (point.X < this.Width - 100 && point.Y < 30)
                        //{
                        //    m.Msg = 0x00A1;//更改消息为非客户区按下鼠标
                        //    m.LParam = IntPtr.Zero;//默认值
                        //    m.WParam = new IntPtr(2);//鼠标放在标题栏内

                        //    base.WndProc(ref m);
                        //    break;
                        //}

                        Point mousePoint = this.PointToClient(Control.MousePosition);
                        Rectangle dragRectangle = new Rectangle(5, 5, this.ClientSize.Width - 10, this.ClientSize.Height - 10);
                        if (dragRectangle.Contains(mousePoint))
                        {
                            m.Msg = Win32Api.WM_SYSCOMMAND;
                            m.WParam = (IntPtr)(Win32Api.SC_MOVE + Win32Api.HTCAPTION);
                            m.LParam = IntPtr.Zero;
                        }

                        base.WndProc(ref m);
                        break;
                    }
                //case Win32Api.WM_PAINT:
                //    {
                //        this.Repaint();
                //        break;
                //    }
                default:
                    base.WndProc(ref m);
                    break;
            }
        }
    }
}
