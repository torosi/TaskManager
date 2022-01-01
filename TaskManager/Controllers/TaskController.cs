using Microsoft.AspNetCore.Mvc;

namespace TaskManager.Controllers
{
    public class TaskController : Controller
    {
        public IActionResult AddNewTask()
        {
            return View();
        }
    }
}
