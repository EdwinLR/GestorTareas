using Newtonsoft.Json;

namespace GestorTareas.Common.Models
{
    public class StudentResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("studentId")]
        public string? StudentId { get; set; }

        [JsonProperty("career")]
        public string? Career { get; set; }

        [JsonProperty("gender")]
        public string? Gender { get; set; }

        [JsonProperty("assignedActivities")]
        public object? AssignedActivities { get; set; }

        [JsonProperty("user")]
        public object? User { get; set; }
    }
}
