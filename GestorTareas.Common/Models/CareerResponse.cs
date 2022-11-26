using Newtonsoft.Json;

namespace GestorTareas.Common.Models
{
    public class CareerResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("careerCode")]
        public string? CareerCode { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("students")]
        public object? Students { get; set; }
    }
}
