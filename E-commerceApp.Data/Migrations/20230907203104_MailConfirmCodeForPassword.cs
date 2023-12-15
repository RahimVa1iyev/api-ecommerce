using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_commerceApp.Data.Migrations
{
    public partial class MailConfirmCodeForPassword : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MailConfirmCodeForPassword",
                table: "AspNetUsers",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MailConfirmCodeForPassword",
                table: "AspNetUsers");
        }
    }
}
