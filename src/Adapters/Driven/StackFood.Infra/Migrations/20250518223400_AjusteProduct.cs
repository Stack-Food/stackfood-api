using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StackFood.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AjusteProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_product_orders_products_ProductId",
                table: "product_orders");

            migrationBuilder.DropIndex(
                name: "IX_product_orders_ProductId",
                table: "product_orders");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "product_orders",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "product_orders",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "product_orders",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "product_orders",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "product_orders");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "product_orders");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "product_orders");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "product_orders");

            migrationBuilder.CreateIndex(
                name: "IX_product_orders_ProductId",
                table: "product_orders",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_product_orders_products_ProductId",
                table: "product_orders",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
