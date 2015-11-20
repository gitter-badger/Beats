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

		/// <summary>
		/// Horizontal size factor. Default is 1.0.
		/// </summary>
		public float SizeX { get; set; }
		/// <summary>
		/// Vertical size factor. Default is 1.0.
		/// </summary>
		public float SizeY { get; set; }

		/// <summary>
		/// Rotation of the sprite, in radians.
		/// </summary>
		public float Rotation { get; set; }

		/// <summary>
		/// Origin of the sprite, ie point of rotation/scaling.
		/// </summary>
		public float OriginX { get; set; }
		/// <summary>
		/// Origin of the sprite, ie point of rotation/scaling.
		/// </summary>
		public float OriginY { get; set; }

		private int colorBeforeTransform;
		/// <summary>
		/// The color of the sprite. Every pixels color that is drawn by this sprite will be multiplied with this color. Default is White.
		/// </summary>
		public Color Color { get; set; }

		/// <summary>
		/// True if this sprite should fire mouse events such as MouseOver, MouseEnabled, etc.
		/// </summary>
		public bool MouseEvents { get; protected set; }

		public Sprite Parent { get; private set; }

		private HashSet<Sprite> children;

		public Sprite()
		{
			Color = Color.White;
			SizeX = 1f;
			SizeY = 1f;

			children = new HashSet<Sprite>();
		}

		protected void addChild(Sprite sprite)
		{
			if (sprite == null)
				throw new ArgumentNullException(nameof(sprite));
			if (sprite.Parent != null)
				throw new InvalidOperationException("The given sprite already has a parent.");

			sprite.Parent = this;
			children.Add(sprite);
		}
		protected void removeChild(Sprite sprite)
		{
			if (sprite.Parent != this)
				throw new InvalidOperationException("The given sprite is not a child of this sprite.");

			children.Remove(sprite);
			sprite.Parent = null;
		}

		/// <summary>
		/// Transforms the global OpenGL-State as necessary before committing to draw the geometry for the sprite.
		/// </summary>
		protected virtual void transform()
		{
			GL.PushMatrix();

			GL.Translate(X, Y, 0f);
			GL.Scale(SizeX, SizeY, 0f);
			GL.Rotate(Rotation, 0f, 0f, 1f);
			GL.Translate(OriginX, OriginY, 0f);
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
			foreach (Sprite sprite in children)
				sprite.Draw();
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
			foreach (Sprite sprite in children)
				sprite.Update();
		}
	}
}
