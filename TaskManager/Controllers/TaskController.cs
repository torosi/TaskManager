using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskManager.Models;
using TaskManager.Models.AppDBContext;
using TaskManager.Models.ViewModels;

namespace TaskManager.Controllers
{
    public class TaskController : BaseController
    {

        public TaskController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, TaskDbContext taskDbContext) : base(userManager, signInManager, taskDbContext)
        {
        }

        [HttpGet]
        public IActionResult AddNewTask()
        {
            var projects = _context.Projects.ToList();

            var viewModel = new TaskViewModel() {
                Projects = projects.Select(x => x.Id)
            };

            return View(viewModel); //passing in the newly created view model that now contains a list of projects
        }

        [HttpPost]
        public IActionResult AddNewTask(TaskViewModel viewModel)
        {
            if (!ModelState.IsValid) //if model state isnt valid, return view. Do Nothing else
                return View();

            var project = viewModel.Projects.FirstOrDefault();

            Task model = new Task()
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
                Priority = viewModel.Priority,
                UserId = _userManager.GetUserId(User),
                ProjectId = project,
                CreatedDate = DateTime.Now
            };

            _context.Add(model);
            _context.SaveChanges();

            return RedirectToAction("MyTasks");
        }

        public IActionResult MyTasks() {
            var userId = _userManager.GetUserId(User);
            var tasks = _context.Tasks.ToList().Where(x => x.UserId == userId); //gets the tasks from the database with the user ID

            var viewModels = new TaskViewModels()
            {
                Tasks = new List<TaskViewModel>()
            };

            foreach (var t in tasks)
            {
                var viewModel = new TaskViewModel()
                {
                    Id = t.Id,
                    Name = t.Name,
                    Priority = t.Priority,
                    Description = t.Description,
                    UserId = t.UserId,
                    ProjectId = t.ProjectId,
                    CreatedDate = t.CreatedDate
                };
                viewModels.Tasks.Add(viewModel);
            }

            viewModels.Tasks.OrderBy(x => x.Priority);

            return View("MyTasks", viewModels);
        }

        [HttpPost]
        public IActionResult EditTask(TaskViewModel task)
        {
            return View(task);
        }
        [HttpGet]
        public IActionResult EditTask(int id)
        {
            var task = _context.Tasks.ToList().Where(x => x.Id == id).FirstOrDefault();
            return View(task);
        }

    }
}
