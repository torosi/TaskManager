using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TaskManager.Models;
using TaskManager.Models.AppDBContext;
using TaskManager.Models.ViewModels;

namespace TaskManager.Controllers
{
    public class ProjectController : BaseController
    {

        public ProjectController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, TaskDbContext taskDbContext) : base(userManager, signInManager, taskDbContext)
        {
        }

        public IActionResult Index()
        {

            var viewModels = new ProjectViewModels()
            {
                Projects = new List<ProjectViewModel>()
            };

            foreach (var p in _context.Projects.ToList())
            {
                var viewModel = new ProjectViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    UserId = p.UserId,
                };
                viewModels.Projects.Add(viewModel);
            }

            return View(viewModels);
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
                Name = project.Name,
                UserId = _userManager.GetUserId(User)
            };

            _context.Add(model);
            _context.SaveChanges();

            return RedirectToAction("Index");

        }


    }
}
