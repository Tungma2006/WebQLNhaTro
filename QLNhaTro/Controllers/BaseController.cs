using Microsoft.AspNetCore.Mvc;
using QLNhaTro.Data;

public class BaseController : Controller
{
    protected readonly NhaTroDbContext _context;

    public BaseController(NhaTroDbContext context)
    {
        _context = context;
    }

    protected void LoadThongBao()
    {
        var list = _context.ThongBaos
            .OrderByDescending(x => x.NgayTao)
            .Take(5)
            .ToList();

        ViewBag.ThongBaos = list;
        ViewBag.SoThongBaoChuaDoc = list.Count(x => !x.DaDoc);
    }
}