using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beats.Graphics
{
	public class Text
	{
		private static readonly Bitmap bmp = new Bitmap(1, 1);
		private static System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bmp);

		private Texture texture;

		public int Width => texture.Width;
		public int Height => texture.Height;

		public Text(string text, string fontFamily, float emSize, int maxWidth, StringFormat format)
		{
			// cache?
			Font font = new Font(fontFamily, emSize);
			SizeF stringSize = graphics.MeasureString(text, font, maxWidth, format);
			Bitmap bmp = new Bitmap((int)stringSize.Width + 1, (int)stringSize.Height + 1);
			System.Drawing.Graphics gfx = System.Drawing.Graphics.FromImage(bmp);
			gfx.DrawString(text, font, Brushes.White, new PointF(0, 0));

			texture = new Texture(bmp);
		}

		public void Draw()
		{
			texture.Draw();
		}
	}
}
