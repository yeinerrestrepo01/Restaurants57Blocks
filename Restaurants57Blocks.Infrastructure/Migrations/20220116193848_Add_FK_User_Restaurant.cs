using Microsoft.EntityFrameworkCore.Migrations;

namespace Restaurants57Blocks.Infrastructure.Migrations
{
    public partial class Add_FK_User_Restaurant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RestaurantId",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_User_RestaurantId",
                table: "User",
                column: "RestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Restaurant_RestaurantId",
                table: "User",
                column: "RestaurantId",
                principalTable: "Restaurant",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Restaurant_RestaurantId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_RestaurantId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "RestaurantId",
                table: "User");
        }
    }
}
