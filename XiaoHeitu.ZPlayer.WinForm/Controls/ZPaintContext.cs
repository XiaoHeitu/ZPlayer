using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XiaoHeitu.ZPlayer.WinForm.Controls
{
	public class ZPaintContext
	{
		private readonly ZGraphics graphics;
		private readonly Rectangle clipRectangle;



		public ZGraphics Graphics => graphics;
		public Rectangle ClipRectangle => clipRectangle;

		public ZPaintContext(ZGraphics graphics, Rectangle clipRectangle)
		{
			this.graphics = graphics;
			this.clipRectangle = clipRectangle;
		}
	}
}
