using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bfs.StockEx.Data.Migrations
{
    /// <inheritdoc />
    public partial class invstrCashAgreement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "InvestorId",
                table: "stkxSsPortfolio",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "InvestorId",
                table: "stkxInvestorBrokerFund",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "stkxInvestor",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "stkxInvestor",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "EffectId",
                table: "stkxCashTransactionType",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "CashTransactionTypeId",
                table: "stkxCashTransaction",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ExpensesTypeId",
                table: "stkxCashTransaction",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Source",
                table: "stkxCashTransaction",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "SourceDate",
                table: "stkxCashTransaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "SsPortfolioId",
                table: "stkxCashTransaction",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "SspTransactionId",
                table: "stkxCashTransaction",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "TransactionDate",
                table: "stkxCashTransaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "Value",
                table: "stkxCashTransaction",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "AgreementDate",
                table: "stkxBrokerAgreement",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "InvestorId",
                table: "stkxBrokerAgreement",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<decimal>(
                name: "OverdraftMx",
                table: "stkxBrokerAgreement",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "OverdraftPrcnt",
                table: "stkxBrokerAgreement",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<long>(
                name: "SsPortfolioId",
                table: "stkxBrokerAgreement",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "stkxEffect",
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
                    table.PrimaryKey("PK_stkxEffect", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "stkxEffect");

            migrationBuilder.DropColumn(
                name: "InvestorId",
                table: "stkxSsPortfolio");

            migrationBuilder.DropColumn(
                name: "InvestorId",
                table: "stkxInvestorBrokerFund");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "stkxInvestor");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "stkxInvestor");

            migrationBuilder.DropColumn(
                name: "EffectId",
                table: "stkxCashTransactionType");

            migrationBuilder.DropColumn(
                name: "CashTransactionTypeId",
                table: "stkxCashTransaction");

            migrationBuilder.DropColumn(
                name: "ExpensesTypeId",
                table: "stkxCashTransaction");

            migrationBuilder.DropColumn(
                name: "Source",
                table: "stkxCashTransaction");

            migrationBuilder.DropColumn(
                name: "SourceDate",
                table: "stkxCashTransaction");

            migrationBuilder.DropColumn(
                name: "SsPortfolioId",
                table: "stkxCashTransaction");

            migrationBuilder.DropColumn(
                name: "SspTransactionId",
                table: "stkxCashTransaction");

            migrationBuilder.DropColumn(
                name: "TransactionDate",
                table: "stkxCashTransaction");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "stkxCashTransaction");

            migrationBuilder.DropColumn(
                name: "AgreementDate",
                table: "stkxBrokerAgreement");

            migrationBuilder.DropColumn(
                name: "InvestorId",
                table: "stkxBrokerAgreement");

            migrationBuilder.DropColumn(
                name: "OverdraftMx",
                table: "stkxBrokerAgreement");

            migrationBuilder.DropColumn(
                name: "OverdraftPrcnt",
                table: "stkxBrokerAgreement");

            migrationBuilder.DropColumn(
                name: "SsPortfolioId",
                table: "stkxBrokerAgreement");
        }
    }
}
