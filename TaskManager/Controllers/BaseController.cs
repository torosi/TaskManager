using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Models.AppDBContext;

namespace TaskManager.Controllers
{
    public class BaseController : Controller
    {
        protected readonly UserManager<IdentityUser> _userManager;
        protected readonly SignInManager<IdentityUser> _signInManager;
        protected readonly TaskDbContext _context;

        public BaseController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, TaskDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public BaseController()
        {
        }

    }
}
