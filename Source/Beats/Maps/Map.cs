using Beats.Audio;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Beats.Maps
{
	/// <summary>
	/// Represents a map. A map is the main element of gameplay and contains
	/// hitobjects, sounds and other data required for a play session.
	/// </summary>
	public class Map : IDisposable
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
		/// The mapset this map belongs to.
		/// </summary>
		public MapSetMetadata MapSet { get; private set; }

		private bool isDisposed;

		/// <summary>
		/// Constructs a new, empty map.
		/// </summary>
		public Map(MapSetMetadata mapSet)
		{
			if (mapSet == null)
				throw new ArgumentNullException(nameof(mapSet));

			MapSet = mapSet;
			sounds = new List<SoundData>();
			Sounds = new ReadOnlyCollection<SoundData>(sounds);
		}

		[OnDeserialized]
		private void onDeserialized(StreamingContext context)
		{
			foreach(SoundData soundData in Sounds)
			{
				soundData.Path = MapSet.Path;
			}
		}

		/// <summary>
		/// Disposes unmanaged resources used by objects of the map.
		/// </summary>
		public void Dispose()
		{
			if (isDisposed)
				throw new ObjectDisposedException(ToString());
			isDisposed = true;
			foreach(SoundData soundData in Sounds)
			{
				soundData.Dispose();
			}
			sounds.Clear();
		}

		public override string ToString()
		{
			return $"Map {Name}";
		}
	}
}