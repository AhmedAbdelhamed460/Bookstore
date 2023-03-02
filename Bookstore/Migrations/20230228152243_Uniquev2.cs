using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookstore.Migrations
{
    /// <inheritdoc />
    public partial class Uniquev2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_shopingCarts_bookId",
                table: "shopingCarts");

            migrationBuilder.CreateIndex(
                name: "IX_shopingCarts_bookId",
                table: "shopingCarts",
                column: "bookId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_shopingCarts_bookId",
                table: "shopingCarts");

            migrationBuilder.CreateIndex(
                name: "IX_shopingCarts_bookId",
                table: "shopingCarts",
                column: "bookId",
                unique: true);
        }
    }
}
