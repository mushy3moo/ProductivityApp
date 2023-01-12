using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityApp.Models
{
    public class Objective
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public string Icon { get; set; }
        public bool IsCompleted { get; set; }
        public Objective ParentTask { get; set; }
        public List<Objective> AttachedTasks { get; set; }
    }
}
