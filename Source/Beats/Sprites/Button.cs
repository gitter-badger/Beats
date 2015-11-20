using Beats.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beats.Sprites
{
	public class Button : Sprite
	{
		private String buttonText;
		private Picture sideOut;
		private Picture middleOut;
		private Picture captionText;

		private int width;
		public int Width
		{
			get { return width; }
			set
			{
				if (value < sideOut.TextureWidth * 2)
					value = sideOut.TextureWidth * 2;

				width = value;
				middleOut.SizeX = width - sideOut.TextureWidth * 2;
			}
		}

		private int height;
		public int Height
		{
			get { return height; }
			set
			{
				float sizeY = (float)value / (float)sideOut.TextureHeight;
				sideOut.SizeY = sizeY;
				middleOut.SizeY = sizeY;
				height = value;
			}
		}

		public Button(String caption)
		{
			buttonText = caption;

			sideOut = new Picture(new Texture("Assets/Skins/default/button-side-mouse-out.png"));
			middleOut = new Picture(new Texture("Assets/Skins/default/button-center-mouse-out.png"));
			middleOut.X = sideOut.TextureWidth;

			Width = 300;
			Height = 75;

			captionText = new Picture(
				new Text(
					caption,
					"Arial",
					12f,
					Color.Black,
					Width - sideOut.TextureWidth * 2,
					new StringFormat()
					{
						Alignment = StringAlignment.Center,
						LineAlignment = StringAlignment.Center
					}
				).GetTexture()
			);
			captionText.X = (Width - captionText.TextureWidth) / 2;
			captionText.Y = (Height - captionText.TextureHeight) / 2;
		}

		protected override void draw()
		{
			sideOut.X = 0f;
			sideOut.SizeX = 1f;
			sideOut.Draw();

			middleOut.Draw();
			captionText.Draw();

			sideOut.SizeX = -1f;
			sideOut.X = Width;
			sideOut.Draw();
		}
	}
}
