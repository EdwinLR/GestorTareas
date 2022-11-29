using Newtonsoft.Json;

namespace GestorTareas.Common.Models
{
    public class StatusResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("statusName")]
        public string StatusName { get; set; }
    }
}
