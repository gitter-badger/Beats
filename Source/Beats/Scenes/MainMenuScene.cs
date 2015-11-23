using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beats.Sprites;
using Beats.Align;

namespace Beats.Scenes
{
	/// <summary>
	/// Main menu scene that allows proceeding to start the game, exit the game, change settings, etc.
	/// </summary>
	public class MainMenuScene : Scene
	{
		public override string Name => "Main Menu";

		/// <summary>
		/// Constructs a new main menu scene for the given window.
		/// </summary>
		/// <param name="window">The window to construct the main menu scene for.</param>
		public MainMenuScene(Window window)
			: base(window)
		{
			Button menuButton = new Button("Song Select");
			menuButton.OriginX = menuButton.Width / -2f;
			menuButton.Align(
				new RelativeAlignment(menuButton, window, true)
				{
					OffsetY = 10,
					PercentX = 0.5f
				}
			);
			addChild(menuButton);

			Button exitButton = new Button("Exit");
			exitButton.OriginX = exitButton.Width / -2f;
			exitButton.Align(
				new RelativeAlignment(exitButton, menuButton, false)
				{
					OffsetY = menuButton.Height + 10
				}
			);
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
