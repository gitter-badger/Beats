﻿using System;
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
			Button songSelectButton = new Button("Song Select");
			songSelectButton.OriginX = songSelectButton.Width / -2f;
			songSelectButton.Align(
				new RelativeAlignment(songSelectButton, window, true)
				{
					OffsetY = 10,
					PercentX = 0.5f
				}
			);
			songSelectButton.Clicked += SongSelectButton_Clicked;
			addChild(songSelectButton);

			Button exitButton = new Button("Exit");
			exitButton.OriginX = exitButton.Width / -2f;
			exitButton.Align(
				new RelativeAlignment(exitButton, songSelectButton, false)
				{
					OffsetY = songSelectButton.Height + 10
				}
			);
			exitButton.Clicked += ExitButton_Clicked;
			addChild(exitButton);
		}

		private void SongSelectButton_Clicked()
		{
			Window.Transition(this, new SongSelectScene(Window));
		}

		private void ExitButton_Clicked()
		{
			Window.Exit();
		}

		public override bool CanTransitionTo(Scene scene)
		{
			if (scene is SongSelectScene)
				return true;
			else
				return false;
		}

		public override void Reset()
		{
			throw new NotImplementedException();
		}
	}
}
