using System.ComponentModel.DataAnnotations;

namespace QLNhaTro.Models
{
    public class NguoiThue
    {
        [Key]
        public int NguoiThueId { get; set; }
        [Required]
        public string NguoiThueName { get; set; }
        [Required]
        public string CCCD { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public virtual ICollection<HopDong> HopDongs { get; set; } = new List<HopDong>();
        public string TenDangNhap { get; set; } = string.Empty;
        public string MatKhau { get; set; } = "123"; 
    }
}
