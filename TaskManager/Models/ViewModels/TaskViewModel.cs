using System;
using System.Collections.Generic;

namespace TaskManager.Models.ViewModels
{
    public class TaskViewModel //individual tasks
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public string UserId { get; set; }
        public int ProjectId { get; set; }
        public IEnumerable<int> Projects { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class TaskViewModels //collection of tasks
    {
        public List<TaskViewModel> Tasks { get; set; }
    }
}
