using Beats.Graphics;
using Beats.Sprites;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System;
using System.Collections.Generic;
using Beats.Scenes;
using OpenTK.Input;
using System.Threading;
using Beats.Events;
using Beats.Align;

namespace Beats
{
	/// <summary>
	/// The main window for Beats.
	/// </summary>
	public class Window : GameWindow, IBox
	{
		private List<Scene> activeScenes;
		private HashSet<Sprite> hoveredSprites;

		/// <summary>
		/// The x position of this window relative to the screen.
		/// </summary>
		public new int X
		{
			get { return X; }
			set { X = value; XChanged.Trigger(); }
		}
		/// <summary>
		/// The y position of this window relative to the screen.
		/// </summary>
		public new int Y
		{
			get { return Y; }
			set { Y = value; YChanged.Trigger(); }
		}

		/// <summary>
		/// Triggers after the x coordinate of this window changed.
		/// </summary>
		public event Action XChanged;
		/// <summary>
		/// Triggers after the y coordinate of this window changed.
		/// </summary>
		public event Action YChanged;
		/// <summary>
		/// Triggers after the width of this window changed.
		/// </summary>
		public event Action WidthChanged;
		/// <summary>
		/// Triggers after the height of this window changed.
		/// </summary>
		public event Action HeightChanged;

		/// <summary>
		/// Constructs the main window.
		/// </summary>
		public Window()
			: base(800, 600)
		{
			fixViewPort();
			GL.Disable(EnableCap.Lighting);
			GL.Enable(EnableCap.Texture2D);
			GL.EnableClientState(ArrayCap.VertexArray);
			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

			activeScenes = new List<Scene>();
			hoveredSprites = new HashSet<Sprite>();

			MainMenuScene mainMenu = new MainMenuScene();
			activeScenes.Add(mainMenu);

			TargetRenderFrequency = 60;
			TargetUpdateFrequency = 60;
		}

		private void fixViewPort()
		{
			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadIdentity();
			GL.Ortho(-0.5, Width - 0.5, -0.5, Height - 0.5, -1, 1);
			GL.Viewport(0, 0, Width, Height);

			GL.MatrixMode(MatrixMode.Modelview);
			GL.LoadIdentity();
			GL.Scale(1f, -1f, 1f);
			GL.Translate(0.5f, 0.5f, 0f);
			GL.Translate(0f, -Height, 0f);
		}

		/// <summary>
		/// Transitions from the given old scene to the given new scene.
		/// </summary>
		/// <param name="oldScene">The old scene that should be replaced with the new scene.</param>
		/// <param name="newScene">The new scene that should replace the given old scene.</param>
		public void Transition(Scene oldScene, Scene newScene)
		{
			int oldSceneIndex = activeScenes.IndexOf(oldScene);
			if (oldSceneIndex == -1)
				throw new Exception($"Attempted to transition from non-active scene {oldScene.Name} to {newScene.Name}");

			int newSceneIndex = activeScenes.IndexOf(newScene);
			if (newSceneIndex != -1)
				throw new Exception($"Attempted to transition from {oldScene.Name} to already-active scene {newScene.Name}");

			if (!oldScene.CanTransitionTo(newScene))
				throw new Exception($"Attempted invalid transition from {oldScene.Name} to {newScene.Name}");

			EventHandler<EventArgs> onTransitionOutFinished = null; // this is necessary due to definite-assignment rules.
			onTransitionOutFinished = (sender, args) =>
			{
				activeScenes.Remove(oldScene);
				oldScene.TransitionFinished -= onTransitionOutFinished;
			};
			oldScene.TransitionFinished += onTransitionOutFinished;
			oldScene.TransitionOut();

			activeScenes.Add(newScene);
			newScene.TransitionIn();
		}

		protected override void OnKeyDown(KeyboardKeyEventArgs e)
		{
			base.OnKeyDown(e);
		}
		protected override void OnKeyUp(KeyboardKeyEventArgs e)
		{
			base.OnKeyUp(e);
		}
		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			base.OnKeyPress(e);
		}

		protected override void OnMouseMove(MouseMoveEventArgs e)
		{
			foreach (Scene scene in activeScenes)
			{
				recursiveCollisionCheck(
					scene,
					e.X,
					e.Y,
					x => x.MouseEvents,
					x =>
					{
						if (hoveredSprites.Add(x))
							x.SendEvent(new MouseRollOverEventArgs(e));
						x.SendEvent(e);
					},
					x =>
					{
						if (hoveredSprites.Remove(x))
							x.SendEvent(new MouseRollOutEventArgs(e));
					}
				);
			}

			base.OnMouseMove(e);
		}
		protected override void OnMouseDown(MouseButtonEventArgs e)
		{
			foreach (Scene scene in activeScenes)
				recursiveCollisionCheck(scene, e.X, e.Y, x => x.MouseEvents, x => x.SendEvent(e));

			base.OnMouseDown(e);
		}
		protected override void OnMouseUp(MouseButtonEventArgs e)
		{
			foreach (Scene scene in activeScenes)
				recursiveCollisionCheck(scene, e.X, e.Y, x => x.MouseEvents, x => x.SendEvent(e));

			base.OnMouseUp(e);
		}
		protected override void OnMouseWheel(MouseWheelEventArgs e)
		{
			foreach (Scene scene in activeScenes)
				recursiveCollisionCheck(scene, e.X, e.Y, x => x.MouseEvents, x => x.SendEvent(e));

			base.OnMouseWheel(e);
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			fixViewPort();
			WidthChanged.Trigger();
			HeightChanged.Trigger();
		}
		protected override void OnRenderFrame(FrameEventArgs e)
		{
			base.OnRenderFrame(e);

			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

			foreach (Scene scene in activeScenes)
				scene.Draw();

			SwapBuffers();
		}
		protected override void OnUpdateFrame(FrameEventArgs e)
		{
			base.OnUpdateFrame(e);
			foreach (Scene scene in activeScenes)
				scene.Update();
		}

		private void recursiveCollisionCheck(Sprite sprite, double x, double y, Func<Sprite, bool> predicate, Action<Sprite> whenCollides, Action<Sprite> whenNotCollides = null)
		{
			if (!predicate(sprite))
				return;

			if (sprite.CheckCollision(x, y))
			{
				whenCollides(sprite);
				foreach (Sprite child in sprite.Children)
				{
					// translate coordinates to childs coordinate system for next check.
					double newX = x, newY = y;

					newX -= child.X;
					newY -= child.Y;
					newX /= child.SizeX;
					newY /= child.SizeY;

					double cosVal = Math.Cos(-(child.Rotation * (Math.PI / 180)));
					double sinVal = Math.Sin(-(child.Rotation * (Math.PI / 180)));
					double origX = newX;
					newX = newX * cosVal - newY * sinVal;
					newY = origX * sinVal + newY * cosVal;

					newX -= child.OriginX;
					newY -= child.OriginY;

					recursiveCollisionCheck(child, newX, newY, predicate, whenCollides, whenNotCollides);
				}
			}
			else if(whenNotCollides != null)
			{
				whenNotCollides(sprite);
				foreach (Sprite child in sprite.AllChildren)
					whenNotCollides(child);
			}
		}
	}
}