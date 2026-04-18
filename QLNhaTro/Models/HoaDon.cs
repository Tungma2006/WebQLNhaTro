using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using QLNhaTro.Enums;
namespace QLNhaTro.Models
{
    public class HoaDon
    {
        [Key]
        public int HoaDonId { get; set; } 
        public DateTime Thang {  get; set; }
        public int SoDienTieuThu { get; set; }
        public int SoNuocTieuThu { get;set; }
        public Decimal TienDien { get;set;}
        public Decimal TienNuoc { get; set; }
        public Decimal TienPhong { get; set; }
        public Decimal TongTien { get; set; }
        public TrangThaiThanhToan TrangthaiThanhToan { get; set; } = TrangThaiThanhToan.ChuaThanhToan;
        [ForeignKey("HopDongId")]
        public virtual HopDong? HopDong { get; set; }
        
        public int HopDongId { get; set; }
        public string? AnhThanhToan { get; set; }

    }
}
