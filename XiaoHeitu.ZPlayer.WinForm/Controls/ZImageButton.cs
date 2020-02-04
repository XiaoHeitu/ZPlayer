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
    public class ZImageButton : SkinControl
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

        public ZImageButton()
        {
        }

        protected override void OnZPaint(PaintEventArgs e)
        {
            base.OnZPaint(e);
            if (this.ClientRectangle == Rectangle.Empty)
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
                image = ImageApi.ImageStretch(image, this.ImageEdgeInset, this.Size);
            }

            e.Graphics.DrawImage(image, 0, 0, this.Width, this.Height);
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            this.isDown = true;
            this.Refresh();
            base.OnMouseDown(mevent);
        }
        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            this.isDown = false;
            this.Refresh();
            base.OnMouseUp(mevent);
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            this.isHover = true;
            this.Refresh();
            base.OnMouseEnter(e);
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            this.isHover = false;
            this.Refresh();
            base.OnMouseLeave(e);
        }
    }
}
