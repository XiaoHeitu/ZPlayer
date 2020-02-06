using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XiaoHeitu.ZPlayer.WinForm.Controls
{
    public class ZControl
    {
        /// <summary>
        /// 持有者
        /// </summary>
        private ZContainer owner;

        bool isHover = false;
        bool isDown = false;
        bool isInit = false;
        Rectangle AnchorRect = Rectangle.Empty;

        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get; set;
        }

        public ZContainer Owner
        {
            get
            {
                return this.owner;
            }
            set
            {
                this.owner = value;
            }
        }

        /// <summary>
        /// 控件尺寸
        /// </summary>
        [RefreshProperties(RefreshProperties.Repaint)]
        public Size Size
        {
            get; set;
        } = new Size(20, 20);
        /// <summary>
        /// 控件位置
        /// </summary>
        [RefreshProperties(RefreshProperties.Repaint)]
        public Point Location
        {
            get; set;
        }


        /// <summary>
        /// 获取或设置控件绑定到的容器的边缘并确定控件如何随其父级一起调整大小。
        /// </summary>
        [DefaultValue(AnchorStyles.Top | AnchorStyles.Left)]
        [RefreshProperties(RefreshProperties.Repaint)]
        public AnchorStyles Anchor
        {
            get;
            set;
        } = AnchorStyles.Top | AnchorStyles.Left;

        /// <summary>
        /// 获取或设置控件绑定到的容器的边缘并确定控件如何随其父级一起调整大小。
        /// </summary>
        [DefaultValue(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        public bool Visible
        {
            get;
            set;
        } = true;

        public event EventHandler Click;
        public event EventHandler MouseLeave;
        /// <summary>
        /// 
        /// </summary>
        public ZControl()
        {

        }


        public void BeginInit()
        {
            this.isInit = true;
        }
        public void EndInit()
        {
            if (this.owner != null)
            {
                this.AnchorRect = Rectangle.FromLTRB(
                    this.Anchor.HasFlag(AnchorStyles.Left) ? this.Location.X : int.MinValue,
                    this.Anchor.HasFlag(AnchorStyles.Top) ? this.Location.Y : int.MinValue,
                    this.Anchor.HasFlag(AnchorStyles.Right) ? this.owner.Width - this.Location.X - this.Size.Width : int.MinValue,
                    this.Anchor.HasFlag(AnchorStyles.Bottom) ? this.owner.Height - this.Location.Y - this.Size.Height : int.MinValue);
            }
            this.isInit = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        public ZControl(ZContainer owner)
        {
            this.owner = owner;
        }

        protected virtual void OnPaint(ZPaintContext context)
        {
            this.ResumeLayout();
        }

        private void ResumeLayout()
        {
            int x, y, width, height;
            if (this.Anchor.HasFlag(AnchorStyles.Left) && this.Anchor.HasFlag(AnchorStyles.Right))
            {
                x = this.AnchorRect.Left;
                width = this.owner.Size.Width - this.AnchorRect.Left - this.AnchorRect.Right;
            }
            else if (this.Anchor.HasFlag(AnchorStyles.Left))
            {
                x = this.AnchorRect.Left;
                width = this.Size.Width;
            }
            else if (this.Anchor.HasFlag(AnchorStyles.Right))
            {
                x = this.owner.Size.Width - this.AnchorRect.Right - this.Size.Width;
                width = this.Size.Width;
            }
            else
            {
                x = this.Location.X;
                width = this.Size.Width;
            }

            if (this.Anchor.HasFlag(AnchorStyles.Top) && this.Anchor.HasFlag(AnchorStyles.Bottom))
            {
                y = this.AnchorRect.Top;
                height = this.owner.Height - this.AnchorRect.Top - this.AnchorRect.Bottom;
            }
            else if (this.Anchor.HasFlag(AnchorStyles.Top))
            {
                y = this.AnchorRect.Top;
                height = this.Size.Height;
            }
            else if (this.Anchor.HasFlag(AnchorStyles.Bottom))
            {
                y = this.owner.Height - this.AnchorRect.Bottom - this.Size.Height;
                height = this.Size.Height;
            }
            else
            {
                y = this.Location.Y;
                height = this.Size.Height;
            }

            this.Location = new Point(x, y);
            this.Size = new Size(width, height);
        }

        /// <summary>
        /// 执行重绘
        /// </summary>
        /// <param name="containerPea"></param>
        public void DoPaint(PaintEventArgs containerPea)
        {
            if (this.Size.Width > 0 && this.Size.Height > 0)
            {
                //计算子控件绘制区域
                var controlRect = new Rectangle(this.Location, this.Size);
                var drawRect = Rectangle.Intersect(containerPea.ClipRectangle, controlRect);
                if (drawRect == Rectangle.Empty)
                {
                    return;
                }
                drawRect.Offset(-this.Location.X, -this.Location.Y);
                this.OnPaint(new ZPaintContext(new ZGraphics(containerPea.Graphics, this), drawRect));
            }
            else
            {
                var drawRect = new Rectangle(this.Location, this.Size);
                drawRect.Offset(-this.Location.X, -this.Location.Y);
                this.OnPaint(new ZPaintContext(new ZGraphics(containerPea.Graphics, this), drawRect));
            }
        }

        /// <summary>
        /// 使控件的整个图面无效并导致重绘控件。
        /// </summary>
        public void Invalidate()
        {
            if (this.owner == null)
            {
                return;
            }
            this.owner.Invalidate(new Rectangle(this.Location, this.Size));
        }
        /// <summary>
        /// 使控件的指定区域无效（将其添加到控件的更新区域，下次绘制操作时将重新绘制更新区域），并向控件发送绘制消息。
        /// </summary>
        /// <param name="rc">指定区域</param>
        public void Invalidate(Rectangle rc)
        {
            if (this.owner == null)
            {
                return;
            }
            var newRc = rc;
            newRc.Offset(this.Location);
            this.owner.Invalidate(newRc);
        }
        /// <summary>
        /// 使控件重绘其工作区内的无效区域。
        /// </summary>
        public void Update()
        {
            if (this.owner == null)
            {
                return;
            }
            this.owner.Update();
        }

        /// <summary>
        /// 强制控件使其工作区无效并立即重绘自己和任何子控件。
        /// </summary>
        public void Refresh()
        {
            this.Invalidate();
            this.Update();
        }

        protected virtual void OnMouseDown(MouseEventArgs mevent)
        {
        }
        protected virtual void OnMouseUp(MouseEventArgs mevent)
        {
        }
        protected virtual void OnMouseEnter(EventArgs e)
        {
        }
        protected virtual void OnMouseLeave(EventArgs e)
        {
            if (this.MouseLeave != null)
            {
                this.MouseLeave(this, EventArgs.Empty);
            }
        }


        protected virtual void OnMouseMove(MouseEventArgs e)
        {

        }
        protected virtual void OnClick(EventArgs e)
        {
            if (this.Click != null)
            {
                this.Click(this, EventArgs.Empty);
            }
        }

        private MouseEventArgs CreateMouseEventArgs(MouseEventArgs containerMouseEventArgs)
        {
            return new MouseEventArgs(
                containerMouseEventArgs.Button,
                containerMouseEventArgs.Clicks,
                containerMouseEventArgs.X - this.Location.X,
                containerMouseEventArgs.Y - this.Location.Y,
                containerMouseEventArgs.Delta);
        }
        /// <summary>
        /// OnMouseDown-->DoMouseDown
        /// </summary>
        /// <param name="e"></param>
        public void DoMouseDown(MouseEventArgs ce)
        {
            var rect = new Rectangle(this.Location, this.Size);
            if (rect.Contains(ce.Location))
            {
                this.isDown = true;
                var e = this.CreateMouseEventArgs(ce);
                this.OnMouseDown(e);
            }
        }


        /// <summary>
        /// OnMouseUp-->DoMouseUp
        /// </summary>
        /// <param name="e"></param>
        public void DoMouseUp(MouseEventArgs ce)
        {
            var rect = new Rectangle(this.Location, this.Size);
            if (rect.Contains(ce.Location))
            {
                this.isDown = false;
                var e = this.CreateMouseEventArgs(ce);
                this.OnMouseUp(e);
            }
        }

        /// <summary>
        /// OnMouseMove-->DoMouseEnter
        /// </summary>
        /// <param name="e"></param>
        public void DoMouseEnter(MouseEventArgs ce)
        {
            var rect = new Rectangle(this.Location, this.Size);
            if (rect.Contains(ce.Location))
            {
                if (!this.isHover)
                {
                    this.isHover = true;
                    this.OnMouseEnter(EventArgs.Empty);
                }
            }
        }
        /// <summary>
        /// OnMouseMove-->DoMouseLeave
        /// </summary>
        /// <param name="e"></param>
        public void DoMouseLeave(MouseEventArgs ce)
        {
            var rect = new Rectangle(this.Location, this.Size);
            if (!rect.Contains(ce.Location))
            {
                if (this.isHover)
                {
                    this.isHover = false;
                    this.OnMouseLeave(EventArgs.Empty);
                }
            }
        }
        /// <summary>
        /// OnMouseMove-->DoMouseMove
        /// </summary>
        /// <param name="e"></param>
        public virtual void DoMouseMove(MouseEventArgs ce)
        {
            var rect = new Rectangle(this.Location, this.Size);
            if (rect.Contains(ce.Location))
            {
                var e = this.CreateMouseEventArgs(ce);
                this.OnMouseMove(e);
            }
        }
        /// <summary>
        /// OnMouseUp-->DoClick
        /// </summary>
        /// <param name="e"></param>
        public virtual void DoClick(MouseEventArgs ce)
        {
            var rect = new Rectangle(this.Location, this.Size);
            if (rect.Contains(ce.Location))
            {
                this.OnClick(EventArgs.Empty);
            }
        }
    }
}
