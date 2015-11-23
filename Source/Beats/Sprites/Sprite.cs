using Beats.Align;
using Beats.Events;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
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
	public abstract class Sprite : IPosition
	{
		private int x;
		/// <summary>
		/// The x position of the sprite, in pixels.
		/// </summary>
		public int X
		{
			get { return x; }
			set { x = value; XChanged.Trigger(); }
		}
		private int y;
		/// <summary>
		/// The y position of the sprite, in pixels.
		/// </summary>
		public int Y
		{
			get { return y; }
			set { y = value; YChanged.Trigger(); }
		}

		/// <summary>
		/// Triggers after the x coordinate of this sprite changed.
		/// </summary>
		public event Action XChanged;
		/// <summary>
		/// Triggers after the y coordinate of this sprite changed.
		/// </summary>
		public event Action YChanged;

		/// <summary>
		/// Horizontal size factor. Default is 1.0.
		/// </summary>
		public float SizeX { get; set; }
		/// <summary>
		/// Vertical size factor. Default is 1.0.
		/// </summary>
		public float SizeY { get; set; }

		/// <summary>
		/// Rotation of the sprite, in degrees.
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

		private float[] colorBeforeTransform = new float[4];
		/// <summary>
		/// The color of the sprite. Every pixels color that is drawn by this sprite will be multiplied with this color. Default is White.
		/// </summary>
		public Color Color { get; set; }

		/// <summary>
		/// True if this sprite should fire mouse events such as RollOver, MouseDown, etc.
		/// This applies transitively to all children: If a parent does not process mouse events, its
		/// childrens automatically will not process mouse events either, independent of their individual
		/// setting.
		/// </summary>
		public bool MouseEvents { get; protected set; }

		/// <summary>
		/// Allows to iterate over the immediate children of this sprite.
		/// </summary>
		public IEnumerable<Sprite> Children
		{
			get
			{
				foreach (Sprite s in children)
					yield return s;
			}
		}

		/// <summary>
		/// Allows to iterate over all children of this sprite recursively.
		/// </summary>
		public IEnumerable<Sprite> AllChildren
		{
			get
			{
				foreach (Sprite child in children)
				{
					yield return child;
					foreach (Sprite childChild in child.AllChildren)
						yield return childChild;
				}
			}
		}

		/// <summary>
		/// The parent of this sprite, or null if this sprite has no parent.
		/// </summary>
		public Sprite Parent { get; private set; }

		private HashSet<Sprite> children;

		/// <summary>
		/// Occurs when the mouse moves across the surface of the sprite.
		/// </summary>
		public event Action<MouseMoveEventArgs> MouseMove;
		/// <summary>
		/// Occurs when a mouse button is pressed or released while pointing at the sprite.
		/// </summary>
		public event Action<MouseButtonEventArgs> MouseButton;
		/// <summary>
		/// Occurs when the mouse wheel is scrolled while pointing at the sprite.
		/// </summary>
		public event Action<MouseWheelEventArgs> MouseWheel;

		/// <summary>
		/// Occurs when the mouse cursor moves over the sprite.
		/// </summary>
		public event Action<MouseRollOverEventArgs> RollOver;
		/// <summary>
		/// Occurs when the mouse cursor leaves the sprite.
		/// </summary>
		public event Action<MouseRollOutEventArgs> RollOut;

		public event Action<KeyboardKeyEventArgs> KeyDown;
		public event Action<KeyboardKeyEventArgs> KeyUp;
		public event Action<KeyPressEventArgs> KeyPress;

		private List<Alignment> alignments;

		public Sprite()
		{
			Color = Color.White;
			SizeX = 1f;
			SizeY = 1f;

			children = new HashSet<Sprite>();
			alignments = new List<Alignment>();
		}

		public void Align(Alignment alignment)
		{
			if (alignment == null)
				throw new ArgumentNullException(nameof(alignment));
			if (alignment.Target != this)
				throw new InvalidOperationException("The given alignment is not targetting this sprite.");

			alignments.Add(alignment);
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
			GL.GetFloat(GetPName.CurrentColor, colorBeforeTransform);
			GL.Color4(Color.FromArgb(
				(int)(Color.A * colorBeforeTransform[3]),
				(int)(Color.R * colorBeforeTransform[0]),
				(int)(Color.G * colorBeforeTransform[1]),
				(int)(Color.B * colorBeforeTransform[2])
			));
		}
		/// <summary>
		/// Undos transformations of the global OpenGL-State as necessary after drawing the geometry for the sprite.
		/// </summary>
		protected virtual void untransform()
		{
			GL.Color4(Color.FromArgb(
				(int)(colorBeforeTransform[3] * 255),
				(int)(colorBeforeTransform[0] * 255),
				(int)(colorBeforeTransform[1] * 255),
				(int)(colorBeforeTransform[2] * 255)
			));
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
		/// True if this sprite collides with the given point. The point is seen relative to the sprites
		/// coordinate system. This means that if a sprite is rotated by 90°, the point must be rotated
		/// by 90° around the sprites center as well.
		/// </summary>
		/// <param name="x">The x coordinate of the point to test against.</param>
		/// <param name="y">The y coordinate of the point to test against.</param>
		/// <returns>True if the point collides with the sprite, false otherwise.</returns>
		public virtual bool CheckCollision(double x, double y)
		{
			return true;
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

		public void SendEvent(MouseMoveEventArgs e)
		{
			if (MouseMove != null)
				MouseMove(e);
		}
		public void SendEvent(MouseButtonEventArgs e)
		{
			if (MouseButton != null)
				MouseButton(e);
		}
		public void SendEvent(MouseWheelEventArgs e)
		{
			if (MouseWheel != null)
				MouseWheel(e);
		}
		public void SendEvent(MouseRollOverEventArgs e)
		{
			if (RollOver != null)
				RollOver(e);
		}
		public void SendEvent(MouseRollOutEventArgs e)
		{
			if (RollOut != null)
				RollOut(e);
		}
	}
}
