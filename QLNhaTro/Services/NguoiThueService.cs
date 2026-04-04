using QLNhaTro.Models;
using QLNhaTro.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QLNhaTro.Services
{
    public class NguoiThueService
    {
        private readonly IRepository<NguoiThue> _repo;

        public NguoiThueService(IRepository<NguoiThue> repo)
        {
            _repo = repo;
        }

        public List<NguoiThue> GetAll()
        {
            return _repo.GetAll();
        }

        // 🔍 Search
        public List<NguoiThue> Search(string keyword)
        {
            var query = _repo.GetAll();

            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = keyword.Trim();

                query = query.Where(x =>
                    x.NguoiThueName.Contains(keyword, StringComparison.OrdinalIgnoreCase)
                    || x.CCCD.Contains(keyword)
                    || x.PhoneNumber.Contains(keyword)
                ).ToList();
            }

            return query;
        }

        public NguoiThue GetById(int id)
        {
            return _repo.GetById(id);
        }

        public void Create(NguoiThue model)
        {
            if (string.IsNullOrEmpty(model.CCCD))
                throw new Exception("CCCD không được để trống");

            if (_repo.GetAll().Any(x => x.CCCD == model.CCCD))
                throw new Exception("CCCD đã tồn tại");

            model.TenDangNhap = model.CCCD;

            // Đảm bảo mật khẩu không bị null nếu DB yêu cầu
            if (string.IsNullOrEmpty(model.MatKhau))
                model.MatKhau = "123";

            _repo.Add(model);
        }

        public void Update(NguoiThue model)
        {
            _repo.Update(model);
        }

        public void Delete(int id)
        {
            _repo.Delete(id);
        }
    }
}