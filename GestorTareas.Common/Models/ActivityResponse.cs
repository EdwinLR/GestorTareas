using Newtonsoft.Json;
using System.Collections.Generic;
using System;

namespace GestorTareas.Common.Models
{
    public class ActivityResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("deadline")]
        public DateTime Deadline { get; set; }

        [JsonProperty("progress")]
        public int Progress { get; set; }

        [JsonProperty("creationDate")]
        public DateTime CreationDate { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("priority")]
        public string Priority { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("project")]
        public string Project { get; set; }

        [JsonProperty("assignedActivities")]
        public List<object>? AssignedActivities { get; set; }
    }
}
