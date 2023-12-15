using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_commerceApp.Data.Migrations
{
    public partial class UpdtaeProductColorEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductColors_Sizes_SizeId",
                table: "ProductColors");

            migrationBuilder.RenameColumn(
                name: "SizeId",
                table: "ProductColors",
                newName: "ColorId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductColors_SizeId",
                table: "ProductColors",
                newName: "IX_ProductColors_ColorId");

            migrationBuilder.AlterColumn<bool>(
                name: "ImageStatus",
                table: "Images",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductColors_Sizes_ColorId",
                table: "ProductColors",
                column: "ColorId",
                principalTable: "Sizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductColors_Sizes_ColorId",
                table: "ProductColors");

            migrationBuilder.RenameColumn(
                name: "ColorId",
                table: "ProductColors",
                newName: "SizeId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductColors_ColorId",
                table: "ProductColors",
                newName: "IX_ProductColors_SizeId");

            migrationBuilder.AlterColumn<bool>(
                name: "ImageStatus",
                table: "Images",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductColors_Sizes_SizeId",
                table: "ProductColors",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
