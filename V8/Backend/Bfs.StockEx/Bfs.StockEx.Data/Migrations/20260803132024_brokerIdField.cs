using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bfs.StockEx.Data.Migrations
{
    /// <inheritdoc />
    public partial class brokerIdField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "BrokerId",
                table: "stkxSsPortfolio",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "BrokerId",
                table: "stkxInvestorBrokerFund",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "BrokerId",
                table: "stkxBrokerAgreement",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "stkxBroker",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "stkxBroker",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "TradingRoomId",
                table: "stkxBroker",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BrokerId",
                table: "stkxSsPortfolio");

            migrationBuilder.DropColumn(
                name: "BrokerId",
                table: "stkxInvestorBrokerFund");

            migrationBuilder.DropColumn(
                name: "BrokerId",
                table: "stkxBrokerAgreement");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "stkxBroker");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "stkxBroker");

            migrationBuilder.DropColumn(
                name: "TradingRoomId",
                table: "stkxBroker");
        }
    }
}
