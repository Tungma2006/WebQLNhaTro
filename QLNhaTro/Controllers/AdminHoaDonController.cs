using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QLNhaTro.Data;
using QLNhaTro.Models;
using QLNhaTro.Services;

namespace QLNhaTro.Controllers
{
    public class AdminHoaDonController : BaseController
    {
        private readonly HoaDonService _service;

        public AdminHoaDonController(NhaTroDbContext context, HoaDonService service) : base(context)
        {
            _service = service;
        }

        public IActionResult Index(string tenPhong, bool? chuaThanhToan)
        {
            LoadThongBao();

            var data = _service.Search(tenPhong, chuaThanhToan);

            ViewBag.TenPhong = tenPhong;
            ViewBag.ChuaThanhToan = chuaThanhToan;

            return View(data);
        }

        public IActionResult Details(int id)
        {
            var hd = _service.GetById(id);

            var vm = new HoaDonViewModel
            {
                HoaDonId = hd.HoaDonId,
                Thang = hd.Thang,
                SoDienTieuThu = hd.SoDienTieuThu,
                SoNuocTieuThu = hd.SoNuocTieuThu,
                TienDien = hd.TienDien,
                TienNuoc = hd.TienNuoc,
                TienPhong = hd.TienPhong,
                TongTien = hd.TongTien,
                TrangthaiThanhToan = hd.TrangthaiThanhToan,
                TenPhong = hd.HopDong?.Phong?.TenPhong
            };

            return View(vm);
        }

        public IActionResult Create()
        {
            ViewBag.HopDongs = new SelectList(_context.HopDongs, "HopDongId", "HopDongId");
            return View();
        }

        [HttpPost]
        public IActionResult Create(HoaDon model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.HopDongs = new SelectList(_context.HopDongs, "HopDongId", "HopDongId");
                return View(model);
            }

            _service.Create(model);

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int id)
        {
            var hd = _service.GetById(id);

            var vm = new HoaDonViewModel
            {
                HoaDonId = hd.HoaDonId,
                Thang = hd.Thang,
                SoDienTieuThu = hd.SoDienTieuThu,
                SoNuocTieuThu = hd.SoNuocTieuThu,
                TrangthaiThanhToan = hd.TrangthaiThanhToan,
                HopDongId = hd.HopDongId,
                TenPhong = hd.HopDong?.Phong?.TenPhong
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(HoaDonViewModel vm)
        {
            var hd = _service.GetById(vm.HoaDonId);

            // chỉ update field cần thiết
            hd.Thang = vm.Thang;
            hd.SoDienTieuThu = vm.SoDienTieuThu;
            hd.SoNuocTieuThu = vm.SoNuocTieuThu;
            hd.TrangthaiThanhToan = vm.TrangthaiThanhToan;

            _service.Update(hd);

            return RedirectToAction(nameof(Index));
        }

        

        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}