using QLNhaTro.Models;
using QLNhaTro.Repositories;

namespace QLNhaTro.Services
{
    public class PhongService
    {
        private readonly IRepository<Phong> _repo;

        public PhongService(IRepository<Phong> repo)
        {
            _repo = repo;
        }

        public List<Phong> GetAll()
        {
            return _repo.GetAll();
        }

        public Phong GetById(int id)
        {
            return _repo.GetById(id);
        }

        public string Create(Phong phong)
        {
            if (phong == null)
                return "Dữ liệu không hợp lệ";

            if (phong.GiaPhong <= 0)
                return "Giá phòng phải > 0";

            if (_repo.GetAll().Any(x => x.TenPhong == phong.TenPhong))
                return "Phòng đã tồn tại";

            _repo.Add(phong);
            return null; // không lỗi
        }

        public void Update(Phong phong)
        {
            _repo.Update(phong);
        }

        public void Delete(int id)
        {
            _repo.Delete(id);
        }
        public List<Phong> Search(string keyword)
        {
            var query = _repo.GetAll();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(p => p.TenPhong
                    .Contains(keyword, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            return query;
        }
    }
}
