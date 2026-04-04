using QLNhaTro.Models;
using QLNhaTro.Repositories;
using QLNhaTro.Data;
using Microsoft.EntityFrameworkCore;

namespace QLNhaTro.Services
{
    public class HopDongService
    {
        private readonly IRepository<HopDong> _repo;

        private readonly NhaTroDbContext _context;

        public HopDongService(IRepository<HopDong> repo, NhaTroDbContext context)
        {
            _repo = repo;
            _context = context;
        }
        public List<HopDong> GetAll()
        {
            return _repo.GetAll();
        }
        public HopDong GetById(int id)
        {
            return _context.HopDongs
                   .Include(h => h.NguoiThue)
                   .Include(h => h.Phong)
                   .FirstOrDefault(h => h.HopDongId == id);
        }
        
        public void Update(HopDong hopDong)
        {
            _repo.Update(hopDong);
        }
        public void Delete(int id)
        {
            _repo.Delete(id);
        }
        public void Create(HopDong hopDong)
        {
            try
            {
                _repo.Add(hopDong);
            }
            catch (Exception ex)
            {
                // Ghi log lỗi nếu cần và ném lỗi ra ngoài để Controller bắt được
                throw new Exception("Lỗi khi lưu hợp đồng: " + ex.Message);
            }
        }
        public List<HopDong> Search(string keyword)
        {
            // 1. CHỈ KHỞI TẠO QUERY (Không dùng .ToList() ở đây)
            var query = _context.HopDongs
                                .Include(h => h.NguoiThue)
                                .Include(h => h.Phong)
                                .AsQueryable(); // Dùng AsQueryable để giữ kết nối Database

            // 2. LỌC DỮ LIỆU
            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = keyword.Trim().ToLower();

                query = query.Where(h =>
                    (h.NguoiThue.NguoiThueName != null && h.NguoiThue.NguoiThueName.ToLower().Contains(keyword)) ||
                    (h.Phong.TenPhong != null && h.Phong.TenPhong.ToLower().Contains(keyword)) ||
                    (h.NguoiThue.CCCD != null && h.NguoiThue.CCCD.Contains(keyword))
                );
            }

            // 3. CUỐI CÙNG MỚI ĐƯA VỀ LIST
            return query.ToList();
        }


        public List<HopDong> GetByNguoiThue(string cccd)
        {
            return _context.HopDongs
                .Include(h => h.NguoiThue)
                .Include(h => h.Phong)
                .Where(h => h.NguoiThue.CCCD == cccd) // So sánh trực tiếp CCCD của người thuê
                .ToList();
        }
    }
}
