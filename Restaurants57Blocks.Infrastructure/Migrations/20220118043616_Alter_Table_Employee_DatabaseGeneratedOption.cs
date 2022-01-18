using Microsoft.EntityFrameworkCore.Migrations;

namespace Restaurants57Blocks.Infrastructure.Migrations
{
    public partial class Alter_Table_Employee_DatabaseGeneratedOption : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Identification",
                table: "Employee",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Employee",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Employee",
                newName: "Identification");

            migrationBuilder.AlterColumn<int>(
                name: "Identification",
                table: "Employee",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");
        }
    }
}
