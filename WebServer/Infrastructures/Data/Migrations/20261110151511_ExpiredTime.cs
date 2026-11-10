using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebServer.Infrastructures.Data.Migrations
{
    /// <inheritdoc />
    public partial class ExpiredTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "max_expired_in_minute",
                table: "payment_method",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "payment_method",
                keyColumn: "id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000001"),
                column: "max_expired_in_minute",
                value: 1440);

            migrationBuilder.UpdateData(
                table: "payment_method",
                keyColumn: "id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000002"),
                column: "max_expired_in_minute",
                value: 1440);

            migrationBuilder.UpdateData(
                table: "payment_method",
                keyColumn: "id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000003"),
                column: "max_expired_in_minute",
                value: 1440);

            migrationBuilder.UpdateData(
                table: "payment_method",
                keyColumn: "id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000004"),
                column: "max_expired_in_minute",
                value: 1440);

            migrationBuilder.UpdateData(
                table: "payment_method",
                keyColumn: "id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000005"),
                column: "max_expired_in_minute",
                value: 1440);

            migrationBuilder.UpdateData(
                table: "payment_method",
                keyColumn: "id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000006"),
                column: "max_expired_in_minute",
                value: 1440);

            migrationBuilder.UpdateData(
                table: "payment_method",
                keyColumn: "id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000007"),
                column: "max_expired_in_minute",
                value: 1440);

            migrationBuilder.UpdateData(
                table: "payment_method",
                keyColumn: "id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000008"),
                column: "max_expired_in_minute",
                value: 1440);

            migrationBuilder.UpdateData(
                table: "payment_method",
                keyColumn: "id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000009"),
                column: "max_expired_in_minute",
                value: 1440);

            migrationBuilder.UpdateData(
                table: "payment_method",
                keyColumn: "id",
                keyValue: new Guid("20000000-0000-0000-0000-00000000000a"),
                column: "max_expired_in_minute",
                value: 1440);

            migrationBuilder.UpdateData(
                table: "payment_method",
                keyColumn: "id",
                keyValue: new Guid("20000000-0000-0000-0000-00000000000b"),
                column: "max_expired_in_minute",
                value: 1440);

            migrationBuilder.UpdateData(
                table: "payment_method",
                keyColumn: "id",
                keyValue: new Guid("20000000-0000-0000-0000-00000000000c"),
                column: "max_expired_in_minute",
                value: 1440);

            migrationBuilder.UpdateData(
                table: "payment_method",
                keyColumn: "id",
                keyValue: new Guid("20000000-0000-0000-0000-00000000000d"),
                column: "max_expired_in_minute",
                value: 24);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "max_expired_in_minute",
                table: "payment_method");
        }
    }
}
