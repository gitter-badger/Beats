using Beats.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beats.Maps
{
    public static class MapManager
    {
        public static List<MapSetMetadata> GetMapSets(string directory = "Maps")
        {
            List<MapSetMetadata> sets = new List<MapSetMetadata>();
            foreach(string setDir in IOHelper.GetDirectories(directory))
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
