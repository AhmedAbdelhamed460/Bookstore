using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookstore.Migrations
{
    /// <inheritdoc />
    public partial class v5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "appUserID",
                table: "Reviews",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_appUserID",
                table: "Reviews",
                column: "appUserID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AspNetUsers_appUserID",
                table: "Reviews",
                column: "appUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropForeignKey(
              name: "FK_Reviews_AspNetUsers_appUserID",
              table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_appUserID",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "appUserID",
                table: "Reviews");

        }
    }
}
