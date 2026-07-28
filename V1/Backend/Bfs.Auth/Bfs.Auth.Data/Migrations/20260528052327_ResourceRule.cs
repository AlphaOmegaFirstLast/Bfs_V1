using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bfs.Auth.Data.Migrations
{
    /// <inheritdoc />
    public partial class ResourceRule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "athResourceRule",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TenantId = table.Column<long>(type: "bigint", nullable: false),
                    SelectBlackList = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    BfsComponentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JoinStatement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WhereStatement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParameterName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParameterValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParameterType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    BfsComponentId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_athResourceRule", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "athResourceRule");
        }
    }
}
