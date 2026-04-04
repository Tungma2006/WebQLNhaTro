using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLNhaTro.Models
{
    public class ThanhToan
    {
        [Key]
        public int ThanhToanId { get; set; }
        public decimal SoTien {  get; set; }
        public DateOnly NgayThanhToan { get; set; }
        [ForeignKey("HoaDonId")]
        public HoaDon HoaDon { get; set; }
        public int HoaDonId { get; set; }
    }
}
