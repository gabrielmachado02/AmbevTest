using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ambev.Sales.ORM.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "Sales",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "SaleItems",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ItemStatus",
                table: "SaleItems",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "SaleItems",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "SaleItems");

            migrationBuilder.DropColumn(
                name: "ItemStatus",
                table: "SaleItems");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "SaleItems");
        }
    }
}
