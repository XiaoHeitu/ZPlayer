using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XiaoHeitu.ZPlayer.WinForm.Events
{
    public class HoverEventArgs : EventArgs
    {
        public HoverEventArgs(float hoverValue, Point mouseLocation)
        {
            this.HoverValue = hoverValue;
            this.MouseLocation = mouseLocation;
        }
        public float HoverValue
        {
            get;
            private set;
        }
        public Point MouseLocation
        {
            get;
            private set;
        }
    }
}
