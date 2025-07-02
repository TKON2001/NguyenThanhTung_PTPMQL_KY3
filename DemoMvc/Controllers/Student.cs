using Microsoft.AspNetCore.Mvc;

namespace DemoMvc.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(string FullName, string Diachi)
        {
            string strOutput = "Xin chao " + FullName + " - " + Diachi;
            ViewBag.infoStudent = strOutput;
            return View();
        }
    }
}

