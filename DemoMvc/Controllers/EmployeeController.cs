using Microsoft.AspNetCore.Mvc;

namespace DemoMvc.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: /Employee/List/
        public IActionResult List()
        {
            return View();
        }

        // GET: /Employee/Profile/
        public IActionResult Profile()
        {
            return View();
        }
    }
}
