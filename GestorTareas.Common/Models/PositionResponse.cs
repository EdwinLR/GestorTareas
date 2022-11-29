using Newtonsoft.Json;

namespace GestorTareas.Common.Models
{
    public class PositionResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("workers")]
        public object? Workers { get; set; }
    }
}
