using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityApp.Models
{
    public class Milestone
    {
        public string Id { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public string Icon { get; set; }
        public bool IsCompleted { get; set; }
        public Milestone ParentMilestone { get; set; }
        public Milestone ChildMilestone { get; set; }
        public List<Objective> AttachedTasks { get; set; }
    }
}
