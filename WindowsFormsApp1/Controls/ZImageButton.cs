using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.Controls
{
    public class ZImageButton : TransparentBackControl
    {
        [Browsable(true)]
        public Image NormalImage { get; set; }
        [Browsable(true)]
        public Image PressImage { get; set; }
        [Browsable(true)]
        public Image HoverImage { get; set; }

        private bool isDown = false;
        private bool isHover = false;

        BaseForm _Form = null;

        public ZImageButton()
        {
        }
        protected override void OnParentChanged(EventArgs e)
        {
            this._Form = this.FindForm() as BaseForm;
            base.OnParentChanged(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if(this.ClientRectangle== Rectangle.Empty)
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
        protected override void OnMouseMove(MouseEventArgs mevent)
        {
            base.OnMouseMove(mevent);
        }
    }
}
