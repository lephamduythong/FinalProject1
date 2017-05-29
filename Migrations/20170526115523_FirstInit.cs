using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FinalProject.Migrations
{
    public partial class FirstInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_HocMaVui",
                columns: table => new
                {
                    HocMaVuiId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Hide = table.Column<int>(nullable: false),
                    NoiDung = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false),
                    Ten = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_HocMaVui", x => x.HocMaVuiId);
                });

            migrationBuilder.CreateTable(
                name: "tbl_HocSinh",
                columns: table => new
                {
                    HocSinhId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: false),
                    Hide = table.Column<int>(nullable: false),
                    Hinh = table.Column<string>(nullable: true),
                    HoTen = table.Column<string>(nullable: false),
                    NgaySinh = table.Column<DateTime>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    Password = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_HocSinh", x => x.HocSinhId);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Lop",
                columns: table => new
                {
                    LopId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Hide = table.Column<int>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    Ten = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Lop", x => x.LopId);
                });

            migrationBuilder.CreateTable(
                name: "tbl_ToanVuiMoiNgay",
                columns: table => new
                {
                    ToanVuiMoiNgayId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Hide = table.Column<int>(nullable: false),
                    NoiDung = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false),
                    Ten = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ToanVuiMoiNgay", x => x.ToanVuiMoiNgayId);
                });

            migrationBuilder.CreateTable(
                name: "tbl_HocPhi",
                columns: table => new
                {
                    HocPhiId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DaThanhToan = table.Column<int>(nullable: false),
                    Hide = table.Column<int>(nullable: false),
                    HocSinhId = table.Column<int>(nullable: false),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_HocPhi", x => x.HocPhiId);
                    table.ForeignKey(
                        name: "FK_tbl_HocPhi_tbl_HocSinh_HocSinhId",
                        column: x => x.HocSinhId,
                        principalTable: "tbl_HocSinh",
                        principalColumn: "HocSinhId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Chuong",
                columns: table => new
                {
                    ChuongId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Hide = table.Column<int>(nullable: false),
                    LopId = table.Column<int>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    Ten = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Chuong", x => x.ChuongId);
                    table.ForeignKey(
                        name: "FK_tbl_Chuong_tbl_Lop_LopId",
                        column: x => x.LopId,
                        principalTable: "tbl_Lop",
                        principalColumn: "LopId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_BaiHoc",
                columns: table => new
                {
                    BaiHocId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ChuongId = table.Column<int>(nullable: false),
                    Hide = table.Column<int>(nullable: false),
                    NoiDung = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false),
                    Ten = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_BaiHoc", x => x.BaiHocId);
                    table.ForeignKey(
                        name: "FK_tbl_BaiHoc_tbl_Chuong_ChuongId",
                        column: x => x.ChuongId,
                        principalTable: "tbl_Chuong",
                        principalColumn: "ChuongId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_BaiTap",
                columns: table => new
                {
                    BaiTapId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BaiHocId = table.Column<int>(nullable: false),
                    Hide = table.Column<int>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    Ten = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_BaiTap", x => x.BaiTapId);
                    table.ForeignKey(
                        name: "FK_tbl_BaiTap_tbl_BaiHoc_BaiHocId",
                        column: x => x.BaiHocId,
                        principalTable: "tbl_BaiHoc",
                        principalColumn: "BaiHocId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_BinhLuan",
                columns: table => new
                {
                    BinhLuanId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BaiHocId = table.Column<int>(nullable: false),
                    Hide = table.Column<int>(nullable: false),
                    NoiDung = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false),
                    TacGia = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_BinhLuan", x => x.BinhLuanId);
                    table.ForeignKey(
                        name: "FK_tbl_BinhLuan_tbl_BaiHoc_BaiHocId",
                        column: x => x.BaiHocId,
                        principalTable: "tbl_BaiHoc",
                        principalColumn: "BaiHocId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_HocSinhBaiHoc",
                columns: table => new
                {
                    HocSinhId = table.Column<int>(nullable: false),
                    BaiHocId = table.Column<int>(nullable: false),
                    HoanThanh = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_HocSinhBaiHoc", x => new { x.HocSinhId, x.BaiHocId });
                    table.ForeignKey(
                        name: "FK_tbl_HocSinhBaiHoc_tbl_BaiHoc_BaiHocId",
                        column: x => x.BaiHocId,
                        principalTable: "tbl_BaiHoc",
                        principalColumn: "BaiHocId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_HocSinhBaiHoc_tbl_HocSinh_HocSinhId",
                        column: x => x.HocSinhId,
                        principalTable: "tbl_HocSinh",
                        principalColumn: "HocSinhId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_CauHoi",
                columns: table => new
                {
                    CauHoiId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BaiTapId = table.Column<int>(nullable: false),
                    CauDung = table.Column<int>(nullable: false),
                    CauTraLoi1 = table.Column<string>(nullable: true),
                    CauTraLoi2 = table.Column<string>(nullable: true),
                    CauTraLoi3 = table.Column<string>(nullable: true),
                    CauTraLoi4 = table.Column<string>(nullable: true),
                    Hide = table.Column<int>(nullable: false),
                    NoiDung = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_CauHoi", x => x.CauHoiId);
                    table.ForeignKey(
                        name: "FK_tbl_CauHoi_tbl_BaiTap_BaiTapId",
                        column: x => x.BaiTapId,
                        principalTable: "tbl_BaiTap",
                        principalColumn: "BaiTapId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_HocSinhBaiTap",
                columns: table => new
                {
                    HocSinhId = table.Column<int>(nullable: false),
                    BaiTapId = table.Column<int>(nullable: false),
                    HoanThanh = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_HocSinhBaiTap", x => new { x.HocSinhId, x.BaiTapId });
                    table.ForeignKey(
                        name: "FK_tbl_HocSinhBaiTap_tbl_BaiTap_BaiTapId",
                        column: x => x.BaiTapId,
                        principalTable: "tbl_BaiTap",
                        principalColumn: "BaiTapId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_HocSinhBaiTap_tbl_HocSinh_HocSinhId",
                        column: x => x.HocSinhId,
                        principalTable: "tbl_HocSinh",
                        principalColumn: "HocSinhId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BaiHoc_ChuongId",
                table: "tbl_BaiHoc",
                column: "ChuongId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BaiTap_BaiHocId",
                table: "tbl_BaiTap",
                column: "BaiHocId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BinhLuan_BaiHocId",
                table: "tbl_BinhLuan",
                column: "BaiHocId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_CauHoi_BaiTapId",
                table: "tbl_CauHoi",
                column: "BaiTapId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Chuong_LopId",
                table: "tbl_Chuong",
                column: "LopId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_HocPhi_HocSinhId",
                table: "tbl_HocPhi",
                column: "HocSinhId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_HocSinhBaiHoc_BaiHocId",
                table: "tbl_HocSinhBaiHoc",
                column: "BaiHocId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_HocSinhBaiTap_BaiTapId",
                table: "tbl_HocSinhBaiTap",
                column: "BaiTapId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_BinhLuan");

            migrationBuilder.DropTable(
                name: "tbl_CauHoi");

            migrationBuilder.DropTable(
                name: "tbl_HocMaVui");

            migrationBuilder.DropTable(
                name: "tbl_HocPhi");

            migrationBuilder.DropTable(
                name: "tbl_HocSinhBaiHoc");

            migrationBuilder.DropTable(
                name: "tbl_HocSinhBaiTap");

            migrationBuilder.DropTable(
                name: "tbl_ToanVuiMoiNgay");

            migrationBuilder.DropTable(
                name: "tbl_BaiTap");

            migrationBuilder.DropTable(
                name: "tbl_HocSinh");

            migrationBuilder.DropTable(
                name: "tbl_BaiHoc");

            migrationBuilder.DropTable(
                name: "tbl_Chuong");

            migrationBuilder.DropTable(
                name: "tbl_Lop");
        }
    }
}
