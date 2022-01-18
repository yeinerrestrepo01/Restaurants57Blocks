using Microsoft.EntityFrameworkCore.Migrations;

namespace Restaurants57Blocks.Infrastructure.Migrations
{
    public partial class Alter_FK_Employee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Employee_Identifcation",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "Identifcation",
                table: "User",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_User_Identifcation",
                table: "User",
                newName: "IX_User_EmployeeId");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Employee",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Employee_EmployeeId",
                table: "User",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Identifcation",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Employee_EmployeeId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Employee");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "User",
                newName: "Identifcation");

            migrationBuilder.RenameIndex(
                name: "IX_User_EmployeeId",
                table: "User",
                newName: "IX_User_Identifcation");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Employee_Identifcation",
                table: "User",
                column: "Identifcation",
                principalTable: "Employee",
                principalColumn: "Identifcation",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
