using Newtonsoft.Json;

namespace GestorTareas.Common.Models
{
    public class PriorityResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("priorityName")]
        public string PriorityName { get; set; }
    }
}
