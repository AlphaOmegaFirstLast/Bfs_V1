using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bfs.Master.Data.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActionLocation",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TenantId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionLocation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ActionType",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TenantId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AggregateType",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TenantId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AggregateType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BackendDataType",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TenantId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BackendDataType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BfsClient",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TenantId = table.Column<long>(type: "bigint", nullable: false),
                    DbConnection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomFields = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BfsClient", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BfsComponent",
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
                    QueryBaseTable = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InterfaceRequired = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BfsSystemId = table.Column<long>(type: "bigint", nullable: false),
                    DataTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BfsComponent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BfsComponentBusinessAction",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TenantId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    BfsComponentId = table.Column<long>(type: "bigint", nullable: false),
                    BusinessActionId = table.Column<long>(type: "bigint", nullable: false),
                    ActionLocationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BfsComponentBusinessAction", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BfsComponentSystemAction",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TenantId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    BfsComponentId = table.Column<long>(type: "bigint", nullable: false),
                    SystemActionId = table.Column<long>(type: "bigint", nullable: false),
                    ActionLocationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BfsComponentSystemAction", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BfsField",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TenantId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Field = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BfsComponentId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_BfsField", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BfsSystem",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TenantId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsMaster = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BasePortNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DbPrefix = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemTemplateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BfsSystem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BfsTenant",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TenantId = table.Column<long>(type: "bigint", nullable: false),
                    DbConnection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Theme = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomFields = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BfsTenant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BfsTenantSystem",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TenantId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    BfsTenantId = table.Column<long>(type: "bigint", nullable: false),
                    BfsSystemId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BfsTenantSystem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusinessAction",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TenantId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessAction", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChartElement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TenantId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChartElement", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomFieldDefinition",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TenantId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BfsComponentId = table.Column<long>(type: "bigint", nullable: false),
                    FieldValidation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomFieldDefinition", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomReports",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TenantId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Request = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BaseReport = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPrivate = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomReports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DataType",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TenantId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeploymentAzure",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TenantId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ScriptFile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SourceProject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SourcePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublishPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Config = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnvironmentValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TargetVirtualDir = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublishProfilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppService = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResourceGroup = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BfsSystemId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeploymentAzure", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeploymentLocal",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TenantId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ScriptFile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SourceProject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SourcePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublishPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Config = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnvironmentValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TargetVirtualDir = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WebSite = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppPoolName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Port = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsHttpsRequired = table.Column<bool>(type: "bit", nullable: false),
                    BfsSystemId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeploymentLocal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FilterType",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TenantId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilterType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FormControlType",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TenantId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormControlType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemAction",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TenantId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MatchProperty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MatchValues = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionTemplate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionTypeId = table.Column<int>(type: "int", nullable: false),
                    WriterTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemAction", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemTemplate",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TenantId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OutputDirectory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SolutionDirectory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Template = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemTemplate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WriterType",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TenantId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WriterType", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActionLocation");

            migrationBuilder.DropTable(
                name: "ActionType");

            migrationBuilder.DropTable(
                name: "AggregateType");

            migrationBuilder.DropTable(
                name: "BackendDataType");

            migrationBuilder.DropTable(
                name: "BfsClient");

            migrationBuilder.DropTable(
                name: "BfsComponent");

            migrationBuilder.DropTable(
                name: "BfsComponentBusinessAction");

            migrationBuilder.DropTable(
                name: "BfsComponentSystemAction");

            migrationBuilder.DropTable(
                name: "BfsField");

            migrationBuilder.DropTable(
                name: "BfsSystem");

            migrationBuilder.DropTable(
                name: "BfsTenant");

            migrationBuilder.DropTable(
                name: "BfsTenantSystem");

            migrationBuilder.DropTable(
                name: "BusinessAction");

            migrationBuilder.DropTable(
                name: "ChartElement");

            migrationBuilder.DropTable(
                name: "CustomFieldDefinition");

            migrationBuilder.DropTable(
                name: "CustomReports");

            migrationBuilder.DropTable(
                name: "DataType");

            migrationBuilder.DropTable(
                name: "DeploymentAzure");

            migrationBuilder.DropTable(
                name: "DeploymentLocal");

            migrationBuilder.DropTable(
                name: "FilterType");

            migrationBuilder.DropTable(
                name: "FormControlType");

            migrationBuilder.DropTable(
                name: "SystemAction");

            migrationBuilder.DropTable(
                name: "SystemTemplate");

            migrationBuilder.DropTable(
                name: "WriterType");
        }
    }
}
