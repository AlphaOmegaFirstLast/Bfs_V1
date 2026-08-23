using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bfs.Master.Data.Migrations
{
    /// <inheritdoc />
    public partial class init_Master6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BfsClient");

            migrationBuilder.AddColumn<string>(
                name: "ActionTemplate",
                table: "BusinessAction",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MatchProperty",
                table: "BusinessAction",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MatchValues",
                table: "BusinessAction",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "WriterTypeId",
                table: "BusinessAction",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActionTemplate",
                table: "BusinessAction");

            migrationBuilder.DropColumn(
                name: "MatchProperty",
                table: "BusinessAction");

            migrationBuilder.DropColumn(
                name: "MatchValues",
                table: "BusinessAction");

            migrationBuilder.DropColumn(
                name: "WriterTypeId",
                table: "BusinessAction");

            migrationBuilder.CreateTable(
                name: "BfsClient",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    DbConnection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenantId = table.Column<long>(type: "bigint", nullable: false),
                    CustomFields = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BfsClient", x => x.Id);
                });
        }
    }
}
