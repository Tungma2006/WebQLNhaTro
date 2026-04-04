using System.ComponentModel.DataAnnotations;

namespace QLNhaTro.Models
{
    public class Phong
    {
        [Key]
        public int PhongId { get; set; }
        [Required]
        public string TenPhong { get; set; }
        [Required]
        public float DienTich { get; set; }
        [Required]
        public int SoNguoiToiDa { get; set; }
        [Required]
        public decimal GiaPhong { get; set; }
        [Required]
        public string Trangthai { get; set; }
        public virtual ICollection<HopDong> HopDongs { get; set; } = new List<HopDong>();
    }
}
