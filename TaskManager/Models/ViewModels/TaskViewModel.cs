using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Models.ViewModels
{
    public class TaskViewModel
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
    }
}
