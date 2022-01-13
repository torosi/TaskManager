using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using TaskManager.Models;
using TaskManager.Models.AppDBContext;

namespace TaskManager.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        private readonly TaskDbContext _context;

        public HomeController(TaskDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager) : base(userManager, signInManager)
        {
            _context = context;
        }

    public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
            var tasks = _context.Tasks.ToList().Where(x => x.UserId == userId);
            ViewBag.Count = tasks.Count();
            return View();
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
