using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TaskManager.Models;
using TaskManager.Models.AppDBContext;
using TaskManager.Models.ViewModels;

namespace TaskManager.Controllers
{
    public class TaskController : Controller
    {

        private readonly TaskDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;


        public TaskController(TaskDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult AddNewTask()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddNewTask(TaskViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View();

            Task model = new Task()
            {
                Id = 0,
                Name = viewModel.Name,
                Description = viewModel.Description,
                Priority = viewModel.Priority,
                UserId = _userManager.GetUserId(User),
                IsCompleted = false
            };

            _context.Add(model);
            _context.SaveChanges();

            return RedirectToAction("AddNewTask");
        }

        public IActionResult MyTasks() {
            var userId = _userManager.GetUserId(User);
            var tasks = _context.Tasks.ToList().Where(x => x.UserId == userId);

            var viewModels = new List<TaskViewModel>();

            foreach (var t in tasks)
            {
                var viewModel = new TaskViewModel()
                {
                    Id = t.Id,
                    Name = t.Name,
                    Priority = t.Priority,
                    Description = t.Description,
                    UserId = t.UserId,
                    IsCompleted = t.IsCompleted
                };
                viewModels.Add(viewModel);
            }

            viewModels.OrderBy(x => x.Priority);

            return View("MyTasks", viewModels);
        }

        [HttpPost]
        public IActionResult MarkCompleted(IEnumerable<TaskViewModel> tasks)
        {
            if (!ModelState.IsValid)
                return View("MyTasks", tasks);

            foreach (var t in tasks.Where(m => m.IsCompleted == true))
            {
                var result = _context.Tasks.SingleOrDefault(o => o.Id == t.Id);
                if (result != null)
                {
                    result.IsCompleted = true;
                    _context.SaveChanges();
                }
            }

            return View("MyTasks", tasks);
        }

    }
}
