using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bfs.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class bfsFieldRemoveFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsJoinField",
                table: "BfsField");

            migrationBuilder.DropColumn(
                name: "IsQueryColumn",
                table: "BfsField");

            migrationBuilder.DropColumn(
                name: "ParentTable",
                table: "BfsField");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsJoinField",
                table: "BfsField",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsQueryColumn",
                table: "BfsField",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ParentTable",
                table: "BfsField",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
