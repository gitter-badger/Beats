using Beats.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beats.Sprites
{
	public class Picture : Sprite, IDisposable
	{

		private Texture texture;

		private Picture()
		{
		}
		public Picture(Texture texture)
			: this()
		{
			this.texture = texture;
		}

		protected override void draw()
		{
			base.draw();
			texture.Draw();
		}

		public void Dispose()
		{
			texture.Dispose();
		}
	}
}
