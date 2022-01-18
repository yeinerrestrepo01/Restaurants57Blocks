using Microsoft.EntityFrameworkCore.Migrations;

namespace Restaurants57Blocks.Infrastructure.Migrations
{
    public partial class Alter_Name_Column_Identification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Identifcation",
                table: "Employee",
                newName: "Identification");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Identification",
                table: "Employee",
                newName: "Identifcation");
        }
    }
}
