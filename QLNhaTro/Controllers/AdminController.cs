using Microsoft.AspNetCore.Mvc;

namespace QLNhaTro.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        // 🔥 Điều hướng
        public IActionResult Phong()
        {
            return RedirectToAction("Index", "AdminPhong");
        }

        public IActionResult NguoiThue()
        {
            return RedirectToAction("Index", "AdminNguoiThue");
        }

        public IActionResult HopDong()
        {
            return RedirectToAction("Index", "AdminHopDong");
        }

        public IActionResult HoaDon()
        {
            return RedirectToAction("Index", "AdminHoaDon");
        }
    }
}