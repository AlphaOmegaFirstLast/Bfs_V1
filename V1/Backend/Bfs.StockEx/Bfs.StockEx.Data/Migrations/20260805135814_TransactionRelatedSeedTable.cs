using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bfs.StockEx.Data.Migrations
{
    /// <inheritdoc />
    public partial class TransactionRelatedSeedTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "stkxCashTransactionType");

            migrationBuilder.DropTable(
                name: "stkxEffect");

            migrationBuilder.AddColumn<int>(
                name: "CalculationMethodId",
                table: "stkxTransactionType",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EffectTypeId",
                table: "stkxTransactionType",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SourceTypeId",
                table: "stkxTransactionType",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StockEntityTypeId",
                table: "stkxTransactionType",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StockFieldTypeId",
                table: "stkxTransactionType",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "stkxCalculationMethod",
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
                    table.PrimaryKey("PK_stkxCalculationMethod", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "stkxEffectType",
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
                    table.PrimaryKey("PK_stkxEffectType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "stkxSourceType",
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
                    table.PrimaryKey("PK_stkxSourceType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "stkxStockEntityType",
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
                    table.PrimaryKey("PK_stkxStockEntityType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "stkxStockFieldType",
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
                    table.PrimaryKey("PK_stkxStockFieldType", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "stkxCalculationMethod");

            migrationBuilder.DropTable(
                name: "stkxEffectType");

            migrationBuilder.DropTable(
                name: "stkxSourceType");

            migrationBuilder.DropTable(
                name: "stkxStockEntityType");

            migrationBuilder.DropTable(
                name: "stkxStockFieldType");

            migrationBuilder.DropColumn(
                name: "CalculationMethodId",
                table: "stkxTransactionType");

            migrationBuilder.DropColumn(
                name: "EffectTypeId",
                table: "stkxTransactionType");

            migrationBuilder.DropColumn(
                name: "SourceTypeId",
                table: "stkxTransactionType");

            migrationBuilder.DropColumn(
                name: "StockEntityTypeId",
                table: "stkxTransactionType");

            migrationBuilder.DropColumn(
                name: "StockFieldTypeId",
                table: "stkxTransactionType");

            migrationBuilder.CreateTable(
                name: "stkxCashTransactionType",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    EffectId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenantId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stkxCashTransactionType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "stkxEffect",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenantId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stkxEffect", x => x.Id);
                });
        }
    }
}
