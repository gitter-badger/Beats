using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beats.Sprites
{
	class Button:Sprite
	{
		private String buttonText;
		private Picture buttonPicture;
		private float sizeX;

		public Button(String text, float Xpos, float Ypos, Picture pic)
		{
			buttonPicture = pic;
			buttonText = text;
			X = Xpos;
			Y = Ypos;
		}

		protected override void draw()
		{
			buttonPicture.Draw();
		}
	}
}
