using Beats.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Beats.Maps
{
	/// <summary>
	/// Represents metadata for a mapset.
	/// </summary>
	public class MapSetMetadata
	{
		[JsonRequired]
		private string[] maps { get; set; }

		/// <summary>
		/// The path to the map set on disk.
		/// </summary>
		public string Path { get; private set; }

		/// <summary>
		/// Constructs a new map set located at the given path.
		/// </summary>
		/// <param name="path">The path to the mapset.</param>
		public MapSetMetadata(string path)
		{
			Path = path;
		}

		/// <summary>
		/// Loads all maps in this mapset and returns them as a list.
		/// </summary>
		/// <returns>A list of all maps in this mapset.</returns>
		public List<Map> GetMaps()
		{
			List<Map> maps = new List<Map>();
			foreach (string map in this.maps)
			{
				Map metaData = new Map(this);
				if (!IOHelper.TryReadInto(Path + "/" + map, metaData))
					continue;

				maps.Add(metaData);
			}
			return maps;
		}
	}
}