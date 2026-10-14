using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bfs.Master.Data.Migrations
{
    /// <inheritdoc />
    public partial class ActionSystemId_DeploymentName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "BfsSystemId",
                table: "SystemAction",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "DeploymentLocal",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "DeploymentAzure",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "BfsSystemId",
                table: "BusinessAction",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BfsSystemId",
                table: "SystemAction");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "DeploymentLocal");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "DeploymentAzure");

            migrationBuilder.DropColumn(
                name: "BfsSystemId",
                table: "BusinessAction");
        }
    }
}
