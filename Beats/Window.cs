using Beats.Graphics;
using Beats.Sprites;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System;
using System.Collections.Generic;
using Beats.Scenes;

namespace Beats
{
	/// <summary>
	/// The main window for Beats.
	/// </summary>
	public class Window : GameWindow
	{
		private List<Scene> activeScenes;

		/// <summary>
		/// Constructs the main window.
		/// </summary>
		public Window()
			: base(1024, 768)
		{
			fixViewPort();
			GL.Disable(EnableCap.Lighting);
			GL.Enable(EnableCap.Texture2D);
			GL.EnableClientState(ArrayCap.VertexArray);
			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

			activeScenes = new List<Scene>();
		}

		private void fixViewPort()
		{
			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadIdentity();
			GL.Ortho(0, Width, 0, Height, -1, 1);
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

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			fixViewPort();
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
		}
	}
}