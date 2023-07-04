using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityApp.Models
{
    public class ObjectiveModel : IModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("deadline")]
        public DateTime Deadline { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("isCompleted")]
        public bool IsCompleted { get; set; }

        [JsonProperty("parentTasks")]
        public ObjectiveModel ParentTasks { get; set; }

        [JsonProperty("attachedTasks")]
        public List<ObjectiveModel> AttachedTasks { get; set; }
    }
}
