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

		public MainMenuScene(Window window)
			: base(window)
		{
			Button menuButton = new Button("Song Select");
			menuButton.X = 200f;
			menuButton.Y = 200f;
			addChild(menuButton);

			Button exitButton = new Button("Exit");
			exitButton.X = 200f;
			exitButton.Y = 350f;
			addChild(exitButton);
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
