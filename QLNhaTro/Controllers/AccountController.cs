using Microsoft.AspNetCore.Mvc;
using QLNhaTro.Data;
using System.Linq;

namespace QLNhaTro.Controllers
{
    public class AccountController : Controller
    {
        private readonly NhaTroDbContext _context;

        public AccountController(NhaTroDbContext context)
        {
            _context = context;
        }

        // GET
        public IActionResult Login()
        {
            return View();
        }

        // POST
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // ✅ check admin trước
            if (username == "admin" && password == "123")
            {
                HttpContext.Session.SetString("User", username);
                HttpContext.Session.SetString("Role", "Admin");
                return RedirectToAction("Index", "Admin");
            }

            // 🔍 tìm tenant
            var user = _context.NguoiThues
                .FirstOrDefault(x => x.TenDangNhap == username
                                  && x.MatKhau == password);

            if (user == null)
            {
                ViewBag.Error = "Sai tài khoản hoặc mật khẩu";
                return View();
            }

            HttpContext.Session.SetString("User", username);

            // kiểm tra CCCD
            if (_context.NguoiThues.Any(x => x.CCCD == username))
            {
                HttpContext.Session.SetString("Role", "Tenant");
                return RedirectToAction("Index", "TenantPhong");
            }

            return RedirectToAction("Login");
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}