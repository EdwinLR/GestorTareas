using Newtonsoft.Json;
using System;

namespace GestorTareas.Common.Models
{
    public class ConvocationResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("startingDate")]
        public DateTime StartingDate { get; set; }

        [JsonProperty("endingDate")]
        public DateTime EndingDate { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("requirements")]
        public string Requirements { get; set; }

        [JsonProperty("prizes")]
        public string Prizes { get; set; }

        [JsonProperty("convocationUrl")]
        public string ConvocationUrl { get; set; }

        [JsonProperty("institute")]
        public string Institute { get; set; }
    }
}
