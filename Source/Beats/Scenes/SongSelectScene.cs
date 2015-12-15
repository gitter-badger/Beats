using Beats.Align;
using Beats.Maps;
using Beats.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beats.Scenes
{
	/// <summary>
	/// Represents the song select scene that displays a list of all available maps.
	/// </summary>
	public class SongSelectScene : Scene
	{
		public override string Name => "Song Select";

		/// <summary>
		/// Constructs a new song select scene for the given window.
		/// </summary>
		/// <param name="window">The window for the song select scene.</param>
		public SongSelectScene(Window window)
			: base(window)
		{
			foreach(MapSetMetadata mapSet in MapManager.GetMapSets())
			{
				foreach(Map map in mapSet.GetMaps())
				{
					Button mapBtn = new Button(map.Name);
					mapBtn.Align(
						new RelativeAlignment(mapBtn, window, true)
						{
							OffsetX = 20,
							OffsetY = 20
						}
					);
					addChild(mapBtn);
				}
			}
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
