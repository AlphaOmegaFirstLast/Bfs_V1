using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bfs.StockEx.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateSspTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TransactionTypeId",
                table: "stkxSspTransaction",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<decimal>(
                name: "ToQuantity",
                table: "stkxSspTransaction",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ToQuantity",
                table: "stkxSspTransaction");

            migrationBuilder.AlterColumn<long>(
                name: "TransactionTypeId",
                table: "stkxSspTransaction",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
