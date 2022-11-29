using Newtonsoft.Json;

namespace GestorTareas.Common.Models
{
    public class GenderResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("genderName")]
        public string GenderName { get; set; }

        [JsonProperty("students")]
        public object? Students { get; set; }
    }
}
