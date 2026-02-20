using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bfs.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class SystemActionIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MatchProprty",
                table: "SystemAction",
                newName: "MatchProperty");

            migrationBuilder.AlterColumn<long>(
                name: "SystemActionId",
                table: "BfsComponentSystemAction",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MatchProperty",
                table: "SystemAction",
                newName: "MatchProprty");

            migrationBuilder.AlterColumn<int>(
                name: "SystemActionId",
                table: "BfsComponentSystemAction",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
