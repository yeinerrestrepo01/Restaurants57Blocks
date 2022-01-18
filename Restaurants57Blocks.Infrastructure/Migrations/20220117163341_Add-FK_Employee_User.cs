using Microsoft.EntityFrameworkCore.Migrations;

namespace Restaurants57Blocks.Infrastructure.Migrations
{
    public partial class AddFK_Employee_User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Restaurant_RestaurantId",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "RestaurantId",
                table: "User",
                newName: "Identifcation");

            migrationBuilder.RenameIndex(
                name: "IX_User_RestaurantId",
                table: "User",
                newName: "IX_User_Identifcation");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Employee",
                newName: "Identifcation");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Employee_Identifcation",
                table: "User",
                column: "Identifcation",
                principalTable: "Employee",
                principalColumn: "Identifcation",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Employee_Identifcation",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "Identifcation",
                table: "User",
                newName: "RestaurantId");

            migrationBuilder.RenameIndex(
                name: "IX_User_Identifcation",
                table: "User",
                newName: "IX_User_RestaurantId");

            migrationBuilder.RenameColumn(
                name: "Identifcation",
                table: "Employee",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Restaurant_RestaurantId",
                table: "User",
                column: "RestaurantId",
                principalTable: "Restaurant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
