using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProject.Migrations
{
    public partial class Change2_HocPhi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DaThanhToan",
                table: "tbl_HocPhi");

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayHetHan",
                table: "tbl_HocPhi",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayThanhToan",
                table: "tbl_HocPhi",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NgayHetHan",
                table: "tbl_HocPhi");

            migrationBuilder.DropColumn(
                name: "NgayThanhToan",
                table: "tbl_HocPhi");

            migrationBuilder.AddColumn<int>(
                name: "DaThanhToan",
                table: "tbl_HocPhi",
                nullable: false,
                defaultValue: 0);
        }
    }
}
