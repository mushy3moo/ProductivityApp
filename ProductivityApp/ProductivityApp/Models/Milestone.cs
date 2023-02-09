using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ProductivityApp.Models
{
    public class Milestone
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("createdOn")]
        public DateTime CreatedOn { get; }

        [JsonProperty("deadline")]
        public DateTime Deadline { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("isComplete")]
        public bool IsCompleted { get; set; }

        [JsonProperty("attachments")]
        public List<Attachment> Attachments { get; set; }

        [JsonProperty("parentMilestone")]
        public Milestone ParentMilestone { get; set; }

        [JsonProperty("childMilestones")]
        public List<Milestone> ChildMilestones { get; set; }

        [JsonProperty("childObjectives")]
        public List<Objective> ChildObjectives { get; set; }

        public Milestone()
        {
            CreatedOn = DateTime.Now;
            IsCompleted = false;
        }
    }
}
