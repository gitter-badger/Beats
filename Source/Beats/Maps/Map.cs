using Beats.Audio;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Beats.Maps
{
	/// <summary>
	/// Represents a map. A map is the main element of gameplay and contains
	/// hitobjects, sounds and other data required for a play session.
	/// </summary>
	public class Map
	{
		/// <summary>
		/// The name of the map.
		/// </summary>
		[JsonRequired, JsonProperty("name")]
		public string Name { get; private set; }

		[JsonRequired]
		private List<SoundData> sounds { get; set; }

		/// <summary>
		/// The sounds that are played on this map, including the music as well as other sound effects.
		/// </summary>
		public ReadOnlyCollection<SoundData> Sounds { get; private set; }

		/// <summary>
		/// Constructs a new, empty map.
		/// </summary>
		public Map()
		{
			sounds = new List<SoundData>();
			Sounds = new ReadOnlyCollection<SoundData>(sounds);
		}
	}
}