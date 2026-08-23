using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bfs.StockEx.Data.Migrations
{
    /// <inheritdoc />
    public partial class allTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CurrencyId",
                table: "stkxStockShare",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TradingRoomId",
                table: "stkxStockShare",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "stkxSspTransaction",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Quantity",
                table: "stkxSspTransaction",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Source",
                table: "stkxSspTransaction",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "SourceDate",
                table: "stkxSspTransaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "SsPortfolioId",
                table: "stkxSspTransaction",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "StockShareId",
                table: "stkxSspTransaction",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "TransactionDate",
                table: "stkxSspTransaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "TransactionTypeId",
                table: "stkxSspTransaction",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<decimal>(
                name: "AverageCost",
                table: "stkxSspStock",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Quantity",
                table: "stkxSspStock",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<long>(
                name: "SsPortfolioId",
                table: "stkxSspStock",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "StockShareId",
                table: "stkxSspStock",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<decimal>(
                name: "Balance",
                table: "stkxSsPortfolioBalance",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<long>(
                name: "SsPortfolioId",
                table: "stkxSsPortfolioBalance",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<decimal>(
                name: "OverdraftValue",
                table: "stkxOverdraftPortfolio",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<long>(
                name: "SsPortfolioId",
                table: "stkxOverdraftPortfolio",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<decimal>(
                name: "Fund",
                table: "stkxInvestorBrokerFund",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "FundDate",
                table: "stkxInvestorBrokerFund",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "stkxCurrentPrice",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<long>(
                name: "StockShareId",
                table: "stkxCurrentPrice",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "TransactionDate",
                table: "stkxCurrentPrice",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "AnnounceDate",
                table: "stkxCoupon",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CouponStatusId",
                table: "stkxCoupon",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CouponTypeId",
                table: "stkxCoupon",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "stkxCoupon",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "StockShareId",
                table: "stkxCoupon",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TradingRoomId",
                table: "stkxCoupon",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<decimal>(
                name: "Value",
                table: "stkxCoupon",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "ValueDate",
                table: "stkxCoupon",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "stkxStockShare");

            migrationBuilder.DropColumn(
                name: "TradingRoomId",
                table: "stkxStockShare");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "stkxSspTransaction");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "stkxSspTransaction");

            migrationBuilder.DropColumn(
                name: "Source",
                table: "stkxSspTransaction");

            migrationBuilder.DropColumn(
                name: "SourceDate",
                table: "stkxSspTransaction");

            migrationBuilder.DropColumn(
                name: "SsPortfolioId",
                table: "stkxSspTransaction");

            migrationBuilder.DropColumn(
                name: "StockShareId",
                table: "stkxSspTransaction");

            migrationBuilder.DropColumn(
                name: "TransactionDate",
                table: "stkxSspTransaction");

            migrationBuilder.DropColumn(
                name: "TransactionTypeId",
                table: "stkxSspTransaction");

            migrationBuilder.DropColumn(
                name: "AverageCost",
                table: "stkxSspStock");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "stkxSspStock");

            migrationBuilder.DropColumn(
                name: "SsPortfolioId",
                table: "stkxSspStock");

            migrationBuilder.DropColumn(
                name: "StockShareId",
                table: "stkxSspStock");

            migrationBuilder.DropColumn(
                name: "Balance",
                table: "stkxSsPortfolioBalance");

            migrationBuilder.DropColumn(
                name: "SsPortfolioId",
                table: "stkxSsPortfolioBalance");

            migrationBuilder.DropColumn(
                name: "OverdraftValue",
                table: "stkxOverdraftPortfolio");

            migrationBuilder.DropColumn(
                name: "SsPortfolioId",
                table: "stkxOverdraftPortfolio");

            migrationBuilder.DropColumn(
                name: "Fund",
                table: "stkxInvestorBrokerFund");

            migrationBuilder.DropColumn(
                name: "FundDate",
                table: "stkxInvestorBrokerFund");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "stkxCurrentPrice");

            migrationBuilder.DropColumn(
                name: "StockShareId",
                table: "stkxCurrentPrice");

            migrationBuilder.DropColumn(
                name: "TransactionDate",
                table: "stkxCurrentPrice");

            migrationBuilder.DropColumn(
                name: "AnnounceDate",
                table: "stkxCoupon");

            migrationBuilder.DropColumn(
                name: "CouponStatusId",
                table: "stkxCoupon");

            migrationBuilder.DropColumn(
                name: "CouponTypeId",
                table: "stkxCoupon");

            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "stkxCoupon");

            migrationBuilder.DropColumn(
                name: "StockShareId",
                table: "stkxCoupon");

            migrationBuilder.DropColumn(
                name: "TradingRoomId",
                table: "stkxCoupon");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "stkxCoupon");

            migrationBuilder.DropColumn(
                name: "ValueDate",
                table: "stkxCoupon");
        }
    }
}
