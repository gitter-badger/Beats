using Beats.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beats.Test
{
	public class TestSprite : Sprite
	{
		public void AddChild(Sprite child)
		{
			addChild(child);
		}

		public void RemoveChild(Sprite child)
		{
			removeChild(child);
		}
	}
}
