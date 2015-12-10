using Newtonsoft.Json;

namespace Beats.Maps
{
    public class Map
    {
        [JsonRequired, JsonProperty("name")]
        public string Name { get; private set; }
    }
}