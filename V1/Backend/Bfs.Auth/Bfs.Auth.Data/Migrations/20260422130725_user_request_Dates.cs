using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bfs.Auth.Data.Migrations
{
    /// <inheritdoc />
    public partial class user_request_Dates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "athRoleUser");

            migrationBuilder.AddColumn<DateTime>(
                name: "RequestDate",
                table: "athUserRequest",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ResponseDate",
                table: "athUserRequest",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "athUserRequest",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "UserRequestStatusId",
                table: "athUserRequest",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestDate",
                table: "athUserRequest");

            migrationBuilder.DropColumn(
                name: "ResponseDate",
                table: "athUserRequest");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "athUserRequest");

            migrationBuilder.DropColumn(
                name: "UserRequestStatusId",
                table: "athUserRequest");

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "athRoleUser",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
