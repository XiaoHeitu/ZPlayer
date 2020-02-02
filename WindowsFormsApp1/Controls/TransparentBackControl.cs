using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.Controls
{
    public class TransparentBackControl: Control
    {
        private BaseForm _form = null;
        protected override void OnParentChanged(EventArgs e)
        {
            this._form=this.FindForm() as BaseForm;
            base.OnParentChanged(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (this._form != null)
            {
                e.Graphics.DrawImage(this._form.GetActualBackground(new Rectangle(this.Location.X, this.Location.Y, this.Size.Width, this.Size.Height)), Point.Empty);
            }
            //base.OnPaint(e);
        }
    }
}
