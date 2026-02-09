using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bfs.BestFit.Data.Migrations
{
    /// <inheritdoc />
    public partial class BestFitTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BestFitComponent",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TenantId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsSoftDelete = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MenuName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MenuPlaceHolder = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QueryBaseTable = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BestFitSystemId = table.Column<long>(type: "bigint", nullable: false),
                    DataTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BestFitComponent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BestFitField",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TenantId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Field = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsQueryColumn = table.Column<bool>(type: "bit", nullable: false),
                    IsJoinField = table.Column<bool>(type: "bit", nullable: false),
                    ParentTable = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ComponentId = table.Column<long>(type: "bigint", nullable: false),
                    FilterTypeId = table.Column<int>(type: "int", nullable: false),
                    BackendDataTypeId = table.Column<int>(type: "int", nullable: false),
                    FieldValidation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FormInfo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MatrixInfo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReportInfo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToolTipInfo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BestFitField", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BestFitSystem",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TenantId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BasePortNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DbPrefix = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientId = table.Column<long>(type: "bigint", nullable: false),
                    SystemTemplateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BestFitSystem", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BestFitComponent");

            migrationBuilder.DropTable(
                name: "BestFitField");

            migrationBuilder.DropTable(
                name: "BestFitSystem");
        }
    }
}
