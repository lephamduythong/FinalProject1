using Microsoft.EntityFrameworkCore;
using Final.Models;

namespace Final.Models
{
    public class CoSoDuLieu : DbContext
    {
        public DbSet<Lop> Lops { get; set; }
        public DbSet<Chuong> Chuongs { get; set; }
        public DbSet<BaiHoc> BaiHocs { get; set; }
        public DbSet<HocSinh> HocSinhs { get; set; }
        public DbSet<BinhLuan> BinhLuans { get; set; }
        public DbSet<HocSinhBaiHoc> HocSinhBaiHocs { get; set; }
        public DbSet<HocSinhBaiTap> HocSinhBaiTaps { get; set; }
        public DbSet<BaiTap> BaiTaps { get; set; }
        public DbSet<HocPhi> HocPhis { get; set; }
        public DbSet<CauHoi> CauHois { get; set; }
        public DbSet<ToanVuiMoiNgay> ToanVuiMoiNgays { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=DoAn1_Ver2;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HocSinhBaiHoc>()
                .HasKey(c => new { c.HocSinhId, c.BaiHocId });
            modelBuilder.Entity<HocSinhBaiTap>()
                .HasKey(c => new { c.HocSinhId, c.BaiTapId });
        }
    }
}