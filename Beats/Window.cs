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

		}

		protected override void OnRenderFrame(FrameEventArgs e)
		{
			base.OnRenderFrame(e);

			GL.ClearColor(Color.Black);
			GL.Clear(ClearBufferMask.ColorBufferBit);

			SwapBuffers();
		}
		protected override void OnUpdateFrame(FrameEventArgs e)
		{
			base.OnUpdateFrame(e);
		}
	}
}