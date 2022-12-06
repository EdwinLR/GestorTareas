using Newtonsoft.Json;

namespace GestorTareas.Common.Models
{
    public class ProjectColaboratorResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("projectId")]
        public string ProjectId { get; set; }


    }
}
