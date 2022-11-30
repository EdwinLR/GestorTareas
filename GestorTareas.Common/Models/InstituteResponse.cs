using Newtonsoft.Json;

namespace GestorTareas.Common.Models
{
    public class InstituteResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("contactPhone")]
        public string? ContactPhone { get; set; }

        [JsonProperty("streetName")]
        public string? StreetName { get; set; }

        [JsonProperty("streetNumber")]
        public string? StreetNumber { get; set; }

        [JsonProperty("district")]
        public string? District { get; set; }

        [JsonProperty("city")]
        public string? City { get; set; }

        [JsonProperty("country")]
        public string? Country { get; set; }

        [JsonProperty("contactPeople")]
        public object? ContactPeople { get; set; }

        [JsonProperty("convocations")]
        public object? Convocations { get; set; }
    }
}
