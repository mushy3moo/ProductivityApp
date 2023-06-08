using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityApp.Models
{
    public class ObjectiveModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public string Icon { get; set; }
        public bool IsCompleted { get; set; }
        public ObjectiveModel ParentTask { get; set; }
        public List<ObjectiveModel> AttachedTasks { get; set; }
    }
}
