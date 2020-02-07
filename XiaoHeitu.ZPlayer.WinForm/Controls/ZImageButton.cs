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
        private Padding imageEdgeInset = Padding.Empty;
        private Image normalImage;
        private Image pressImage;
        private Image hoverImage;

        [Browsable(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        private Padding ImageEdgeInset
        {
            get
            {
                return this.imageEdgeInset;
            }
            set
            {
                this.imageEdgeInset = value;
            }
        }
        [Browsable(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        public Image NormalImage
        {
            get
            {
                return this.normalImage;
            }
            set
            {
                this.normalImage = value;
                this.Invalidate();
            }
        }
        [Browsable(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        public Image PressImage
        {
            get
            {
                return this.pressImage;
            }
            set
            {
                this.pressImage = value;
                this.Invalidate();
            }
        }
        [Browsable(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        public Image HoverImage
        {
            get
            {
                return this.hoverImage;
            }
            set
            {
                this.hoverImage = value;
                this.Invalidate();
            }
        }

        //public ZImageButton() : base()
        //{

        //}

        protected override void OnPaint(ZPaintContext context)
        {
            base.OnPaint(context);
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
                image = ImageApi.ImageStretch(image, this.ImageEdgeInset, this.Size, context.ClipRectangle);
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
