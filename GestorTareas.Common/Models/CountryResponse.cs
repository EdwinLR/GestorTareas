using Newtonsoft.Json;

namespace GestorTareas.Common.Models
{
    public class CountryResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("countryName")]
        public string CountryName { get; set; }
    }
}
