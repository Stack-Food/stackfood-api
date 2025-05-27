using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StackFood.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentTypeColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "payments",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "payments");
        }
    }
}
