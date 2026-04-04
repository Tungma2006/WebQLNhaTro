using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLNhaTro.Migrations
{
    /// <inheritdoc />
    public partial class lan2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HopDongs_HoaDons_HoaDonId",
                table: "HopDongs");

            migrationBuilder.AlterColumn<int>(
                name: "HoaDonId",
                table: "HopDongs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_HopDongs_HoaDons_HoaDonId",
                table: "HopDongs",
                column: "HoaDonId",
                principalTable: "HoaDons",
                principalColumn: "HoaDonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HopDongs_HoaDons_HoaDonId",
                table: "HopDongs");

            migrationBuilder.AlterColumn<int>(
                name: "HoaDonId",
                table: "HopDongs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HopDongs_HoaDons_HoaDonId",
                table: "HopDongs",
                column: "HoaDonId",
                principalTable: "HoaDons",
                principalColumn: "HoaDonId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
