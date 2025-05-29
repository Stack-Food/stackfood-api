using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StackFood.Infra.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalPaymentId",
                table: "payments");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "payments",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "payments",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "ExternalPaymentId",
                table: "payments",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
