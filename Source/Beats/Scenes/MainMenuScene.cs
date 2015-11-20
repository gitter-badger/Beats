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
		public override string Name => "Main Menu";

		public MainMenuScene()
		{
			Button menuButton = new Button("Song Select");
			menuButton.X = 50f;
			menuButton.Y = 50f;
			addChild(menuButton);
		}

		public override bool CanTransitionTo(Scene scene)
		{
			throw new NotImplementedException();
		}

		public override void Reset()
		{
			throw new NotImplementedException();
		}
	}
}
