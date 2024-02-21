using Microsoft.EntityFrameworkCore.Migrations;

namespace MAD.ActiveDirectory.Push.Migrations
{
    public partial class User_AddColumn_AdditionalProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdditionalProperties",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdditionalProperties",
                table: "User");
        }
    }
}
