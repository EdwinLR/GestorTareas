using Newtonsoft.Json;

namespace GestorTareas.Common.Models
{
    public class AssignedActivityResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("studentId")]
        public string StudentId { get; set; }
    }
}
