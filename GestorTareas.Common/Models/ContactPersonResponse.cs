using Newtonsoft.Json;

namespace GestorTareas.Common.Models
{
    public class ContactPersonResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("fatherLastName")]
        public string FatherLastName { get; set; }

        [JsonProperty("motherLastName")]
        public string MotherLastName { get; set; }

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("fullName")]
        public string FullName => $"{FatherLastName} {MotherLastName} {FirstName}";

        [JsonProperty("institute")]
        public string Institute { get; set; }
    }
}
