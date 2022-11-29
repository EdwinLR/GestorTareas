using Newtonsoft.Json;

namespace GestorTareas.Common.Models
{
    public class WorkerResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("workerId")]
        public string? WorkerId { get; set; }

        [JsonProperty("position")]
        public string? Position { get; set; }

        [JsonProperty("firstName")]
        public string? FirstName { get; set; }

        [JsonProperty("fatherLastName")]
        public string? FatherLastName { get; set; }

        [JsonProperty("motherLastName")]
        public string? MotherLastName { get; set; }

        [JsonProperty("fullName")]
        public string FullName => $"{FatherLastName} {MotherLastName} {FirstName}";

        [JsonProperty("email")]
        public string? Email { get; set; }

        [JsonProperty("phoneNumber")]
        public string? PhoneNumber { get; set; }

        [JsonProperty("userId")]
        public string? UserId { get; set; }
    }
}
