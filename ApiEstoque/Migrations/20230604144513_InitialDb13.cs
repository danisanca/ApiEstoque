using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiEstoque.Migrations
{
    /// <inheritdoc />
    public partial class InitialDb13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionsHistory_Shop_ShopId",
                table: "TransactionsHistory");

            migrationBuilder.DropIndex(
                name: "IX_TransactionsHistory_ShopId",
                table: "TransactionsHistory");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "TransactionsHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "TransactionsHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TransactionsHistory_ProductId",
                table: "TransactionsHistory",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionsHistory_ShopId",
                table: "TransactionsHistory",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionsHistory_UserId",
                table: "TransactionsHistory",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionsHistory_Products_ProductId",
                table: "TransactionsHistory",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionsHistory_Shop_ShopId",
                table: "TransactionsHistory",
                column: "ShopId",
                principalTable: "Shop",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionsHistory_Users_UserId",
                table: "TransactionsHistory",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionsHistory_Products_ProductId",
                table: "TransactionsHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionsHistory_Shop_ShopId",
                table: "TransactionsHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionsHistory_Users_UserId",
                table: "TransactionsHistory");

            migrationBuilder.DropIndex(
                name: "IX_TransactionsHistory_ProductId",
                table: "TransactionsHistory");

            migrationBuilder.DropIndex(
                name: "IX_TransactionsHistory_ShopId",
                table: "TransactionsHistory");

            migrationBuilder.DropIndex(
                name: "IX_TransactionsHistory_UserId",
                table: "TransactionsHistory");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "TransactionsHistory");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TransactionsHistory");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionsHistory_ShopId",
                table: "TransactionsHistory",
                column: "ShopId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionsHistory_Shop_ShopId",
                table: "TransactionsHistory",
                column: "ShopId",
                principalTable: "Shop",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
