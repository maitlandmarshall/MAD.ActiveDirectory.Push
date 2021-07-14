using Microsoft.EntityFrameworkCore.Migrations;

namespace MAD.ActiveDirectory.Push.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    UserPrincipalName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GivenName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhysicalDeliveryOfficeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    C = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtensionAttribute1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtensionAttribute2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtensionAttribute3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtensionAttribute4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtensionAttribute5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manager = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
