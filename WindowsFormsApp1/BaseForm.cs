using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Properties;

namespace WindowsFormsApp1
{

    public class BaseForm : Form
    {
        BufferedGraphicsContext MyBufferedGraphics = new BufferedGraphicsContext();

        [Browsable(true)]
        public Padding BackgroundEdgeInset
        {
            get; set;
        } = new Padding(0, 0, 0, 0);

        private Image _actualBackground = null;
        public Image ActualBackground
        {
            get
            {
                return this._actualBackground;
            }
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            if (this.BackgroundImage == null || this.BackgroundImageLayout != ImageLayout.Stretch || this.ClientRectangle == Rectangle.Empty)
            {
                base.OnPaint(e);
                return;
            }

            BufferedGraphics MyBuffer = this.MyBufferedGraphics.Allocate(this.CreateGraphics(), this.ClientRectangle);//创建一个缓冲图像MyBuffer
            this.MyBufferedGraphics.MaximumBuffer = new Size(this.ClientRectangle.Width, this.ClientRectangle.Height);//指定缓冲图像背景类的大小 
            MyBuffer.Graphics.Clear(Color.Fuchsia);//指定图像背景色
            var image = this.ImageStretch(this.BackgroundImage, this.BackgroundEdgeInset, this.MyBufferedGraphics.MaximumBuffer);


            MyBuffer.Graphics.DrawImage(image, 0, 0);
            MyBuffer.Render();//将画好的图显示到窗口当中
            MyBuffer.Dispose();//释放资源  

            this._actualBackground = image;
        }

        private void ShowBackgroundWithBitBlt(Bitmap image)
        {
            using (Graphics grDest = Graphics.FromHwnd(this.Handle))
            using (Graphics grSrc = Graphics.FromImage(image))
            {
                IntPtr hdcDest = IntPtr.Zero;
                IntPtr hdcSrc = IntPtr.Zero;
                IntPtr hBitmap = IntPtr.Zero;
                IntPtr hOldObject = IntPtr.Zero;
                try
                {
                    hdcDest = grDest.GetHdc();
                    hdcSrc = grSrc.GetHdc();
                    hBitmap = image.GetHbitmap();
                    hOldObject = Win32Api.SelectObject(hdcSrc, hBitmap);
                    if (hOldObject == IntPtr.Zero)
                        throw new Win32Exception();
                    if (!Win32Api.BitBlt(hdcDest, 0, 0, this.Width, this.Height, hdcSrc, 0, 0, Win32Api.TernaryRasterOperations.SRCCOPY))
                        throw new Win32Exception();
                }
                finally
                {
                    if (hOldObject != IntPtr.Zero) Win32Api.SelectObject(hdcSrc, hOldObject);
                    if (hBitmap != IntPtr.Zero) Win32Api.DeleteObject(hBitmap);
                    if (hdcDest != IntPtr.Zero) grDest.ReleaseHdc(hdcDest);
                    if (hdcSrc != IntPtr.Zero) grSrc.ReleaseHdc(hdcSrc);
                }
            }
        }

        private Image ImageStretch(Image source, Padding edge, Size newSize)
        {
            Bitmap newImage = new Bitmap(newSize.Width, newSize.Height);
            var gp = Graphics.FromImage(newImage);
            gp.DrawImage(source, new Rectangle(0, 0, edge.Left, edge.Top), 0, 0, edge.Left, edge.Top, GraphicsUnit.Pixel);//左上
            gp.DrawImage(source, new Rectangle(newImage.Width - edge.Right, 0, edge.Right, edge.Top), source.Width - edge.Right, 0, edge.Right, edge.Top, GraphicsUnit.Pixel);//右上

            gp.DrawImage(source, new Rectangle(0, newImage.Height - edge.Bottom, edge.Left, edge.Bottom), 0, source.Height - edge.Bottom, edge.Left, edge.Bottom, GraphicsUnit.Pixel); //左下
            gp.DrawImage(source, new Rectangle(newImage.Width - edge.Right, newImage.Height - edge.Bottom, edge.Right, edge.Bottom), source.Width - edge.Right, source.Height - edge.Bottom, edge.Right, edge.Bottom, GraphicsUnit.Pixel); //右下

            int iTemp = source.Width - edge.Left - edge.Right;
            iTemp = iTemp > 0 ? iTemp : 1;
            gp.DrawImage(source, new Rectangle(edge.Left, 0, newImage.Width - edge.Right - edge.Left, edge.Top), edge.Right, 0, iTemp, edge.Top, GraphicsUnit.Pixel);//中上
            gp.DrawImage(source, new Rectangle(edge.Left, newImage.Height - edge.Bottom, newImage.Width - edge.Right - edge.Left, edge.Bottom), edge.Right, source.Height - edge.Bottom, iTemp, edge.Bottom, GraphicsUnit.Pixel);//中下

            iTemp = source.Height - edge.Bottom - edge.Top;
            iTemp = iTemp > 0 ? iTemp : 1;
            gp.DrawImage(source, new Rectangle(0, edge.Top, edge.Left, newImage.Height - edge.Top - edge.Bottom), 0, edge.Top, edge.Left, iTemp, GraphicsUnit.Pixel);//左中
            gp.DrawImage(source, new Rectangle(newImage.Width - edge.Right, edge.Top, edge.Right, newImage.Height - edge.Top - edge.Bottom), source.Width - edge.Right, edge.Top, edge.Right, iTemp, GraphicsUnit.Pixel);//右中

            iTemp = source.Width - edge.Left - edge.Right;
            iTemp = iTemp > 0 ? iTemp : 1; //width

            int iTemp1 = source.Height - edge.Top - edge.Bottom;
            iTemp1 = iTemp1 > 0 ? iTemp1 : 1;
            gp.DrawImage(source, new Rectangle(edge.Left, edge.Top, newImage.Width - edge.Left - edge.Right, newImage.Height - edge.Top - edge.Bottom), edge.Left, edge.Top, iTemp, iTemp1, GraphicsUnit.Pixel);//中间

            return newImage;
        }

        /// <summary>
        /// 获取指定矩形的图片
        /// </summary>
        /// <param name="rectangle"></param>
        /// <returns></returns>
        internal Image GetActualBackground(Rectangle rectangle)
        {
            var result = new Bitmap(rectangle.Width, rectangle.Height);
            if (this._actualBackground != null)
            {
                using (var g = Graphics.FromImage(result))
                {
                    g.DrawImage(this._actualBackground, 0, 0, rectangle, GraphicsUnit.Pixel);
                }
            }
            return result;
        }
    }
}
