using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookstore.Migrations
{
    /// <inheritdoc />
    public partial class Uniquev1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_shopingCarts_AppUserId",
                table: "shopingCarts");

            migrationBuilder.CreateIndex(
                name: "IX_shopingCarts_AppUserId",
                table: "shopingCarts",
                column: "AppUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_shopingCarts_AppUserId",
                table: "shopingCarts");

            migrationBuilder.CreateIndex(
                name: "IX_shopingCarts_AppUserId",
                table: "shopingCarts",
                column: "AppUserId",
                unique: true);
        }
    }
}
