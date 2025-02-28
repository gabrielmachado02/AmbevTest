using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ambev.Sales.ORM.Migrations
{
    /// <inheritdoc />
    public partial class AlterFieldSaleNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SaleNumber",
                table: "Sales",
                type: "text",
                nullable: false,
                defaultValueSql: "LPAD((FLOOR(EXTRACT(EPOCH FROM clock_timestamp()) * 1000 + RANDOM() * 999)::BIGINT)::TEXT, 9, '0')",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.CreateIndex(
                name: "IX_Sales_SaleNumber",
                table: "Sales",
                column: "SaleNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Sales_SaleNumber",
                table: "Sales");

            migrationBuilder.AlterColumn<string>(
                name: "SaleNumber",
                table: "Sales",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValueSql: "LPAD((FLOOR(EXTRACT(EPOCH FROM clock_timestamp()) * 1000 + RANDOM() * 999)::BIGINT)::TEXT, 9, '0')");
        }
    }
}
