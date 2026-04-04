using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLNhaTro.Data;
using QLNhaTro.Models;
using QLNhaTro.Services;

namespace QLNhaTro.Controllers
{
    public class AdminHopDongController : Controller
    {
        private readonly HopDongService _service;
        private readonly NhaTroDbContext _context;

        public AdminHopDongController(HopDongService service, NhaTroDbContext context)
        {
            _service = service;
            _context = context;
        }

        public IActionResult Index(string keyword)
        {
            var data = _service.Search(keyword);
            ViewBag.Keyword = keyword;
            return View(data);
        }

        public IActionResult Details(int id)
        {
            
            var hd = _service.GetById(id);
            if (hd == null) return NotFound();
            return View(hd);
        }


        [HttpGet]
        public IActionResult Create()
        {
            // Load danh sách Phòng để chọn
            ViewBag.PhongId = new SelectList(_context.Phongs.ToList(), "PhongId", "TenPhong");

            // Load danh sách Người thuê để chọn (người đại diện ký hợp đồng)
            ViewBag.NguoiThueId = new SelectList(_context.NguoiThues.ToList(), "NguoiThueId", "NguoiThueName");

            return View();
        }
        [HttpPost]
        public IActionResult Create(HopDong model)
        {
            // Kiểm tra xem ID có hợp lệ không trước khi gọi Service
            if (model.PhongId == 0 || model.NguoiThueId == 0)
            {
                ModelState.AddModelError("", "Vui lòng chọn đầy đủ Phòng và Người thuê!");
            }

            if (ModelState.IsValid)
            {
                _service.Create(model); // Nếu lỗi ở đây, nó sẽ văng ra trang Error
                return RedirectToAction(nameof(Index));
            }

            // Nếu lỗi, phải nạp lại ViewBag để hiển thị lại Dropdown
            ViewBag.PhongId = new SelectList(_context.Phongs.ToList(), "PhongId", "TenPhong", model.PhongId);
            ViewBag.NguoiThueId = new SelectList(_context.NguoiThues.ToList(), "NguoiThueId", "NguoiThueName", model.NguoiThueId);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public List<HopDong> Search(string keyword)
        {
            // 1. Khởi tạo query và Include các bảng liên quan trực tiếp
            // Chúng ta dùng IQueryable để lọc dữ liệu dưới Database trước khi ToList()
            var query = _context.HopDongs
                                .Include(h => h.Phong)
                                .Include(h => h.NguoiThue)
                                .AsQueryable();

            // 2. Kiểm tra từ khóa tìm kiếm
            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = keyword.Trim().ToLower();

                // 3. Tìm kiếm theo Tên người thuê hoặc Tên phòng
                query = query.Where(h =>
                    (h.NguoiThue.NguoiThueName != null && h.NguoiThue.NguoiThueName.ToLower().Contains(keyword)) ||
                    (h.Phong.TenPhong != null && h.Phong.TenPhong.ToLower().Contains(keyword))
                );
            }

            // 4. Trả về kết quả cuối cùng
            return query.ToList();
        }

        //[HttpPost]
        //public IActionResult Edit(HopDong model)
        //{
        //    _service.Update(model);
        //    return RedirectToAction(nameof(Index));
        //}

        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}