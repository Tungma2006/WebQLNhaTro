using QLNhaTro.Enums;

namespace QLNhaTro.Models
{
    public class HoaDonViewModel
    {
        public int HoaDonId { get; set; }
        public DateTime Thang { get; set; }

        public int SoDienTieuThu { get; set; }
        public int SoNuocTieuThu { get; set; }

        public decimal TienDien { get; set; }
        public decimal TienNuoc { get; set; }
        public decimal TienPhong { get; set; }
        public decimal TongTien { get; set; }

        public TrangThaiThanhToan TrangthaiThanhToan { get; set; }
        public int HopDongId { get; set; }

        // 👇 chỉ để hiển thị
        public string TenPhong { get; set; }
    }
}
