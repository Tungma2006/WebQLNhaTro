using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLNhaTro.Data;
using QLNhaTro.Models;
using QLNhaTro.Services;

namespace QLNhaTro.Controllers
{
    public class AdminPhongController : BaseController
    {
        private readonly PhongService _service;
        private readonly NhaTroDbContext _context;

        public AdminPhongController(NhaTroDbContext context, PhongService service) : base(context)
        {
            _service = service;
        }

        public IActionResult Index(string keyword)
        {
            LoadThongBao();
            var data = _service.Search(keyword);
            ViewBag.Keyword = keyword;
            return View(data);
        }

        public IActionResult Details(int id)
        {
            var p = _service.GetById(id);
            if (p == null) return NotFound();
            return View(p);
        }

        public IActionResult Create() => View();


        [HttpPost]
        public IActionResult Create(Phong model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); // quay lại form + giữ dữ liệu + hiển thị lỗi
            }

            _service.Create(model);

            return RedirectToAction("Index"); // 👉 quay về trang danh sách
        }

        public IActionResult Edit(int id)
        {
            return View(_service.GetById(id));
        }

        [HttpPost]
        public IActionResult Edit(Phong model)
        {
            _service.Update(model);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }

    }
}