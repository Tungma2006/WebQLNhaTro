using Microsoft.AspNetCore.Mvc;
using QLNhaTro.Services;
using QLNhaTro.Models;

namespace QLNhaTro.Controllers
{
    public class AdminHoaDonController : Controller
    {
        private readonly HoaDonService _service;

        public AdminHoaDonController(HoaDonService service)
        {
            _service = service;
        }

        public IActionResult Index(string keyword)
        {
            var data = _service.SearchByHopDong(keyword);
            ViewBag.Keyword = keyword;
            return View(data);
        }

        public IActionResult Details(int id)
        {
            var hd = _service.GetById(id);
            if (hd == null) return NotFound();
            return View(hd);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(HoaDon model)
        {
            _service.Create(model);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            return View(_service.GetById(id));
        }

        [HttpPost]
        public IActionResult Edit(HoaDon model)
        {
            _service.Update(model);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            return View(_service.GetById(id));
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}