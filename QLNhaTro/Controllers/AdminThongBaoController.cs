using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLNhaTro.Data;

namespace QLNhaTro.Controllers
{
    public class AdminThongBaoController : BaseController
    {
        private readonly NhaTroDbContext _context;

        public AdminThongBaoController(NhaTroDbContext context) : base(context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var data = _context.ThongBaos
                .OrderByDescending(x => x.Id)
                .ToList();

            return View(data);
        }

        public IActionResult Details(int id)
        {
            var tb = _context.ThongBaos
                .Include(x => x.HoaDon)
                .FirstOrDefault(x => x.Id == id);

            return View(tb);
        }
    }
}
