using Microsoft.EntityFrameworkCore;
using QLNhaTro.Data;
using QLNhaTro.Models;
using QLNhaTro.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using QLNhaTro.Enums;

namespace QLNhaTro.Services
{
    public class HoaDonService
    {
        private readonly IRepository<HoaDon> _repo;
        private readonly NhaTroDbContext _context;
        public HoaDonService(IRepository<HoaDon> repo, NhaTroDbContext context)
        {
            _repo = repo;
            _context = context;
        }
        public List<HoaDon> GetAll()
        {
            return _repo.GetAll();
        }

        public HoaDon GetById(int id)
        {
            return _context.HoaDons
                .Include(h => h.HopDong)
                .ThenInclude(hd => hd.Phong)
                .FirstOrDefault(h => h.HoaDonId == id);
        }

        public void Create(HoaDon hoaDon)
        {
            if (hoaDon == null)
                throw new Exception("Dữ liệu không hợp lệ");

            var today = DateTime.Now.Date;
            var currentMonth = new DateTime(today.Year, today.Month, 1);

            // ❗ Không cho tạo hóa đơn tháng trước
            if (hoaDon.Thang < currentMonth)
                throw new Exception("Không được tạo hóa đơn cho tháng trước");

            // ❗ Không trùng tháng
            if (_repo.GetAll().Any(x =>
                x.HopDongId == hoaDon.HopDongId &&
                x.Thang.Month == hoaDon.Thang.Month &&
                x.Thang.Year == hoaDon.Thang.Year))
            {
                throw new Exception("Hóa đơn tháng này đã tồn tại");
            }

            // 👉 set về ngày đầu tháng (rất quan trọng)
            hoaDon.Thang = new DateTime(hoaDon.Thang.Year, hoaDon.Thang.Month, 1);

            // 👉 giá
            decimal giaDien = 3500;
            decimal giaNuoc = 10000;

            hoaDon.TienDien = hoaDon.SoDienTieuThu * giaDien;
            hoaDon.TienNuoc = hoaDon.SoNuocTieuThu * giaNuoc;

            // 👉 tiền phòng fix cứng (hoặc lấy DB)
            hoaDon.TienPhong = 1000000;

            hoaDon.TongTien = hoaDon.TienPhong
                            + hoaDon.TienDien
                            + hoaDon.TienNuoc;

            hoaDon.TrangthaiThanhToan = TrangThaiThanhToan.ChuaThanhToan;

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
        //public List<HoaDon> SearchByHopDong(string keyword)
        //{
        //    var query = _repo.GetAll();

        //    if (!string.IsNullOrEmpty(keyword))
        //    {
        //        if (int.TryParse(keyword, out int id))
        //        {
        //            query = query.Where(h => h.HopDongId == id).ToList();
        //        }
        //        else
        //        {
        //            return new List<HoaDon>(); // nhập sai → trả rỗng
        //        }
        //    }

        //    return query;
        //}
        public List<HoaDon> Search(string tenPhong, bool? chuaThanhToan)
        {
            var query = _context.HoaDons
                .Include(h => h.HopDong)
                .ThenInclude(hd => hd.Phong)
                .AsQueryable();

            // 🔍 Tìm theo tên phòng
            if (!string.IsNullOrEmpty(tenPhong))
            {
                query = query.Where(h => h.HopDong.Phong.TenPhong.Contains(tenPhong));
            }

            // ☑ Chưa thanh toán
            if (chuaThanhToan == true)
            {
                query = query.Where(h =>
                    h.TrangthaiThanhToan == TrangThaiThanhToan.ChuaThanhToan);
            }

            return query.ToList();
        }
        public List<HoaDon> GetHoaDonThangHienTai(string cccd)
        {
            var now = DateTime.Now;

            var data = _context.HoaDons
                .Include(h => h.HopDong)
                    .ThenInclude(hd => hd.Phong)   // 🔥 THÊM DÒNG NÀY
                .Include(h => h.HopDong)
                    .ThenInclude(hd => hd.NguoiThue)
                .Where(h =>
                    h.Thang.Month == now.Month &&
                    h.Thang.Year == now.Year &&
                    h.HopDong.NguoiThue.CCCD == cccd
                )
                .ToList();

            return data;
        }
    }
}