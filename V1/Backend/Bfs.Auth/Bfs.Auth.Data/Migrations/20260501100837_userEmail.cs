using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bfs.Auth.Data.Migrations
{
    /// <inheritdoc />
    public partial class userEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "athUser",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "athUser");
        }
    }
}
