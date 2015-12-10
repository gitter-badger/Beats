using Beats.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Beats.Maps
{
    public class MapSetMetadata
    {
        [JsonRequired]
        private string[] maps { get; set; }

        public string Path { get; private set; }

        public MapSetMetadata(string path)
        {
            Path = path;
        }

        public List<Map> GetMaps()
        {
            List<Map> maps = new List<Map>();
            foreach(string map in this.maps)
            {
                Map metaData = new Map();
                if (!IOHelper.TryReadInto(Path + "/" + map, metaData))
                    continue;

                maps.Add(metaData);
            }
            return maps;
        }
    }
}