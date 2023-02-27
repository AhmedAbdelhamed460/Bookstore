using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookstore.Migrations
{
    /// <inheritdoc />
    public partial class nouhv1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_shopingCarts_bookId",
                table: "shopingCarts");

            migrationBuilder.CreateIndex(
                name: "IX_shopingCarts_AppUserId",
                table: "shopingCarts",
                column: "AppUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_shopingCarts_bookId",
                table: "shopingCarts",
                column: "bookId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_shopingCarts_AppUserId",
                table: "shopingCarts");

            migrationBuilder.DropIndex(
                name: "IX_shopingCarts_bookId",
                table: "shopingCarts");

            migrationBuilder.CreateIndex(
                name: "IX_shopingCarts_bookId",
                table: "shopingCarts",
                column: "bookId");
        }
    }
}
