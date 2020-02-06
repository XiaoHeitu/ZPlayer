using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XiaoHeitu.ZPlayer.WinForm.Events
{
    public class ValueChangedEventArgs
    {
        public ValueChangedEventArgs(float value)
        {
            this.Value = value;
        }

        public float Value
        {
            get;
            private set;
        }
    }
}
