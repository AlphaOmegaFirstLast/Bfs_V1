using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bfs.StockEx.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateCashTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CashTransactionTypeId",
                table: "stkxCashTransaction");

            migrationBuilder.AddColumn<int>(
                name: "TransactionTypeId",
                table: "stkxCashTransaction",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionTypeId",
                table: "stkxCashTransaction");

            migrationBuilder.AddColumn<long>(
                name: "CashTransactionTypeId",
                table: "stkxCashTransaction",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
