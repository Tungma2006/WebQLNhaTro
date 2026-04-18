using System.ComponentModel.DataAnnotations;
namespace QLNhaTro.Enums;
public enum TrangThaiThanhToan
{
    [Display(Name = "Chưa thanh toán")]
    ChuaThanhToan,

    [Display(Name = "Đã thanh toán")]
    DaThanhToan
}