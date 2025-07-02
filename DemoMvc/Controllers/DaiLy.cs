using Microsoft.AspNetCore.Mvc;

namespace DemoMvc.Controllers
{
    public class DaiLyController : Controller
    {
        // GET: /DaiLy/Index/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string MaDaiLy, string TenDaiLy, string DiaChi, string NguoiDaiDien, string DienThoai, string MaHTPP)
        {
            string strOutput = "Xin chao " + MaDaiLy + " - " + TenDaiLy + " - " + DiaChi + " - " + NguoiDaiDien + " - " + DienThoai + " - " + MaHTPP;
            ViewBag.infoDaiLy = strOutput;
            return View();
        }
    }
}