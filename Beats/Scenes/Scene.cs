using Beats.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beats.Scenes
{
	/// <summary>
	/// Represents a scene. A scene has a list of sprites it will draw, can be reset and transition to other scenes.
	/// </summary>
	public abstract class Scene
	{
		public abstract string Name { get; }

		private List<Sprite> spriteList;

		/// <summary>
		/// Constructor for the Scene class.
		/// </summary>
		public Scene()
		{
			spriteList = new List<Sprite>();
		}

		/// <summary>
		/// Draws the scene.
		/// </summary>
		public abstract void Draw();

		/// <summary>
		/// Resets the scene.
		/// </summary>
		public abstract void Reset();
	}
}
