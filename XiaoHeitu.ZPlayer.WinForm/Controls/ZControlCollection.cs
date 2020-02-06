using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XiaoHeitu.ZPlayer.WinForm.Controls
{
    public class ZControlCollection : List<ZControl>
    {
        private ZContainer owner;
        public ZControlCollection(ZContainer owner)
        {
            this.owner = owner;
        }
        /// <summary>
        /// 将对象添加到集合的末尾
        /// </summary>
        /// <param name="control">Z控件</param>
        public new void Add(ZControl control)
        {
            control.Owner = this.owner;
            base.Add(control);
        }

        /// <summary>
        /// 将元素插入集合的指定索引处
        /// </summary>
        /// <param name="index">索引</param>
        /// <param name="control">Z控件</param>
        public new void Insert(int index, ZControl control)
        {
            control.Owner = this.owner;
            base.Insert(index, control);
        }

        
    }
}
