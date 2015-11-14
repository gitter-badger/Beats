using Beats.Graphics;
using Beats.Sprites;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System;

namespace Beats
{
	/// <summary>
	/// The main window for Beats.
	/// </summary>
	public class Window : GameWindow
	{
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

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			fixViewPort();
		}
		protected override void OnRenderFrame(FrameEventArgs e)
		{
			base.OnRenderFrame(e);

			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

			SwapBuffers();
		}
		protected override void OnUpdateFrame(FrameEventArgs e)
		{
			base.OnUpdateFrame(e);
		}
	}
}