
using QLNhaTro.Models;
namespace QLNhaTro.Models;
public class ThongBao
{
    public int Id { get; set; }
    public string NoiDung { get; set; }
    public DateTime NgayTao { get; set; } = DateTime.Now;

    public bool DaDoc { get; set; } = false;

    public int HoaDonId { get; set; }
    public virtual HoaDon HoaDon { get; set; }
}