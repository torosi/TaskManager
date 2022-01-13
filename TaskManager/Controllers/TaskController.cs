﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
                ProjectId = project
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
                    Name = t.Name,
                    Priority = t.Priority,
                    Description = t.Description,
                    UserId = t.UserId,
                    ProjectId = t.ProjectId
                };
                viewModels.Tasks.Add(viewModel);
            }

            viewModels.Tasks.OrderBy(x => x.Priority);

            return View("MyTasks", viewModels);
        }

    }
}
