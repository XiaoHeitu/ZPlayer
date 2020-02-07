using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XiaoHeitu.ZPlayer.WinForm.Apis;

namespace XiaoHeitu.ZPlayer.WinForm.Controls
{
    public class ZContainer : Control
    {

        [Browsable(true)]
        public ZControlCollection ZControls
        {
            get;
            private set;
        }
        [Browsable(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        public Padding BackgroundEdgeInset
        {
            get; set;
        } = new Padding(0, 0, 0, 0);

        [Browsable(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        public new Image BackgroundImage
        {
            get; set;
        }
        public ZContainer()
        {
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;
            this.SetStyle(ControlStyles.Opaque, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            this.SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲
            this.SetStyle(ControlStyles.Opaque, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.ZControls = new ZControlCollection(this);
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            foreach (var control in this.ZControls)
            {
                control.DoLayout();
            }
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {            
            //Console.WriteLine($"OnMouseDown:{e.Location}");
            bool handler = false;
            foreach (var control in this.ZControls)
            {
                handler |= control.DoMouseDown(e);
            }
            if (handler)
            {
                return;
            }
            base.OnMouseDown(e);
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            //Console.WriteLine($"OnMouseUp:{e.Location}");
            bool handler = false;
            foreach (var control in this.ZControls)
            {
                handler |= control.DoMouseUp(e);
                handler |= control.DoClick(e);
            }
            if (handler)
            {
                return;
            }
            base.OnMouseUp(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            //Console.WriteLine($"OnMouseMove:{e.Location}");
            bool handler = false;
            foreach (var control in this.ZControls)
            {
                handler |= control.DoMouseMove(e);
                handler |= control.DoMouseEnter(e);
                handler |= control.DoMouseLeave(e);
            }
            if (handler)
            {
                return;
            }
            base.OnMouseMove(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            if (this.BackgroundImage == null || this.BackgroundImageLayout != ImageLayout.Stretch || this.ClientRectangle == Rectangle.Empty)
            {
                base.OnPaint(e);
                return;
            }

            var image = ImageApi.ImageStretch(this.BackgroundImage, this.BackgroundEdgeInset, this.ClientSize, e.ClipRectangle);
            e.Graphics.DrawImage(image, e.ClipRectangle);
            this.PaintChildren(e);
        }

        private void PaintChildren(PaintEventArgs containerPea)
        {
            foreach (var c in this.ZControls)
            {
                c.DoPaint(containerPea);
            }
        }
    }
}
