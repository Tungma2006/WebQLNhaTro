using Microsoft.EntityFrameworkCore;
using QLNhaTro.Data;
using QLNhaTro.Models;
using QLNhaTro.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QLNhaTro.Services
{
    public class HoaDonService
    {
        private readonly IRepository<HoaDon> _repo;
        private readonly NhaTroDbContext _context;
        public HoaDonService(IRepository<HoaDon> repo)
        {
            _repo = repo;
        }

        public List<HoaDon> GetAll()
        {
            return _repo.GetAll();
        }

        public HoaDon GetById(int id)
        {
            return _repo.GetById(id);
        }

        public void Create(HoaDon hoaDon)
        {
            if (hoaDon == null)
                throw new Exception("Dữ liệu không hợp lệ");

            var today = DateOnly.FromDateTime(DateTime.Now);

            // ❗ Không cho tạo hóa đơn trong quá khứ
            if (hoaDon.Thang < today)
                throw new Exception("Tháng không hợp lệ");

            // ❗ Không cho tạo trùng hóa đơn theo hợp đồng + tháng
            if (_repo.GetAll().Any(x => x.HopDongId == hoaDon.HopDongId
                                    && x.Thang == hoaDon.Thang))
            {
                throw new Exception("Hóa đơn tháng này đã tồn tại");
            }
            var currentMonth = new DateOnly(today.Year, today.Month, 1);

            if (hoaDon.Thang < currentMonth)
            {
                throw new Exception("Không được tạo hóa đơn cho tháng trước");
            }

            // 👉 Tính tiền
            decimal giaDien = 3500;   // 3,5k / số
            decimal giaNuoc = 10000;  // 10k / m3

            hoaDon.TienDien = hoaDon.SoDienTieuThu * giaDien;
            hoaDon.TienNuoc = hoaDon.SoNuocTieuThu * giaNuoc;

            // 👉 Tổng tiền
            hoaDon.TongTien = hoaDon.TienPhong
                            + hoaDon.TienDien
                            + hoaDon.TienNuoc;

            // trạng thái mặc định
            hoaDon.TrangthaiThanhToan = "Chưa thanh toán";

            _repo.Add(hoaDon);
        }

        public void Update(HoaDon hoaDon)
        {
            if (hoaDon == null)
                throw new Exception("Dữ liệu không hợp lệ");

            decimal giaDien = 3500;
            decimal giaNuoc = 10000;

            hoaDon.TienDien = hoaDon.SoDienTieuThu * giaDien;
            hoaDon.TienNuoc = hoaDon.SoNuocTieuThu * giaNuoc;

            hoaDon.TongTien = hoaDon.TienPhong
                            + hoaDon.TienDien
                            + hoaDon.TienNuoc;

            _repo.Update(hoaDon);
        }

        public void Delete(int id)
        {
            _repo.Delete(id);
        }
        public List<HoaDon> SearchByHopDong(string keyword)
        {
            var query = _repo.GetAll();

            if (!string.IsNullOrEmpty(keyword))
            {
                if (int.TryParse(keyword, out int id))
                {
                    query = query.Where(h => h.HopDongId == id).ToList();
                }
                else
                {
                    return new List<HoaDon>(); // nhập sai → trả rỗng
                }
            }

            return query;
        }
        public List<HoaDon> GetHoaDonThangHienTai(string cccd)
        {
            var now = DateTime.Now;

            var data = _context.HoaDons
                .Include(h => h.HopDong)
                    .ThenInclude(hd => hd.NguoiThue) // Kết nối trực tiếp từ HopDong sang NguoiThue
                .Where(h =>
                    h.Thang.Month == now.Month &&
                    h.Thang.Year == now.Year &&
                    h.HopDong.NguoiThue.CCCD == cccd // So khớp CCCD trực tiếp
                )
                .ToList();

            return data;
        }
    }
}