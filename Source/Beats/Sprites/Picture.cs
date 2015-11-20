using Beats.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beats.Sprites
{
	/// <summary>
	/// A simple sprite that can be used to display static textures on screen.
	/// </summary>
	public class Picture : Sprite, IDisposable
	{
		private Texture texture;

		public int TextureWidth => texture.Width;
		public int TextureHeight => texture.Height;

		private Picture()
		{
		}
		/// <summary>
		/// Constructs a new picture with the given texture.
		/// </summary>
		/// <param name="texture">The texture that the picture should use.</param>
		public Picture(Texture texture)
			: this()
		{
			if (texture == null)
				throw new ArgumentNullException(nameof(texture));

			this.texture = texture;
		}

		protected override void draw()
		{
			base.draw();
			texture.Draw();
		}

		/// <summary>
		/// Disposes of the internal texture the picture uses.
		/// </summary>
		public void Dispose()
		{
			texture.Dispose();
			texture = null;
		}
	}
}
