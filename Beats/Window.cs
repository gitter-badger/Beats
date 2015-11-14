using Beats.Graphics;
using Beats.Sprites;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace Beats
{
	public class Window : GameWindow
	{
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

		protected override void OnRenderFrame(FrameEventArgs e)
		{
			base.OnRenderFrame(e);

			/*GL.ClearColor(Color.Black);
			GL.Clear(ClearBufferMask.ColorBufferBit);*/

			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
			using (Picture pic = new Picture(new Texture("test.jpg")))
			{
				pic.Draw();
			}

			/**/

			SwapBuffers();
		}
		protected override void OnUpdateFrame(FrameEventArgs e)
		{
			base.OnUpdateFrame(e);
		}
	}
}