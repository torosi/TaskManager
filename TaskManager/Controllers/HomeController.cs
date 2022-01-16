using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using TaskManager.Models;
using TaskManager.Models.AppDBContext;
using TaskManager.Models.ViewModels;

namespace TaskManager.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {

        public HomeController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, TaskDbContext taskDbContext) : base(userManager, signInManager, taskDbContext)
        {
        }

    public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
            var allTasks = _context.Tasks.ToList().Where(x => x.UserId == userId);
            ViewBag.Tasks = allTasks.Count();

            var allProjects = _context.Projects.ToList();
            ViewBag.Projects = allProjects.Count();

            var annualTasks = allTasks.Where(x => x.CreatedDate.Year == DateTime.Now.Year);
            int[] annualCounts = {
                    annualTasks.Where(a => a.CreatedDate.Month == 1).Count(),
                    annualTasks.Where(a => a.CreatedDate.Month == 2).Count(),
                    annualTasks.Where(a => a.CreatedDate.Month == 3).Count(),
                    annualTasks.Where(a => a.CreatedDate.Month == 4).Count(),
                    annualTasks.Where(a => a.CreatedDate.Month == 5).Count(),
                    annualTasks.Where(a => a.CreatedDate.Month == 6).Count(),
                    annualTasks.Where(a => a.CreatedDate.Month == 7).Count(),
                    annualTasks.Where(a => a.CreatedDate.Month == 8).Count(),
                    annualTasks.Where(a => a.CreatedDate.Month == 9).Count(),
                    annualTasks.Where(a => a.CreatedDate.Month == 10).Count(),
                    annualTasks.Where(a => a.CreatedDate.Month == 11).Count(),
                    annualTasks.Where(a => a.CreatedDate.Month == 12).Count()
            };
            var viewModel = new DashboardViewModel()
            {
                Tasks = annualCounts
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
