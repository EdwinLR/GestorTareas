using Newtonsoft.Json;

namespace GestorTareas.Common.Models
{
    public class CategoryResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("categoryName")]
        public string CategoryName { get; set; }
    }
}
