using Microsoft.AspNetCore.Mvc;

namespace DemoMvc.Controllers
{
    public class DemoController : Controller
    {
        public IActionResult Index()
        {
            string message = "Hello from Demo Controller!";
            ViewBag.Message = message;
            return View();
        }

        [HttpPost]
        public IActionResult SendData(string hoTen, string diaChi)
        {
            string strOutput = "Hello " + hoTen;
            ViewBag.Message = strOutput;
            return View();
        }
    }
}