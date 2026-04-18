using Microsoft.AspNetCore.Mvc;
using QLNhaTro.Data;
using QLNhaTro.Models;
using QLNhaTro.Services;

namespace QLNhaTro.Controllers
{
    public class AdminNguoiThueController : BaseController
    {
        private readonly NguoiThueService _service;

        public AdminNguoiThueController(NhaTroDbContext context, NguoiThueService service) : base(context)
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
            var nt = _service.GetById(id);
            if (nt == null) return NotFound();
            return View(nt);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(NguoiThue model)
        {
            if (!ModelState.IsValid) return View(model);

            _service.Create(model);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            return View(_service.GetById(id));
        }

        [HttpPost]
        public IActionResult Edit(NguoiThue model)
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