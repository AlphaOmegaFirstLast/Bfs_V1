using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bfs.StockEx.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatedBrokerAgreement2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OverdraftMx",
                table: "stkxBrokerAgreement");

            migrationBuilder.DropColumn(
                name: "OverdraftPrcnt",
                table: "stkxBrokerAgreement");

            migrationBuilder.DropColumn(
                name: "SsPortfolioId",
                table: "stkxBrokerAgreement");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "OverdraftMx",
                table: "stkxBrokerAgreement",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "OverdraftPrcnt",
                table: "stkxBrokerAgreement",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<long>(
                name: "SsPortfolioId",
                table: "stkxBrokerAgreement",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
