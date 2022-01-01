using Microsoft.AspNetCore.Mvc;
using TaskManager.Models;
using TaskManager.Models.AppDBContext;
using TaskManager.Models.ViewModels;

namespace TaskManager.Controllers
{
    public class TaskController : Controller
    {

        private readonly TaskDbContext _context;

        public TaskController(TaskDbContext context)
        {
            _context = context;
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
                Priority = viewModel.Priority
            };

            _context.Add(model);
            _context.SaveChanges();

            return RedirectToAction("AddNewTask");
        }


    }
}
