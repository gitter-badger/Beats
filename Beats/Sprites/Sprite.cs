using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beats.Sprites
{
	public abstract class Sprite
	{
		public float X { get; set; }
		public float Y { get; set; }

		private int colorBeforeTransform;
		public Color Color { get; set; }

		public Sprite()
		{
			Color = Color.White;
		}

		/// <summary>
		/// Transforms the global OpenGL-State as necessary before committing to draw the geometry for the sprite.
		/// </summary>
		protected virtual void transform()
		{
			GL.PushMatrix();

			GL.Translate(X, Y, 0f);
			/*GL.Scale(scale.x, scale.y, 0f);
			GL.Rotate(rotation, 0f, 0f, 1f);
			GL.Translate(origin.x, origin.y, 0f);*/
			/*GL.GetInteger(GetPName.CurrentColor, out colorBeforeTransform);
			GL.Color4(Color);*/
		}
		/// <summary>
		/// Undos transformations of the global OpenGL-State as necessary after drawing the geometry for the sprite.
		/// </summary>
		protected virtual void untransform()
		{
			//GL.Color4(Color.FromArgb(colorBeforeTransform));
			GL.PopMatrix();
		}
		/// <summary>
		/// Draws the geometry of the sprite.
		/// </summary>
		protected virtual void draw()
		{

		}

		public void Draw()
		{
			transform();
			draw();
			untransform();
		}
	}
}
