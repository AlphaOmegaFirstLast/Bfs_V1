using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bfs.StockEx.Data.Migrations
{
    /// <inheritdoc />
    public partial class InterestCurrency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "stkxSsPortfolio");

            migrationBuilder.AddColumn<long>(
                name: "CurrencyId",
                table: "stkxSsPortfolioBalance",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<decimal>(
                name: "Interest",
                table: "stkxSsPortfolio",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<long>(
                name: "CurrencyId",
                table: "stkxCashTransaction",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "stkxSsPortfolioBalance");

            migrationBuilder.DropColumn(
                name: "Interest",
                table: "stkxSsPortfolio");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "stkxCashTransaction");

            migrationBuilder.AddColumn<long>(
                name: "CurrencyId",
                table: "stkxSsPortfolio",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
