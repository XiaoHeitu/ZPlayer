using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XiaoHeitu.ZPlayer.WinForm.Apis;
using XiaoHeitu.ZPlayer.WinForm.Events;
using XiaoHeitu.ZPlayer.WinForm.Properties;

namespace XiaoHeitu.ZPlayer.WinForm.Controls
{
    public class ZSlider : ZControl
    {
        private float value = 0;
        private int railWidth = 11;
        private Image railImage = Resources.Slider_Rail;
        private Padding railEdgeInset = new Padding(6, 5, 6, 5);
        private Image loaderImage = Resources.Slider_Loader;
        private Padding loaderEdgeInset = new Padding(6, 5, 6, 5);
        private Size draggerSize = new Size(19, 19);
        private Image normalDraggerImage = Resources.Slider_Dragger_OnPress;
        private Image pressDraggerImage = Resources.Slider_Dragger_OnPress;
        private Image hoverDraggerImage = Resources.Slider_Dragger;
        private Padding draggerEdgeInset = new Padding(0);
        private float loaderValue = 0;
        private Padding railPadding = new Padding(0);


        private bool isDown = false;
        private bool isHover = false;
        private bool isDraggerDown = false;
        private bool isDraggerHover = false;

        float lastHoverValue = -1;

        [Browsable(true)]
        public float Value
        {
            get => this.value;
            set
            {
                if (this.value != value && !this.isDraggerDown && !this.isDown)
                {
                    this.value = value;
                    this.Invalidate();
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
                    this.Invalidate();
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
                    this.Invalidate();
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
                    this.Invalidate();
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
                    this.Invalidate();
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
                    this.Invalidate();
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
                    this.Invalidate();
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
                    this.Invalidate();
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
                    this.Invalidate();
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
                    this.Invalidate();
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
                    this.Invalidate();
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
                    this.Invalidate();
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
                    this.Invalidate();
                }
            }
        }



        public event ValueChangedEventHandler ValueChanged;
        public event HoverEventHandler Hover;



        protected override void OnPaint(ZPaintContext context)
        {
            base.OnPaint(context);
            this.DrawControl(context.Graphics);
        }


        //Point dragStartPoint;

        protected virtual void OnValueChanged(float newValue)
        {
            if (this.ValueChanged != null)
            {
                this.ValueChanged(this, new ValueChangedEventArgs(newValue));
            }
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            if (mevent.Button == MouseButtons.Left)
            {
                this.isDown = true;
            }
            if (this.isDraggerHover && mevent.Button == MouseButtons.Left)
            {
                this.isDraggerDown = true;
            }
            else
            {
                this.isDraggerDown = false;
                if (mevent.Button == MouseButtons.Left)
                {
                    var newValue = this.GetValueByPoint(mevent.Location);
                    this.OnValueChanged(newValue);

                    this.value = newValue;
                }
            }



            this.Invalidate();
            base.OnMouseDown(mevent);
        }
        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            this.isDown = false;
            this.isDraggerDown = false;
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
            this.isDraggerHover = false;

            this.Invalidate();
            base.OnMouseLeave(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {

            PointF draggerPoint = new PointF((this.Size.Width - this.draggerSize.Width) * this.value, (this.Size.Height - this.draggerSize.Height) / 2f);
            RectangleF draggerRectangle = new RectangleF(draggerPoint, this.DraggerSize);

            if (draggerRectangle.Contains(e.Location))
            {
                this.isDraggerHover = true;
            }
            else
            {
                this.isDraggerHover = false;
            }
            //滑块事件
            if (this.isDraggerDown)
            {
                var newValue = this.GetValueByPoint(e.Location);
                this.OnValueChanged(newValue);

                this.value = newValue;
            }


            //悬停事件
            //计算滑轨位置位置
            PointF railPoint = new PointF(this.draggerSize.Width / 2f, (this.Size.Height - this.railWidth) / 2f);
            //计算滑轨大小
            Size railSize = new Size(this.Size.Width - this.draggerSize.Width, this.railWidth);
            RectangleF railRectangle = new RectangleF(railPoint, railSize);
            if (railRectangle.Contains(e.Location))
            {
                //计算悬停值
                var hoverValue = this.GetValueByPoint(e.Location);
                //触发事件
                if (this.Hover != null && hoverValue != this.lastHoverValue)
                {
                    this.lastHoverValue = hoverValue;
                    this.Hover(this, new HoverEventArgs(hoverValue, e.Location));
                }
            }


            this.Invalidate();
            base.OnMouseMove(e);
        }

        private float GetValueByPoint(PointF point)
        {
            var value = (point.X - (this.draggerSize.Width / 2f)) / (float)(this.Size.Width - this.draggerSize.Width);
            if (value > 1)
            {
                value = 1;
            }
            if (value < 0)
            {
                value = 0;
            }
            return value;
        }

        private void DrawControl(ZGraphics g)
        {
            var width = this.Size.Width;
            var height = this.Size.Height;
            //画滑轨
            //计算滑轨位置位置
            PointF railPoint = new PointF(this.draggerSize.Width / 2f, (height - this.railWidth) / 2f);
            //计算滑轨大小
            Size railSize = new Size(width - this.draggerSize.Width, this.railWidth);
            if (railSize.Width > 0 && railSize.Height > 0)
            {
                //计算滑轨图像
                Image railImage = null;
                if (this.railEdgeInset != Padding.Empty)
                {
                    railImage = ImageApi.ImageStretch(this.railImage, this.railEdgeInset, railSize);
                }
                else
                {
                    railImage = this.railImage;
                }
                //开始画
                //g.DrawImage(railImage, railPoint.X, railPoint.Y, railSize.Width, railSize.Height);
                g.DrawImage(railImage, new RectangleF(railPoint, railSize), new RectangleF(Point.Empty, railSize), GraphicsUnit.Pixel);
            }
            //画加载进度
            if (this.loaderValue > 0)
            {
                //计算加载进度位置
                PointF loaderPoint = new PointF(railPoint.X + this.railPadding.Left, railPoint.Y + this.railPadding.Top);
                //计算加载进度大小
                Size loaderSize = new Size((int)((railSize.Width - this.railPadding.Left - this.railPadding.Right) * this.loaderValue), railSize.Height - this.railPadding.Top - this.railPadding.Bottom);
                if (loaderSize.Width > 0 && loaderSize.Height > 0)
                {
                    //计算加载进度图像
                    //计算滑轨图像

                    Image loaderImage = null;
                    if (this.loaderEdgeInset != Padding.Empty)
                    {
                        loaderImage = ImageApi.ImageStretch(this.loaderImage, this.loaderEdgeInset, loaderSize);
                    }
                    else
                    {
                        loaderImage = this.loaderImage;
                    }
                    //开始画
                    //g.DrawImage(loaderImage, loaderPoint.X, loaderPoint.Y, loaderSize.Width, loaderSize.Height);
                    g.DrawImage(loaderImage, new RectangleF(loaderPoint, loaderSize), new RectangleF(Point.Empty, loaderSize), GraphicsUnit.Pixel);
                }
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
                //g.DrawImage(draggerImage, draggerPoint.X, draggerPoint.Y, this.draggerSize.Width, this.draggerSize.Height);
                g.DrawImage(draggerImage, new RectangleF(draggerPoint, this.draggerSize), new RectangleF(Point.Empty, this.draggerSize), GraphicsUnit.Pixel);
            }
        }

    }
}
