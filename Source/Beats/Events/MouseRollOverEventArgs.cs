using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beats.Events
{
	public class MouseRollOverEventArgs : EventArgs
	{
		private MouseMoveEventArgs source;

		public Point Position => source.Position;
		public int X => source.X;
		public int Y => source.Y;

		public int XDelta => source.XDelta;
		public int YDelta => source.YDelta;

		public MouseState Mouse => source.Mouse;

		public MouseRollOverEventArgs(MouseMoveEventArgs e)
		{
			source = e;
		}
	}
}
