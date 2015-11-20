using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beats.Graphics
{
	/// <summary>
	/// Represents a texture that has been loaded into graphics card memory.
	/// </summary>
	public class Texture : IDrawable, IDisposable
	{
		private uint textureId;

		private uint indexBufferId;
		private uint vertexBufferId;
		private uint texCoordBufferId;

		private int[] vertices;
		private ushort[] texCoords;

		private static byte[] indices = new byte[] { 0, 1, 2, 3 };

		/// <summary>
		/// The width of the texture, in pixels.
		/// </summary>
		public int Width { get; private set; }
		/// <summary>
		/// The height of the texture, in pixels.
		/// </summary>
		public int Height { get; private set; }

		/// <summary>
		/// Constructs a new texture. The texture will contain pixel data from the image at the given file path.
		/// </summary>
		/// <param name="filePath">The path to the image the texture should load.</param>
		public Texture(string filePath)
			: this((Bitmap)Bitmap.FromFile(filePath))
		{
			
		}
		/// <summary>
		/// Constructs a new texture. The texture will contain pixel data from the given bitmap.
		/// </summary>
		/// <param name="bmp">The bitmap with the pixel data the texture should contain.</param>
		/// <param name="disposeBitmap">True if the given bitmap should be disposed after being uploaded.</param>
		public Texture(Bitmap bmp, bool disposeBitmap = true)
		{
			if (bmp == null)
				throw new ArgumentNullException(nameof(bmp));

			Width = bmp.Width;
			Height = bmp.Height;

			BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

			textureId = (uint)GL.GenTexture();
			GL.BindTexture(TextureTarget.Texture2D, textureId);
			GL.TexImage2D(
				TextureTarget.Texture2D,
				0,
				PixelInternalFormat.Rgba,
				Width,
				Height,
				0,
				OpenTK.Graphics.OpenGL.PixelFormat.Bgra,
				PixelType.UnsignedByte,
				bmpData.Scan0
			);

			bmp.UnlockBits(bmpData);
			if (disposeBitmap)
				bmp.Dispose();

			finishConstruct();
		}
		/// <summary>
		/// Constructs a new texture. The texture will have the given width and height and be filled with the given color.
		/// </summary>
		/// <param name="color">The color the texture should be filled with.</param>
		/// <param name="width">The width the texture should have.</param>
		/// <param name="height">The height the texture should have.</param>
		public Texture(Color color, int width, int height)
		{
			Bitmap bmp = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			for (int x = 0; x < width; ++x)
				for (int y = 0; y < height; ++y)
					bmp.SetPixel(x, y, color);

			Width = bmp.Width;
			Height = bmp.Height;

			BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

			textureId = (uint)GL.GenTexture();
			GL.BindTexture(TextureTarget.Texture2D, textureId);
			GL.TexImage2D(
				TextureTarget.Texture2D,
				0,
				PixelInternalFormat.Rgba,
				Width,
				Height,
				0,
				OpenTK.Graphics.OpenGL.PixelFormat.Rgba,
				PixelType.UnsignedByte,
				bmpData.Scan0
			);
			bmp.UnlockBits(bmpData);
			finishConstruct();
		}

		private void finishConstruct()
		{
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)All.Linear);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)All.Linear);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)All.Repeat);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)All.Repeat);

			uint[] bufferIds = new uint[3];
			GL.GenBuffers(3, bufferIds);
			indexBufferId = bufferIds[0];
			vertexBufferId = bufferIds[1];
			texCoordBufferId = bufferIds[2];

			GL.BindBuffer(BufferTarget.ElementArrayBuffer, indexBufferId);
			GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(sizeof(float) * indices.Length), indices, BufferUsageHint.StaticDraw);
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);

			vertices = new int[]
			{
				0, 0,
				Width, 0,
				Width, Height,
				0, Height
			};
			GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferId);
			GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(sizeof(int) * vertices.Length), vertices, BufferUsageHint.StaticDraw);
			GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

			texCoords = new ushort[]
			{
				0, 0,
				1, 0,
				1, 1,
				0, 1
			};
			GL.BindBuffer(BufferTarget.ArrayBuffer, texCoordBufferId);
			GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(sizeof(ushort) * texCoords.Length), texCoords, BufferUsageHint.StaticDraw);
			GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
		}

		/// <summary>
		/// Draws the texture. Does not apply any transformations to anything.
		/// </summary>
		public void Draw()
		{
			GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferId);
			GL.EnableClientState(ArrayCap.VertexArray);
			GL.VertexPointer(2, VertexPointerType.Int, 0, IntPtr.Zero);

			GL.BindBuffer(BufferTarget.ArrayBuffer, texCoordBufferId);
			GL.EnableClientState(ArrayCap.TextureCoordArray);
			GL.TexCoordPointer(2, TexCoordPointerType.Short, 0, IntPtr.Zero);

			GL.BindBuffer(BufferTarget.ElementArrayBuffer, indexBufferId);

			GL.BindTexture(TextureTarget.Texture2D, textureId);

			GL.DrawElements(PrimitiveType.Quads, 4, DrawElementsType.UnsignedByte, IntPtr.Zero);

			GL.DisableClientState(ArrayCap.VertexArray);
			GL.DisableClientState(ArrayCap.TextureCoordArray);
			GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
		}

		/// <summary>
		/// Frees OpenGL-Related resources.
		/// </summary>
		public void Dispose()
		{
			GL.DeleteTexture(textureId);
			GL.DeleteBuffers(1, ref indexBufferId);
			GL.DeleteBuffers(1, ref vertexBufferId);
			GL.DeleteBuffers(1, ref texCoordBufferId);
		}
	}
}
