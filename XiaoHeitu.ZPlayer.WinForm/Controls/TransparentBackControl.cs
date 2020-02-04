using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XiaoHeitu.ZPlayer.WinForm.Forms;

namespace XiaoHeitu.ZPlayer.WinForm.Controls
{
    public class TransparentBackControl: Control
    {
        BufferedGraphicsContext MyBufferedGraphics = new BufferedGraphicsContext();
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
                var width = this.Size.Width;
                var height = this.Size.Height;
                using (BufferedGraphics MyBuffer = this.MyBufferedGraphics.Allocate(this.CreateGraphics(), new Rectangle(0, 0, width, height)))//创建一个缓冲图像MyBuffer
                {
                    this.MyBufferedGraphics.MaximumBuffer = new Size(width, height);//指定缓冲图像背景类的大小 
                    var g = MyBuffer.Graphics;

                    g.DrawImage(this._form.GetActualBackground(new Rectangle(this.Location.X, this.Location.Y, this.Size.Width, this.Size.Height)), Point.Empty);
                    this.OnZPaint(new PaintEventArgs(g, e.ClipRectangle));

                    MyBuffer.Render();//将画好的图显示到窗口当中
                    MyBuffer.Dispose();//释放资源  
                }
            }
            //base.OnPaint(e);
        }

        protected virtual void OnZPaint(PaintEventArgs e)
        {

        }
    }
}
