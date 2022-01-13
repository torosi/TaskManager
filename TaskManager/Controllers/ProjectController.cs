using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Models;
using TaskManager.Models.AppDBContext;
using TaskManager.Models.ViewModels;

namespace TaskManager.Controllers
{
    public class ProjectController : Controller
    {

        private readonly TaskDbContext _context; //_means that it is useed throughout the class. Is only a naming convention

        public ProjectController(TaskDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateNewProject()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateNewProject(ProjectViewModel project)
        {
            if (!ModelState.IsValid) //if model state isnt valid, return view. Do Nothing else
                return View();

            Project model = new Project()
            {
                Id = project.Id,
                Name = project.Name
            };

            _context.Add(model);
            _context.SaveChanges();

            return RedirectToAction("CreateNewProject");

        }


    }
}
