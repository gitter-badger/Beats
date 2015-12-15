using Beats.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beats.Maps
{
	/// <summary>
	/// Represents the map manager. This class can be used to retrieve information about
	/// maps and mapsets.
	/// </summary>
	public static class MapManager
	{
		/// <summary>
		/// Returns a list of all mapsets located in the given directory. Default directory is "Maps".
		/// </summary>
		/// <param name="directory">The directory to search for mapsets in.</param>
		/// <returns>A list of all mapsets located in the given directory.</returns>
		public static List<MapSetMetadata> GetMapSets(string directory = "Maps")
		{
			List<MapSetMetadata> sets = new List<MapSetMetadata>();
			foreach (string setDir in IOHelper.GetDirectories(directory))
			{
				MapSetMetadata metaData = new MapSetMetadata(setDir);
				if (!IOHelper.TryReadInto(setDir + "/set.json", metaData))
					continue;

				sets.Add(metaData);
			}

			return sets;
		}
	}
}
