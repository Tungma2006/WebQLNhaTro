using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLNhaTro.Migrations
{
    /// <inheritdoc />
    public partial class lan1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NguoiThues",
                columns: table => new
                {
                    NguoiThueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NguoiThueName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CCCD = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenDangNhap = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiThues", x => x.NguoiThueId);
                });

            migrationBuilder.CreateTable(
                name: "Phongs",
                columns: table => new
                {
                    PhongId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenPhong = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DienTich = table.Column<float>(type: "real", nullable: false),
                    SoNguoiToiDa = table.Column<int>(type: "int", nullable: false),
                    GiaPhong = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phongs", x => x.PhongId);
                });

            migrationBuilder.CreateTable(
                name: "HoaDons",
                columns: table => new
                {
                    HoaDonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Thang = table.Column<DateOnly>(type: "date", nullable: false),
                    SoDienTieuThu = table.Column<int>(type: "int", nullable: false),
                    SoNuocTieuThu = table.Column<int>(type: "int", nullable: false),
                    TienDien = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    TienNuoc = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    TienPhong = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    TongTien = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    TrangthaiThanhToan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HopDongId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDons", x => x.HoaDonId);
                });

            migrationBuilder.CreateTable(
                name: "HopDongs",
                columns: table => new
                {
                    HopDongId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NgayBatdau = table.Column<DateOnly>(type: "date", nullable: false),
                    NgayKetThuc = table.Column<DateOnly>(type: "date", nullable: false),
                    TienDatCoc = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    PhongId = table.Column<int>(type: "int", nullable: false),
                    NguoiThueId = table.Column<int>(type: "int", nullable: false),
                    HoaDonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HopDongs", x => x.HopDongId);
                    table.ForeignKey(
                        name: "FK_HopDongs_HoaDons_HoaDonId",
                        column: x => x.HoaDonId,
                        principalTable: "HoaDons",
                        principalColumn: "HoaDonId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HopDongs_NguoiThues_NguoiThueId",
                        column: x => x.NguoiThueId,
                        principalTable: "NguoiThues",
                        principalColumn: "NguoiThueId");
                    table.ForeignKey(
                        name: "FK_HopDongs_Phongs_PhongId",
                        column: x => x.PhongId,
                        principalTable: "Phongs",
                        principalColumn: "PhongId");
                });

            migrationBuilder.CreateTable(
                name: "ThanhToans",
                columns: table => new
                {
                    ThanhToanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SoTien = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    NgayThanhToan = table.Column<DateOnly>(type: "date", nullable: false),
                    HoaDonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThanhToans", x => x.ThanhToanId);
                    table.ForeignKey(
                        name: "FK_ThanhToans_HoaDons_HoaDonId",
                        column: x => x.HoaDonId,
                        principalTable: "HoaDons",
                        principalColumn: "HoaDonId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HoaDons_HopDongId",
                table: "HoaDons",
                column: "HopDongId");

            migrationBuilder.CreateIndex(
                name: "IX_HopDongs_HoaDonId",
                table: "HopDongs",
                column: "HoaDonId");

            migrationBuilder.CreateIndex(
                name: "IX_HopDongs_NguoiThueId",
                table: "HopDongs",
                column: "NguoiThueId");

            migrationBuilder.CreateIndex(
                name: "IX_HopDongs_PhongId",
                table: "HopDongs",
                column: "PhongId");

            migrationBuilder.CreateIndex(
                name: "IX_ThanhToans_HoaDonId",
                table: "ThanhToans",
                column: "HoaDonId");

            migrationBuilder.AddForeignKey(
                name: "FK_HoaDons_HopDongs_HopDongId",
                table: "HoaDons",
                column: "HopDongId",
                principalTable: "HopDongs",
                principalColumn: "HopDongId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HoaDons_HopDongs_HopDongId",
                table: "HoaDons");

            migrationBuilder.DropTable(
                name: "ThanhToans");

            migrationBuilder.DropTable(
                name: "HopDongs");

            migrationBuilder.DropTable(
                name: "HoaDons");

            migrationBuilder.DropTable(
                name: "NguoiThues");

            migrationBuilder.DropTable(
                name: "Phongs");
        }
    }
}
