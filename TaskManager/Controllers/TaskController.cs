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
                Name = viewModel.Name,
                Description = viewModel.Description,
                Priority = viewModel.Priority,
                UserId = _userManager.GetUserId(User)
            };

            _context.Add(model);
            _context.SaveChanges();

            return RedirectToAction("AddNewTask");
        }

        public IActionResult MyTasks() {
            var userId = _userManager.GetUserId(User);
            var tasks = _context.Tasks.ToList().Where(x => x.UserId == userId);

            var viewModels = new TaskViewModels();

            foreach (var t in tasks)
            {
                var viewModel = new TaskViewModel()
                {
                    Name = t.Name,
                    Priority = t.Priority,
                    Description = t.Description,
                    UserId = t.UserId
                };
                viewModels.Tasks.Add(viewModel);
            }

            viewModels.Tasks.OrderBy(x => x.Priority);

            return View("MyTasks", viewModels);
        }

    }
}
