using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Application.Migrations
{
    /// <inheritdoc />
    public partial class foreginkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_ProductDetails_ProductDetailsProductId",
                table: "ProductImages");

            migrationBuilder.DropIndex(
                name: "IX_ProductImages_ProductDetailsProductId",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "ProductDetailsProductId",
                table: "ProductImages");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_ProductDetails_ProductId",
                table: "ProductImages",
                column: "ProductId",
                principalTable: "ProductDetails",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_ProductDetails_ProductId",
                table: "ProductImages");

            migrationBuilder.DropIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages");

            migrationBuilder.AddColumn<int>(
                name: "ProductDetailsProductId",
                table: "ProductImages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductDetailsProductId",
                table: "ProductImages",
                column: "ProductDetailsProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_ProductDetails_ProductDetailsProductId",
                table: "ProductImages",
                column: "ProductDetailsProductId",
                principalTable: "ProductDetails",
                principalColumn: "ProductId");
        }
    }
}
