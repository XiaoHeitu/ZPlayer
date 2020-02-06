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
    }
}
