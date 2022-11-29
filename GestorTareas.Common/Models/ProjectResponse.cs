using Newtonsoft.Json;

namespace GestorTareas.Common.Models
{
    public class ProjectResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("projectName")]
        public string ProjectName { get; set; }

        [JsonProperty("convocation")]
        public string Convocation { get; set; }

        [JsonProperty("projectCollaborators")]
        public object? ProjectCollaborators { get; set; }
    }
}
