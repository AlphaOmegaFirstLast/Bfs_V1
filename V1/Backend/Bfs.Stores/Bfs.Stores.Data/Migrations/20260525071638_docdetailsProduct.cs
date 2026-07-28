using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bfs.Stores.Data.Migrations
{
    /// <inheritdoc />
    public partial class docdetailsProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StoreId",
                table: "strDocumentDetails",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "OperationId",
                table: "strDocumentDetails",
                newName: "UnitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UnitId",
                table: "strDocumentDetails",
                newName: "OperationId");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "strDocumentDetails",
                newName: "StoreId");
        }
    }
}
