using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XiaoHeitu.ZPlayer.WinForm.Apis;
using XiaoHeitu.ZPlayer.WinForm.Forms;

namespace XiaoHeitu.ZPlayer.WinForm.Controls
{
    public class ZImageButton : ZControl
    {

        private bool isDown = false;
        private bool isHover = false;

        BaseForm _Form = null;

        [Browsable(true)]
        private Padding ImageEdgeInset { get; set; } = Padding.Empty;

        [Browsable(true)]
        public Image NormalImage { get; set; }
        [Browsable(true)]
        public Image PressImage { get; set; }
        [Browsable(true)]
        public Image HoverImage { get; set; }

        //public ZImageButton() : base()
        //{

        //}

        protected override void OnPaint(ZPaintContext context)
        {
            base.OnPaint(context);
            if (!this.Visible)
            {
                return;
            }
            Image image = null;
            if (this.isDown && this.PressImage != null)
            {
                image = this.PressImage;
            }
            else if (this.isHover && this.HoverImage != null)
            {
                image = this.HoverImage;
            }
            else if (this.NormalImage != null)
            {
                image = this.NormalImage;
            }
            else
            {
                return;
            }
            if (this.ImageEdgeInset != Padding.Empty)
            {
                image = ImageApi.ImageStretch(image, this.ImageEdgeInset, this.Size,context.ClipRectangle);
            }

            context.Graphics.DrawImage(image, context.ClipRectangle, new RectangleF(Point.Empty, image.Size), GraphicsUnit.Pixel);
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            this.isDown = true;
            this.Invalidate();
            base.OnMouseDown(mevent);
        }
        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            this.isDown = false;
            this.Invalidate();
            base.OnMouseUp(mevent);
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            this.isHover = true;
            this.Invalidate();
            base.OnMouseEnter(e);
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            this.isHover = false;
            this.Invalidate();
            base.OnMouseLeave(e);
        }
    }
}
