using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bfs.Stores.Data.Migrations
{
    /// <inheritdoc />
    public partial class documentName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "strDocument",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "strDocument");
        }
    }
}
