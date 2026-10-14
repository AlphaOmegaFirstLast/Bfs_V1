using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bfs.StockEx.Data.Migrations
{
    /// <inheritdoc />
    public partial class transactionDeleteName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Interest",
                table: "stkxSsPortfolio");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "stkxCashTransaction");

            migrationBuilder.AddColumn<long>(
                name: "CurrencyId",
                table: "stkxSspTransaction",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CurrencyId",
                table: "stkxSspStock",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "stkxSspTransaction");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "stkxSspStock");

            migrationBuilder.AddColumn<decimal>(
                name: "Interest",
                table: "stkxSsPortfolio",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "stkxCashTransaction",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
