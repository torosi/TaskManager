using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Models.ViewModels
{
    public class ProjectViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }

    }

    public class ProjectViewModels
    {
        public List<ProjectViewModel> Projects { get; set; }

    }
}
