using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Final.Models;

namespace FinalProject.Migrations
{
    [DbContext(typeof(CoSoDuLieu))]
    [Migration("20170603144459_Change3_Add_HinhBinhLuan")]
    partial class Change3_Add_HinhBinhLuan
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Final.Models.BaiHoc", b =>
                {
                    b.Property<int>("BaiHocId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ChuongId");

                    b.Property<int>("Hide");

                    b.Property<string>("NoiDung");

                    b.Property<int>("Order");

                    b.Property<string>("Ten");

                    b.HasKey("BaiHocId");

                    b.HasIndex("ChuongId");

                    b.ToTable("tbl_BaiHoc");
                });

            modelBuilder.Entity("Final.Models.BaiTap", b =>
                {
                    b.Property<int>("BaiTapId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BaiHocId");

                    b.Property<int>("Hide");

                    b.Property<int>("Order");

                    b.Property<string>("Ten");

                    b.HasKey("BaiTapId");

                    b.HasIndex("BaiHocId");

                    b.ToTable("tbl_BaiTap");
                });

            modelBuilder.Entity("Final.Models.BinhLuan", b =>
                {
                    b.Property<int>("BinhLuanId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BaiHocId");

                    b.Property<int>("Hide");

                    b.Property<string>("Hinh");

                    b.Property<string>("NoiDung");

                    b.Property<int>("Order");

                    b.Property<string>("TacGia");

                    b.HasKey("BinhLuanId");

                    b.HasIndex("BaiHocId");

                    b.ToTable("tbl_BinhLuan");
                });

            modelBuilder.Entity("Final.Models.CauHoi", b =>
                {
                    b.Property<int>("CauHoiId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BaiTapId");

                    b.Property<int>("CauDung");

                    b.Property<string>("CauTraLoi1");

                    b.Property<string>("CauTraLoi2");

                    b.Property<string>("CauTraLoi3");

                    b.Property<string>("CauTraLoi4");

                    b.Property<int>("Hide");

                    b.Property<string>("NoiDung");

                    b.Property<int>("Order");

                    b.HasKey("CauHoiId");

                    b.HasIndex("BaiTapId");

                    b.ToTable("tbl_CauHoi");
                });

            modelBuilder.Entity("Final.Models.Chuong", b =>
                {
                    b.Property<int>("ChuongId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Hide");

                    b.Property<int>("LopId");

                    b.Property<int>("Order");

                    b.Property<string>("Ten");

                    b.HasKey("ChuongId");

                    b.HasIndex("LopId");

                    b.ToTable("tbl_Chuong");
                });

            modelBuilder.Entity("Final.Models.HocPhi", b =>
                {
                    b.Property<int>("HocPhiId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Hide");

                    b.Property<int>("HocSinhId");

                    b.Property<DateTime>("NgayHetHan");

                    b.Property<DateTime>("NgayThanhToan");

                    b.Property<int>("Order");

                    b.Property<int>("SoTien");

                    b.HasKey("HocPhiId");

                    b.HasIndex("HocSinhId");

                    b.ToTable("tbl_HocPhi");
                });

            modelBuilder.Entity("Final.Models.HocSinh", b =>
                {
                    b.Property<int>("HocSinhId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<int>("Hide");

                    b.Property<string>("Hinh");

                    b.Property<string>("HoTen")
                        .IsRequired();

                    b.Property<DateTime>("NgaySinh");

                    b.Property<int>("Order");

                    b.Property<string>("Password")
                        .IsRequired();

                    b.HasKey("HocSinhId");

                    b.ToTable("tbl_HocSinh");
                });

            modelBuilder.Entity("Final.Models.HocSinhBaiHoc", b =>
                {
                    b.Property<int>("HocSinhId");

                    b.Property<int>("BaiHocId");

                    b.Property<int>("HoanThanh");

                    b.HasKey("HocSinhId", "BaiHocId");

                    b.HasIndex("BaiHocId");

                    b.ToTable("tbl_HocSinhBaiHoc");
                });

            modelBuilder.Entity("Final.Models.HocSinhBaiTap", b =>
                {
                    b.Property<int>("HocSinhId");

                    b.Property<int>("BaiTapId");

                    b.Property<int>("HoanThanh");

                    b.HasKey("HocSinhId", "BaiTapId");

                    b.HasIndex("BaiTapId");

                    b.ToTable("tbl_HocSinhBaiTap");
                });

            modelBuilder.Entity("Final.Models.Lop", b =>
                {
                    b.Property<int>("LopId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Hide");

                    b.Property<int>("Order");

                    b.Property<string>("Ten");

                    b.HasKey("LopId");

                    b.ToTable("tbl_Lop");
                });

            modelBuilder.Entity("Final.Models.ToanVuiMoiNgay", b =>
                {
                    b.Property<int>("ToanVuiMoiNgayId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Hide");

                    b.Property<string>("NoiDung");

                    b.Property<int>("Order");

                    b.Property<string>("Ten");

                    b.HasKey("ToanVuiMoiNgayId");

                    b.ToTable("tbl_ToanVuiMoiNgay");
                });

            modelBuilder.Entity("Final.Models.BaiHoc", b =>
                {
                    b.HasOne("Final.Models.Chuong", "Chuong")
                        .WithMany("BaiHocs")
                        .HasForeignKey("ChuongId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Final.Models.BaiTap", b =>
                {
                    b.HasOne("Final.Models.BaiHoc", "BaiHoc")
                        .WithMany("BaiTaps")
                        .HasForeignKey("BaiHocId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Final.Models.BinhLuan", b =>
                {
                    b.HasOne("Final.Models.BaiHoc", "BaiHoc")
                        .WithMany("BinhLuans")
                        .HasForeignKey("BaiHocId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Final.Models.CauHoi", b =>
                {
                    b.HasOne("Final.Models.BaiTap", "BaiTap")
                        .WithMany("CauHois")
                        .HasForeignKey("BaiTapId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Final.Models.Chuong", b =>
                {
                    b.HasOne("Final.Models.Lop", "Lop")
                        .WithMany("Chuongs")
                        .HasForeignKey("LopId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Final.Models.HocPhi", b =>
                {
                    b.HasOne("Final.Models.HocSinh", "HocSinh")
                        .WithMany("HocPhis")
                        .HasForeignKey("HocSinhId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Final.Models.HocSinhBaiHoc", b =>
                {
                    b.HasOne("Final.Models.BaiHoc", "BaiHoc")
                        .WithMany("HocSinhBaiHocs")
                        .HasForeignKey("BaiHocId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Final.Models.HocSinh", "HocSinh")
                        .WithMany("HocSinhBaiHocs")
                        .HasForeignKey("HocSinhId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Final.Models.HocSinhBaiTap", b =>
                {
                    b.HasOne("Final.Models.BaiTap", "BaiTap")
                        .WithMany("HocSinhBaiTaps")
                        .HasForeignKey("BaiTapId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Final.Models.HocSinh", "HocSinh")
                        .WithMany("HocSinhBaiTaps")
                        .HasForeignKey("HocSinhId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
