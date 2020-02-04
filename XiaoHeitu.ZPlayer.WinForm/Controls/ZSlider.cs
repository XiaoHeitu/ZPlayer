using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XiaoHeitu.ZPlayer.WinForm.Apis;
using XiaoHeitu.ZPlayer.WinForm.Properties;

namespace XiaoHeitu.ZPlayer.WinForm.Controls
{
    public class ZSlider : SkinControl
    {

        private float value = 0;
        private int railWidth = 10;
        private Image railImage = Resources.Slider_Rail;
        private Padding railEdgeInset = new Padding(2);
        private Image loaderImage = Resources.Slider_Loader;
        private Padding loaderEdgeInset = new Padding(2);
        private Size draggerSize = new Size(15, 15);
        private Image normalDraggerImage = Resources.Pause_Onpress;
        private Image pressDraggerImage = Resources.Pause_Onpress;
        private Image hoverDraggerImage = Resources.Pause;
        private Padding draggerEdgeInset = new Padding(2);
        private float loaderValue = 0;
        private Padding railPadding = new Padding(2);


        private bool isDown = false;
        private bool isHover = false;
        private bool isDraggerDown = false;
        private bool isDraggerHover = false;

        [Browsable(true)]
        public float Value
        {
            get => this.value;
            set
            {
                if (this.value != value && !this.isDraggerDown)
                {
                    this.value = value;
                    this.Repaint();
                }
            }
        }
        [Browsable(true)]
        public Size DraggerSize
        {
            get => this.draggerSize;
            set
            {
                if (this.draggerSize != value)
                {
                    this.draggerSize = value;
                    this.Repaint();
                }
            }
        }
        [Browsable(true)]
        public int RailWidth
        {
            get => this.railWidth;
            set
            {
                if (this.railWidth != value)
                {
                    this.railWidth = value;
                    this.Repaint();
                }
            }
        }
        [Browsable(true)]
        public Image NormalDraggerImage
        {
            get => this.normalDraggerImage;
            set
            {
                if (this.normalDraggerImage != value)
                {
                    this.normalDraggerImage = value;
                    this.Repaint();
                }
            }
        }
        [Browsable(true)]
        public Image PressDraggerImage
        {
            get => this.pressDraggerImage;
            set
            {
                if (this.pressDraggerImage != value)
                {
                    this.pressDraggerImage = value;
                    this.Repaint();
                }
            }
        }
        [Browsable(true)]
        public Image HoverDraggerImage
        {
            get => this.hoverDraggerImage;
            set
            {
                if (this.hoverDraggerImage != value)
                {
                    this.hoverDraggerImage = value;
                    this.Repaint();
                }
            }
        }
        [Browsable(true)]
        public Padding DraggerEdgeInset
        {
            get => this.draggerEdgeInset;
            set
            {
                if (this.draggerEdgeInset != value)
                {
                    this.draggerEdgeInset = value;
                    this.Repaint();
                }
            }
        }
        [Browsable(true)]
        public Image RailImage
        {
            get => this.railImage;
            set
            {
                if (this.railImage != value)
                {
                    this.railImage = value;
                    this.Repaint();
                }
            }
        }
        [Browsable(true)]
        public Padding RailEdgeInset
        {
            get => this.railEdgeInset;
            set
            {
                if (this.railEdgeInset != value)
                {
                    this.railEdgeInset = value;
                    this.Repaint();
                }
            }
        }
        [Browsable(true)]
        public Padding RailPadding
        {
            get => this.railPadding;
            set
            {
                if (this.railPadding != value)
                {
                    this.railPadding = value;
                    this.Repaint();
                }
            }
        }

        [Browsable(true)]
        public float LoaderValue
        {
            get => this.loaderValue;
            set
            {
                if (this.loaderValue != value)
                {
                    this.loaderValue = value;
                    this.Repaint();
                }
            }
        }

        [Browsable(true)]
        public Image LoaderImage
        {
            get => this.loaderImage;
            set
            {
                if (this.loaderImage != value)
                {
                    this.loaderImage = value;
                    this.Repaint();
                }
            }
        }

        [Browsable(true)]
        public Padding LoaderEdgeInset
        {
            get => this.loaderEdgeInset;
            set
            {
                if (this.loaderEdgeInset != value)
                {
                    this.loaderEdgeInset = value;
                    this.Repaint();
                }
            }
        }



        public event Action<object, EventArgs> ValueChanged;

        private void Repaint()
        {

            if (this.Size.IsEmpty)
            {
                return;
            }
            this.OnPaint(new PaintEventArgs(this.CreateGraphics(), new Rectangle(0, 0, this.Size.Width, this.Size.Height)));
        }

        protected override void OnZPaint(PaintEventArgs e)
        {
            base.OnZPaint(e);
            this.DrawControl(e.Graphics);
        }

        Point dragStartPoint;

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            this.isDown = true;
            if (this.isDraggerHover)
            {
                this.isDraggerDown = true;
                this.dragStartPoint = mevent.Location;
            }
            else
            {
                this.isDraggerDown = false;
            }
            this.Repaint();
            base.OnMouseDown(mevent);
        }
        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            this.isDown = false;
            this.isDraggerDown = false;
            this.Repaint();
            base.OnMouseUp(mevent);
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            this.isHover = true;
            this.Repaint();
            base.OnMouseEnter(e);
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            this.isHover = false;
            this.isDraggerHover = false;
            this.isDraggerDown = false;
            this.Repaint();
            base.OnMouseLeave(e);
        }

        bool isDragging = false;
        protected override void OnMouseMove(MouseEventArgs e)
        {
            PointF draggerPoint = new PointF((this.Size.Width - this.draggerSize.Width) * this.value, (this.Size.Height - this.draggerSize.Height) / 2f);
            RectangleF r = new RectangleF(draggerPoint, this.DraggerSize);

            if (r.Contains(e.X, e.Y))
            {
                this.isDraggerHover = true;
            }
            else
            {
                this.isDraggerHover = false;
            }

            if (this.isDraggerDown)
            {
                var newValue = (e.X - (this.draggerSize.Width / 2f)) / (float)(this.Size.Width - this.draggerSize.Width);

                //var xoffset = e.X - this.dragStartPoint.X;

                //float valueOffset = xoffset / (float)(this.Size.Width - this.draggerSize.Width);
                //var newValue = this.value + valueOffset;
                if (newValue > 1)
                {
                    newValue = 1;
                }
                if (newValue < 0)
                {
                    newValue = 0;
                }
                this.value = newValue;
                if (this.ValueChanged != null)
                {
                    this.ValueChanged(this, EventArgs.Empty);
                }

                this.dragStartPoint = e.Location;
            }
            this.Repaint();
            base.OnMouseMove(e);
        }

        private void DrawControl(Graphics g)
        {
            var width = this.Size.Width;
            var height = this.Size.Height;
            //画滑轨
            //计算滑轨位置位置
            PointF railPoint = new PointF(this.draggerSize.Width / 2f, (height - this.railWidth) / 2f);
            //计算滑轨大小
            Size railSize = new Size(width - this.draggerSize.Width, this.railWidth);

            //计算滑轨图像
            var railImage = ImageApi.ImageStretch(this.railImage, this.railEdgeInset, railSize);
            //开始画
            g.DrawImage(railImage, railPoint);

            //画加载进度
            if (this.loaderValue > 0)
            {
                //计算加载进度位置
                PointF loaderPoint = new PointF(railPoint.X + this.railPadding.Left, railPoint.Y + this.railPadding.Top);
                //计算加载进度大小
                Size loaderSize = new Size((int)((railSize.Width - this.railPadding.Left - this.railPadding.Right) * this.loaderValue), railSize.Height - this.railPadding.Top - this.railPadding.Bottom);
                //计算加载进度图像
                var loaderImage = ImageApi.ImageStretch(this.loaderImage, this.loaderEdgeInset, loaderSize);
                //开始画
                g.DrawImage(loaderImage, loaderPoint);
            }

            //画拖动块
            PointF draggerPoint = new PointF((width - this.draggerSize.Width) * this.value, (height - this.draggerSize.Height) / 2f);
            //计算拖动块图像
            Image draggerImage = null;
            if (this.isDraggerDown && this.pressDraggerImage != null)
            {
                draggerImage = this.pressDraggerImage;
            }
            else if (this.isDraggerHover && this.hoverDraggerImage != null)
            {
                draggerImage = this.hoverDraggerImage;
            }
            else if (this.normalDraggerImage != null)
            {
                draggerImage = this.normalDraggerImage;
            }

            if (this.draggerEdgeInset != Padding.Empty)
            {
                draggerImage = ImageApi.ImageStretch(draggerImage, this.draggerEdgeInset, this.draggerSize);
            }
            //开始画
            if (draggerImage != null)
            {
                g.DrawImage(draggerImage, draggerPoint.X, draggerPoint.Y, this.draggerSize.Width, this.draggerSize.Height);
            }
        }
    }
}
