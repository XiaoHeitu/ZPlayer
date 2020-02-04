using System;
using System.Collections.Generic;
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
    }
}
