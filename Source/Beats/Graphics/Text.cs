using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beats.Graphics
{
	/// <summary>
	/// Represents text that can be drawn.
	/// </summary>
	public class Text : IDrawable, IDisposable
	{
		private static readonly Bitmap bmp = new Bitmap(1, 1);
		private static System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bmp);

		private Texture texture;

		/// <summary>
		/// The width of this text.
		/// </summary>
		public int Width => texture.Width;

		/// <summary>
		/// The height of this text.
		/// </summary>
		public int Height => texture.Height;

		/// <summary>
		/// Constructs new drawable text for the given text with the given font-family, font-size and string format.
		/// The text will be restricted in width to the given maxWidth.
		/// </summary>
		/// <param name="text">The text that should be drawn.</param>
		/// <param name="fontFamily">The font family with which the text should be drawn.</param>
		/// <param name="emSize">The size at which the text should be drawn.</param>
		/// <param name="color">The color the text should be drawn with.</param>
		/// <param name="maxWidth">The maximum width the text should fit in.</param>
		/// <param name="format">Formatting options for the drawn text.</param>
		public Text(string text, string fontFamily, float emSize, Color color, int maxWidth, StringFormat format)
		{
			// cache?
			Font font = new Font(fontFamily, emSize);
			SizeF stringSize = graphics.MeasureString(text, font, maxWidth, format);
			Bitmap bmp = new Bitmap((int)stringSize.Width + 1, (int)stringSize.Height + 1);
			System.Drawing.Graphics gfx = System.Drawing.Graphics.FromImage(bmp);
			gfx.DrawString(text, font, new SolidBrush(color), new PointF(0, 0));

			texture = new Texture(bmp);
		}

		/// <summary>
		/// Draws this text.
		/// </summary>
		public void Draw()
		{
			texture.Draw();
		}

		/// <summary>
		/// Gets the texture that was drawn for this text.
		/// </summary>
		/// <returns>The texture that was drawn for this text.</returns>
		public Texture GetTexture()
		{
			return texture;
		}

		/// <summary>
		/// Frees the texture that was drawn for this text. If you used GetTexture() to retrieve the texture, be
		/// aware that the texture returned from that call is going to be disposed.
		/// </summary>
		public void Dispose()
		{
			texture.Dispose();
		}
	}
}
