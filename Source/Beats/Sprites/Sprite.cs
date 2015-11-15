using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beats.Sprites
{
	/// <summary>
	/// Represents a sprite. A sprite is the most basic element. It can be drawn and transformed.
	/// </summary>
	public abstract class Sprite
	{
		/// <summary>
		/// The x position of the sprite, in pixels.
		/// </summary>
		public float X { get; set; }
		/// <summary>
		/// The y position of the sprite, in pixels.
		/// </summary>
		public float Y { get; set; }

		private int colorBeforeTransform;
		/// <summary>
		/// The color of the sprite. Every pixels color that is drawn by this sprite will be multiplied with this color. Default is White.
		/// </summary>
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
			GL.GetInteger(GetPName.CurrentColor, out colorBeforeTransform);
			GL.Color4(Color);
		}
		/// <summary>
		/// Undos transformations of the global OpenGL-State as necessary after drawing the geometry for the sprite.
		/// </summary>
		protected virtual void untransform()
		{
			GL.Color4(Color.FromArgb(colorBeforeTransform));
			GL.PopMatrix();
		}
		/// <summary>
		/// Draws the geometry of the sprite.
		/// </summary>
		protected virtual void draw()
		{

		}

		/// <summary>
		/// Draws the sprite with all its transformations applied.
		/// </summary>
		public void Draw()
		{
			transform();
			draw();
			untransform();
		}

		/// <summary>
		/// Updates the sprite.
		/// </summary>
		public virtual void Update()
		{

		}
	}
}
