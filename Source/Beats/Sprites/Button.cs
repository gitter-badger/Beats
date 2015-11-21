﻿using Beats.Events;
using Beats.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beats.Sprites
{
	public class Button : Sprite, IDisposable
	{
		private enum ButtonState
		{
			Out = 0,
			Over = 1,
			Down = 2
		}

		private String buttonText;
		private Picture captionText;

		private Picture sideOut;
		private Picture middleOut;

		private Picture sideOver;
		private Picture middleOver;

		private Picture sideDown;
		private Picture middleDown;

		private ButtonState state;

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
				middleOver.SizeX = middleOut.SizeX;
				middleDown.SizeX = middleOut.SizeX;
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
				sideOver.SizeY = sizeY;
				sideDown.SizeY = sizeY;
				middleOut.SizeY = sizeY;
				middleOver.SizeY = sizeY;
				middleDown.SizeY = sizeY;
				height = value;
			}
		}

		public Button(String caption)
		{
			MouseEvents = true;
			buttonText = caption;

			sideOut = new Picture(new Texture("Assets/Skins/default/button-side-mouse-out.png"));
			middleOut = new Picture(new Texture("Assets/Skins/default/button-center-mouse-out.png"));
			middleOut.X = sideOut.TextureWidth;

			sideOver = new Picture(new Texture("Assets/Skins/default/button-side-mouse-over.png"));
			middleOver = new Picture(new Texture("Assets/Skins/default/button-center-mouse-over.png"));
			middleOver.X = sideOver.TextureWidth;

			sideDown = new Picture(new Texture("Assets/Skins/default/button-side-mouse-down.png"));
			middleDown = new Picture(new Texture("Assets/Skins/default/button-center-mouse-down.png"));
			middleDown.X = sideDown.TextureWidth;

			Width = 300;
			Height = 75;

			state = ButtonState.Out;

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

			RollOver += Button_RollOver;
			RollOut += Button_RollOut;
		}

		private void Button_RollOut(MouseRollOutEventArgs obj)
		{
			if (state != ButtonState.Over)
				return;

			state = ButtonState.Out;
		}

		private void Button_RollOver(MouseRollOverEventArgs obj)
		{
			if (state != ButtonState.Out)
				return;

			state = ButtonState.Over;
		}

		public override bool CheckCollision(double x, double y)
		{
			if (x < 0 || y < 0)
				return false;
			if (x > Width || y > Height)
				return false;

			return true;
		}

		protected override void draw()
		{
			switch (state)
			{
				case ButtonState.Out:
					sideOut.X = 0f;
					sideOut.SizeX = 1f;
					sideOut.Draw();

					sideOut.SizeX = -1f;
					sideOut.X = Width;
					sideOut.Draw();

					middleOut.Draw();
					break;
				case ButtonState.Over:
					sideOver.X = 0f;
					sideOver.SizeX = 1f;
					sideOver.Draw();

					sideOver.SizeX = -1f;
					sideOver.X = Width;
					sideOver.Draw();

					middleOver.Draw();
					break;
				case ButtonState.Down:
					break;
			}

			captionText.Draw();
		}

		public void Dispose()
		{
			sideOut.Dispose();
			middleOut.Dispose();

			sideOver.Dispose();
			middleOver.Dispose();

			sideDown.Dispose();
			middleDown.Dispose();
		}
	}
}
