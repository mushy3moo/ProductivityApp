using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ProductivityApp.Models
{
    public class MilestoneModel
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
        public List<AttachmentModel> Attachments { get; set; }

        [JsonProperty("parentMilestone")]
        public MilestoneModel ParentMilestone { get; set; }

        [JsonProperty("childMilestones")]
        public List<MilestoneModel> ChildMilestones { get; set; }

        [JsonProperty("childObjectives")]
        public List<ObjectiveModel> ChildObjectives { get; set; }

        public MilestoneModel()
        {
            CreatedOn = DateTime.Now;
            IsCompleted = false;
        }
    }
}
