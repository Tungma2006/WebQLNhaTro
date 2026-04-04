using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLNhaTro.Models
{
    public class HopDong
    {
        [Key]
        public int HopDongId { get; set; }
        [Required]
        public DateOnly NgayBatdau { get; set; }
        [Required]
        public DateOnly NgayKetThuc { get; set; }
        [Required]
        public Decimal TienDatCoc { get; set; }
        public int PhongId { get; set; }

        [ForeignKey("PhongId")]
        public virtual Phong? Phong { get; set; } 
        public int NguoiThueId { get; set; }
        [ForeignKey("NguoiThueId")]
        public virtual NguoiThue? NguoiThue { get; set; }
        [ForeignKey("HoaDonId")]
        public virtual HoaDon? HoaDon { get; set; } = null;
        public List<HoaDon> HoaDons { get; set; } = new List<HoaDon>();
        

    }
}
