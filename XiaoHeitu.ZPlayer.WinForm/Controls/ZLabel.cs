using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XiaoHeitu.ZPlayer.WinForm.Controls
{
    public class ZLabel : ZControl
    {
        private string text;
        private Font font = new Font("宋休", 9);
        private Color textColor = Color.Black;

        [Browsable(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        public string Text
        {
            get
            {
                return this.text;
            }
            set
            {
                this.text = value;
                this.Invalidate();
            }
        }

        [Browsable(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        public Font Font
        {
            get
            {
                return this.font;
            }
            set
            {
                this.font = value;
                this.Invalidate();
            }
        }
        [Browsable(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        public Color TextColor
        {
            get
            {
                return this.textColor;
            }
            set
            {
                this.textColor = value;
                this.Invalidate();
            }
        }
        protected override void OnPaint(ZPaintContext context)
        {
            base.OnPaint(context);

            context.Graphics.DrawString(this.Text, this.Font, new SolidBrush(this.TextColor), new RectangleF(this.Location, this.Size), StringFormat.GenericDefault);
        }
    }

}
