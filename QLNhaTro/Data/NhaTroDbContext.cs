using Microsoft.EntityFrameworkCore;
using QLNhaTro.Models;
using System;
using System.Collections.Generic;
namespace QLNhaTro.Data
{
    public class NhaTroDbContext : DbContext
    {
        public NhaTroDbContext(DbContextOptions<NhaTroDbContext> options) : base(options) { }
        public DbSet<NguoiThue> NguoiThues { get; set; }
        public DbSet<Phong> Phongs { get; set; }
        public DbSet<HopDong> HopDongs { get; set; }
        public DbSet<HoaDon> HoaDons { get; set; }
        public DbSet<ThongBao> ThongBaos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<HopDong>()
            .HasOne(h => h.Phong)
            .WithMany(p => p.HopDongs)
            .HasForeignKey(h => h.PhongId)
            .OnDelete(DeleteBehavior.NoAction);

            // 2. Cấu hình quan hệ MỚI: NguoiThue (1) - HopDong (n)
            // Thay thế hoàn toàn cho ChiTietHopDong trước đây
            modelBuilder.Entity<HopDong>()
                .HasOne(h => h.NguoiThue)
                .WithMany(n => n.HopDongs)
                .HasForeignKey(h => h.NguoiThueId)
                .OnDelete(DeleteBehavior.NoAction);

            // 3. Cấu hình quan hệ: HopDong (1) - HoaDon (n)
            modelBuilder.Entity<HoaDon>()
                .HasOne(hd => hd.HopDong)
                .WithMany(h => h.HoaDons)
                .HasForeignKey(hd => hd.HopDongId)
                .OnDelete(DeleteBehavior.NoAction);

            // 4. Cấu hình quan hệ: HoaDon (1) - ThanhToan (n)
            //modelBuilder.Entity<ThanhToan>()
            //    .HasOne(tt => tt.HoaDon)
            //    .WithMany(hd => hd.ThanhToans)
            //    .HasForeignKey(tt => tt.HoaDonId)
            //    .OnDelete(DeleteBehavior.NoAction);


            // Cấu hình cho tất cả các cột decimal trong toàn bộ Database là (18)
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18)");
            }
        }
    }
}
