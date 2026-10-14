using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bfs.StockEx.Data.Migrations
{
    /// <inheritdoc />
    public partial class updSspTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "stkxSspTransaction");

            migrationBuilder.AddColumn<long>(
                name: "ToSspTransactionId",
                table: "stkxSspTransaction",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ToSspTransactionId",
                table: "stkxSspTransaction");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "stkxSspTransaction",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
