using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beats.Sprites;

namespace Beats.Scenes
{
	public class MainMenuScene : Scene
	{
		public override string Name
		{
			get
			{
				return "Main Menu";
			}
		}

		public override bool CanTransitionTo(Scene scene)
		{
			throw new NotImplementedException();
		}

		public override void Reset()
		{
			sprites = new List<Sprite>();
		}
	}
}
