using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using QLNhaTro.Data;
using QLNhaTro.Enums;
using QLNhaTro.Models;
using QLNhaTro.Services;

namespace QLNhaTro.Controllers
{
    public class TenantController : Controller
    {
        private readonly HoaDonService _hoaDonService;
        private readonly HopDongService _hopDongService;
        private readonly NhaTroDbContext _context;

        public TenantController(
            HoaDonService hoaDonService,
            HopDongService hopDongService,
           NhaTroDbContext context)
        {
            _hoaDonService = hoaDonService;
            _hopDongService = hopDongService;
            _context = context;
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
        [HttpPost]
        public IActionResult UploadThanhToan(int id, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest();

            // 📁 lưu file
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            // 🔍 lấy hóa đơn
            var hoaDon = _hoaDonService.GetById(id);
            if (hoaDon == null) return NotFound();

            // 💾 cập nhật hóa đơn
            hoaDon.AnhThanhToan = "/images/" + fileName;
            _hoaDonService.Update(hoaDon);

            // 🔔 👉 ĐẶT Ở ĐÂY (sau khi xử lý xong)
            _context.ThongBaos.Add(new ThongBao
            {
                NoiDung = $"Hóa đơn #{hoaDon.HoaDonId} đã gửi thanh toán",
                HoaDonId = hoaDon.HoaDonId
            });

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}