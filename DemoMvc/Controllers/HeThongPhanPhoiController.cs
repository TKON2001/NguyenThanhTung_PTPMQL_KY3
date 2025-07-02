using Microsoft.AspNetCore.Mvc;

namespace DemoMvc.Controllers
{
    public class HeThongPhanPhoiController : Controller
    {
        // GET: /HeThongPhanPhoi/Index/
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(string MaHTPP, string TenHTPP)
        {
            string strOutput = "Xin chao " + MaHTPP + " - " + TenHTPP;
            ViewBag.infoHeThongPhanPhoi = strOutput;
            return View();
        }
    }
}

