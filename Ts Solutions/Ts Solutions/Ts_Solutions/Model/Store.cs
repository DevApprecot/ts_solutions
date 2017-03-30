using Newtonsoft.Json;

namespace Ts_Solutions.Model
{
    public class Store
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("lat")]
        public string Lat { get; set; }
        [JsonProperty("lon")]
        public string Lon { get; set; }
        [JsonProperty("address")]
        public string Address { get; set; }
    }
}
