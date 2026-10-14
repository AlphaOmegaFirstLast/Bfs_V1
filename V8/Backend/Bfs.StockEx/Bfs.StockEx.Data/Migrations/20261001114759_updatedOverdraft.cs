using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bfs.StockEx.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatedOverdraft : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OverdraftValue",
                table: "stkxOverdraftPortfolio",
                newName: "OverdraftPercent");

            migrationBuilder.AddColumn<long>(
                name: "BrokerAgreementId",
                table: "stkxOverdraftPortfolio",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<decimal>(
                name: "OverdraftMax",
                table: "stkxOverdraftPortfolio",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BrokerAgreementId",
                table: "stkxOverdraftPortfolio");

            migrationBuilder.DropColumn(
                name: "OverdraftMax",
                table: "stkxOverdraftPortfolio");

            migrationBuilder.RenameColumn(
                name: "OverdraftPercent",
                table: "stkxOverdraftPortfolio",
                newName: "OverdraftValue");
        }
    }
}
