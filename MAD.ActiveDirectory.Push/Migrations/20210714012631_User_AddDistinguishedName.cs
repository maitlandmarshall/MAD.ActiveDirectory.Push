using Microsoft.EntityFrameworkCore.Migrations;

namespace MAD.ActiveDirectory.Push.Migrations
{
    public partial class User_AddDistinguishedName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DistinguishedName",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DistinguishedName",
                table: "User");
        }
    }
}
