using Microsoft.EntityFrameworkCore.Migrations;

namespace MAD.ActiveDirectory.Push.Migrations
{
    public partial class User_Extension6And7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExtensionAttribute6",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExtensionAttribute7",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExtensionAttribute6",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ExtensionAttribute7",
                table: "User");
        }
    }
}
