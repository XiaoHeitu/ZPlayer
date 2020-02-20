using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XiaoHeitu.ZPlayer.WinForm.Apis
{
    internal class ImageApi
    {
        public static Image ImageStretch(Image source, Padding edge, Size newSize)
        {
            Bitmap newImage = new Bitmap(newSize.Width, newSize.Height);
            using (var gp = Graphics.FromImage(newImage))
            {
                if (edge.Top > 0)
                {
                    if (edge.Left > 0)
                    {
                        gp.DrawImage(source, new Rectangle(0, 0, edge.Left, edge.Top), 0, 0, edge.Left, edge.Top, GraphicsUnit.Pixel);//左上
                    }
                    if (edge.Right > 0)
                    {
                        gp.DrawImage(source, new Rectangle(newImage.Width - edge.Right, 0, edge.Right, edge.Top), source.Width - edge.Right, 0, edge.Right, edge.Top, GraphicsUnit.Pixel);//右上
                    }
                }

                if (edge.Bottom > 0)
                {
                    if (edge.Left > 0)
                    {
                        gp.DrawImage(source, new Rectangle(0, newImage.Height - edge.Bottom, edge.Left, edge.Bottom), 0, source.Height - edge.Bottom, edge.Left, edge.Bottom, GraphicsUnit.Pixel); //左下
                    }
                    if (edge.Right > 0)
                    {
                        gp.DrawImage(source, new Rectangle(newImage.Width - edge.Right, newImage.Height - edge.Bottom, edge.Right, edge.Bottom), source.Width - edge.Right, source.Height - edge.Bottom, edge.Right, edge.Bottom, GraphicsUnit.Pixel); //右下
                    }
                }

                int mWidth = source.Width - edge.Left - edge.Right;
                if (mWidth > 0)
                {
                    if (edge.Top > 0)
                    {
                        gp.DrawImage(source, new Rectangle(edge.Left, 0, newImage.Width - edge.Right - edge.Left, edge.Top), edge.Right, 0, mWidth, edge.Top, GraphicsUnit.Pixel);//中上
                    }
                    if (edge.Bottom > 0)
                    {
                        gp.DrawImage(source, new Rectangle(edge.Left, newImage.Height - edge.Bottom, newImage.Width - edge.Right - edge.Left, edge.Bottom), edge.Right, source.Height - edge.Bottom, mWidth, edge.Bottom, GraphicsUnit.Pixel);//中下
                    }
                }


                int mHeight = source.Height - edge.Bottom - edge.Top;
                if (mHeight > 0)
                {
                    if (edge.Left > 0)
                    {
                        gp.DrawImage(source, new Rectangle(0, edge.Top, edge.Left, newImage.Height - edge.Top - edge.Bottom), 0, edge.Top, edge.Left, mHeight, GraphicsUnit.Pixel);//左中
                    }
                    if (edge.Right > 0)
                    {
                        gp.DrawImage(source, new Rectangle(newImage.Width - edge.Right, edge.Top, edge.Right, newImage.Height - edge.Top - edge.Bottom), source.Width - edge.Right, edge.Top, edge.Right, mHeight, GraphicsUnit.Pixel);//右中
                    }
                }

                if (mWidth > 0 && mHeight > 0)
                {
                    gp.DrawImage(source, new Rectangle(edge.Left, edge.Top, newImage.Width - edge.Left - edge.Right, newImage.Height - edge.Top - edge.Bottom), edge.Left, edge.Top, mWidth, mHeight, GraphicsUnit.Pixel);//中间
                }
            }
            return newImage;
        }
        public static Image ImageStretch(Image source, Padding edge, Size newSize, Rectangle clipRectangle)
        {
            var image = ImageStretch(source, edge, newSize);
            if (clipRectangle == new Rectangle(Point.Empty, newSize))
            {
                return image;
            }
            return ImageClip(image, clipRectangle);
        }

        public static Image ImageClip(Image source, Rectangle clipRectangle)
        {
            if (clipRectangle == new Rectangle(Point.Empty, source.Size))
            {
                return source;
            }
            Bitmap newImage = new Bitmap(clipRectangle.Width, clipRectangle.Height);
            using (var gp = Graphics.FromImage(newImage))
            {
                gp.DrawImage(source, new Rectangle(Point.Empty, clipRectangle.Size), clipRectangle, GraphicsUnit.Pixel);
            }
            return newImage;
        }

        public static void DrawImageByBitBlt(Graphics grDest, Bitmap image, Rectangle rDest)
        {
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
                    if (!Win32Api.BitBlt(hdcDest, rDest.X, rDest.Y, rDest.Width, rDest.Height, hdcSrc, 0, 0, Win32Api.TernaryRasterOperations.SRCCOPY))
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

        public static Bitmap BGR24ToBitmap(IntPtr rgb32Source, int width, int height)
        {
            int lenData = width * height * 3;//数据长度，24位图像数据的长度=宽度*高度*3
            byte[] imgBGR = new byte[lenData];//创建指定长度byte数据
            System.Runtime.InteropServices.Marshal.Copy(rgb32Source, imgBGR, 0, imgBGR.Length);

            Bitmap bmp = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            if (imgBGR != null)
            {
                //构造一个位图数组进行数据存储
                byte[] rgbvalues = new byte[imgBGR.Length];

                //对每一个像素的颜色进行转化
                for (int i = 0; i < rgbvalues.Length; i += 3)
                {
                    rgbvalues[i] = imgBGR[i + 2];
                    rgbvalues[i + 1] = imgBGR[i + 1];
                    rgbvalues[i + 2] = imgBGR[i];
                }


                //位图矩形
                Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                //以可读写的方式将图像数据锁定
                System.Drawing.Imaging.BitmapData bmpdata = bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, bmp.PixelFormat);
                //得到图形在内存中的首地址
                IntPtr ptr = bmpdata.Scan0;

                //把处理后的图像数组复制回图像
                System.Runtime.InteropServices.Marshal.Copy(rgbvalues, 0, ptr, imgBGR.Length);
                //解锁位图像素
                bmp.UnlockBits(bmpdata);

            }
            return bmp;
        }
    }
}
