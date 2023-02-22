using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookstore.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Authors_AuthorID",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Categorys_CategoryID",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Publishers_PublisherID",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "date",
                table: "Reviews",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "review",
                table: "Reviews",
                newName: "ReviewText");

            migrationBuilder.RenameColumn(
                name: "shopingDate",
                table: "Orders",
                newName: "ShopingDate");

            migrationBuilder.RenameColumn(
                name: "discount",
                table: "Orders",
                newName: "Discount");

            migrationBuilder.RenameColumn(
                name: "arrivalDate",
                table: "Orders",
                newName: "ArrivalDate");

            migrationBuilder.RenameColumn(
                name: "PublisherID",
                table: "Books",
                newName: "publisherID");

            migrationBuilder.RenameColumn(
                name: "CategoryID",
                table: "Books",
                newName: "categoryID");

            migrationBuilder.RenameColumn(
                name: "AuthorID",
                table: "Books",
                newName: "authorID");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Books",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Books_PublisherID",
                table: "Books",
                newName: "IX_Books_publisherID");

            migrationBuilder.RenameIndex(
                name: "IX_Books_CategoryID",
                table: "Books",
                newName: "IX_Books_categoryID");

            migrationBuilder.RenameIndex(
                name: "IX_Books_AuthorID",
                table: "Books",
                newName: "IX_Books_authorID");

            migrationBuilder.RenameColumn(
                name: "lastname",
                table: "Authors",
                newName: "Lastname");

            migrationBuilder.RenameColumn(
                name: "firstname",
                table: "Authors",
                newName: "Firstname");

            migrationBuilder.CreateTable(
                name: "orderDetails",
                columns: table => new
                {
                    orderId = table.Column<int>(type: "int", nullable: false),
                    bookId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orderDetails", x => new { x.orderId, x.bookId });
                    table.ForeignKey(
                        name: "FK_orderDetails_Books_bookId",
                        column: x => x.bookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_orderDetails_Orders_orderId",
                        column: x => x.orderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_orderDetails_bookId",
                table: "orderDetails",
                column: "bookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Authors_authorID",
                table: "Books",
                column: "authorID",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Categorys_categoryID",
                table: "Books",
                column: "categoryID",
                principalTable: "Categorys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Publishers_publisherID",
                table: "Books",
                column: "publisherID",
                principalTable: "Publishers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Authors_authorID",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Categorys_categoryID",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Publishers_publisherID",
                table: "Books");

            migrationBuilder.DropTable(
                name: "orderDetails");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Reviews",
                newName: "date");

            migrationBuilder.RenameColumn(
                name: "ReviewText",
                table: "Reviews",
                newName: "review");

            migrationBuilder.RenameColumn(
                name: "ShopingDate",
                table: "Orders",
                newName: "shopingDate");

            migrationBuilder.RenameColumn(
                name: "Discount",
                table: "Orders",
                newName: "discount");

            migrationBuilder.RenameColumn(
                name: "ArrivalDate",
                table: "Orders",
                newName: "arrivalDate");

            migrationBuilder.RenameColumn(
                name: "publisherID",
                table: "Books",
                newName: "PublisherID");

            migrationBuilder.RenameColumn(
                name: "categoryID",
                table: "Books",
                newName: "CategoryID");

            migrationBuilder.RenameColumn(
                name: "authorID",
                table: "Books",
                newName: "AuthorID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Books",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_Books_publisherID",
                table: "Books",
                newName: "IX_Books_PublisherID");

            migrationBuilder.RenameIndex(
                name: "IX_Books_categoryID",
                table: "Books",
                newName: "IX_Books_CategoryID");

            migrationBuilder.RenameIndex(
                name: "IX_Books_authorID",
                table: "Books",
                newName: "IX_Books_AuthorID");

            migrationBuilder.RenameColumn(
                name: "Lastname",
                table: "Authors",
                newName: "lastname");

            migrationBuilder.RenameColumn(
                name: "Firstname",
                table: "Authors",
                newName: "firstname");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Authors_AuthorID",
                table: "Books",
                column: "AuthorID",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Categorys_CategoryID",
                table: "Books",
                column: "CategoryID",
                principalTable: "Categorys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Publishers_PublisherID",
                table: "Books",
                column: "PublisherID",
                principalTable: "Publishers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
