using Microsoft.AspNetCore.Mvc;
using DemoMvc.Models;

namespace DemoMvc.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            var model = new ErrorViewModel { RequestId = "12345" };
            return View(model);
        }
    }
}
