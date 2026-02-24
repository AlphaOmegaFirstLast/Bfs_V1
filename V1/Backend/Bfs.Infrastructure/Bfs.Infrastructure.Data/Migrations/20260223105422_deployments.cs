using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bfs.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class deployments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Project",
                table: "DeploymentLocal");

            migrationBuilder.RenameColumn(
                name: "TargetVirtualFolder",
                table: "DeploymentLocal",
                newName: "TargetVirtualDir");

            migrationBuilder.RenameColumn(
                name: "HttpsRequired",
                table: "DeploymentLocal",
                newName: "IsHttpsRequired");

            migrationBuilder.RenameColumn(
                name: "TargetVirtualFolder",
                table: "DeploymentAzure",
                newName: "TargetVirtualDir");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TargetVirtualDir",
                table: "DeploymentLocal",
                newName: "TargetVirtualFolder");

            migrationBuilder.RenameColumn(
                name: "IsHttpsRequired",
                table: "DeploymentLocal",
                newName: "HttpsRequired");

            migrationBuilder.RenameColumn(
                name: "TargetVirtualDir",
                table: "DeploymentAzure",
                newName: "TargetVirtualFolder");

            migrationBuilder.AddColumn<string>(
                name: "Project",
                table: "DeploymentLocal",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
