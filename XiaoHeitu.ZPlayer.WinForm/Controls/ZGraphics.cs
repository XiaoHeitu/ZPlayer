using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XiaoHeitu.ZPlayer.WinForm.Controls
{
    public class ZGraphics
    {
        private Graphics graphics;
        private ZControl zControl;
        public ZGraphics(Graphics graphics, ZControl zControl)
        {
            this.graphics = graphics;
            this.zControl = zControl;
        }

        /// <summary>
        /// 在指定位置并且按指定大小绘制指定的 System.Drawing.Image 的指定部分。
        /// </summary>
        /// <param name="image">要绘制的 System.Drawing.Image。</param>
        /// <param name="destRect">System.Drawing.RectangleF 结构，它指定所绘制图像的位置和大小。 将图像进行缩放以适合该矩形。</param>
        /// <param name="srcRect">System.Drawing.RectangleF 结构，它指定 image 对象中要绘制的部分。</param>
        /// <param name="srcUnit">System.Drawing.GraphicsUnit 枚举的成员，它指定 srcRect 参数所用的度量单位。</param>
        /// <exception cref="System.ArgumentNullException">image 为 null。</exception>
        public void DrawImage(Image image, RectangleF destRect, RectangleF srcRect, GraphicsUnit srcUnit)
        {
            destRect.Offset(this.zControl.Location);
            this.graphics.DrawImage(image, destRect, srcRect, srcUnit);
        }

        /// <summary>
        /// 在指定位置并且按指定大小绘制指定的 System.Drawing.Image 的指定部分。
        /// </summary>
        /// <param name="image">要绘制的 System.Drawing.Image。</param>
        /// <param name="destRect">System.Drawing.Rectangle 结构，它指定所绘制图像的位置和大小。 将图像进行缩放以适合该矩形。</param>
        /// <param name="srcRect">System.Drawing.Rectangle 结构，它指定 image 对象中要绘制的部分。</param>
        /// <param name="srcUnit">System.Drawing.GraphicsUnit 枚举的成员，它指定 srcRect 参数所用的度量单位。</param>
        /// <exception cref="System.ArgumentNullException">image 为 null。</exception>
        public void DrawImage(Image image, Rectangle destRect, Rectangle srcRect, GraphicsUnit srcUnit)
        {
            destRect.Offset(this.zControl.Location);
            this.graphics.DrawImage(image, destRect, srcRect, srcUnit);
        }

        /// <summary>
        /// 使用指定 System.Drawing.StringFormat 的格式化特性，用指定的 System.Drawing.Brush 和 System.Drawing.Font 对象在指定的矩形中绘制指定的文本字符串。
        /// </summary>
        /// <param name="s">要绘制的字符串。</param>
        /// <param name="font">System.Drawing.Font，它定义字符串的文本格式。</param>
        /// <param name="brush">System.Drawing.Brush，它确定所绘制文本的颜色和纹理。</param>
        /// <param name="layoutRectangle">System.Drawing.RectangleF 结构，它指定所绘制文本的位置。</param>
        /// <param name="format">System.Drawing.StringFormat，它指定应用于所绘制文本的格式化特性（如行距和对齐方式）。</param>
        /// <exception cref="System.ArgumentNullException">brush 为 null。 或 - s 为 null。</exception>
        public void DrawString(string s, Font font, Brush brush, RectangleF layoutRectangle, StringFormat format)
        {
            layoutRectangle.Offset(this.zControl.Location);
            this.graphics.DrawString(s, font, brush, layoutRectangle, format);
        }
    }
}
