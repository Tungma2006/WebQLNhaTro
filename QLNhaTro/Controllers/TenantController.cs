using Microsoft.AspNetCore.Mvc;
using QLNhaTro.Services;

namespace QLNhaTro.Controllers
{
    public class TenantController : Controller
    {
        private readonly HoaDonService _hoaDonService;
        private readonly HopDongService _hopDongService;

        public TenantController(
            HoaDonService hoaDonService,
            HopDongService hopDongService)
        {
            _hoaDonService = hoaDonService;
            _hopDongService = hopDongService;
        }

         
        public IActionResult Index()
        {
            var cccd = HttpContext.Session.GetString("User");

            if (string.IsNullOrEmpty(cccd))
                return RedirectToAction("Login", "Account");

            var data = _hoaDonService.GetHoaDonThangHienTai(cccd);
            return View(data);
        }

        // 📌 Danh sách hợp đồng của người thuê
        public IActionResult HopDong()
        {
            var cccd = HttpContext.Session.GetString("User");

            if (string.IsNullOrEmpty(cccd))
                return RedirectToAction("Login", "Account");

            var data = _hopDongService.GetByNguoiThue(cccd);
            return View(data);
        }

        // 📌 Chi tiết hợp đồng
        public IActionResult HopDongDetail(int id)
        {
            var hd = _hopDongService.GetById(id);
            if (hd == null) return NotFound();

            return View(hd);
        }

        // 📌 Chi tiết hóa đơn
        public IActionResult HoaDonDetail(int id)
        {
            var hd = _hoaDonService.GetById(id);
            if (hd == null) return NotFound();

            return View(hd);
        }
    }
}