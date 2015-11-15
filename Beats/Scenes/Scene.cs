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

		public event EventHandler<EventArgs> TransitionFinished;

		private List<Sprite> spriteList;

		/// <summary>
		/// Constructor for the Scene class.
		/// </summary>
		public Scene()
		{
			spriteList = new List<Sprite>();
		}

		/// <summary>
		/// Returns true if this scene is allowed to transition to the given scene, false otherwise.
		/// </summary>
		/// <param name="scene">The scene that should be transitioned to.</param>
		/// <returns>True if this scene is allowed to transition to the given scene, false otherwise.</returns>
		public abstract bool CanTransitionTo(Scene scene);

		/// <summary>
		/// Plays the fadeout transition of this scene. When the transition animation has completed,
		/// the TransitionFinished event should be triggered. By default, this method does not animate anything
		/// and only triggers the TransitionFinished event.
		/// </summary>
		public virtual void TransitionOut()
		{
			if (TransitionFinished != null)
				TransitionFinished(this, new EventArgs());
		}
		/// <summary>
		/// Plays the fadein transition of this scene. When the transition animation has completed,
		/// the TransitionFinished event should be triggered. By default, this method does not animate anything
		/// and only triggers the TransitionFinished event.
		/// </summary>
		public virtual void TransitionIn()
		{
			if (TransitionFinished != null)
				TransitionFinished(this, new EventArgs());
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
