using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebServer.Infrastructures.Data.Migrations
{
    /// <inheritdoc />
    public partial class ShopeePayQris : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "payment_method",
                keyColumn: "id",
                keyValue: new Guid("20000000-0000-0000-0000-00000000000d"),
                column: "name",
                value: "Nobu Qris");

            migrationBuilder.InsertData(
                table: "payment_method",
                columns: new[] { "id", "admin_fee_bearer", "code", "flat_fee", "max_expired_in_minute", "name", "payment_gateway_id", "percentage_fee", "type" },
                values: new object[] { new Guid("20000000-0000-0000-0000-00000000000e"), "Merchant", "SP", 0m, 24, "Shopeepay Qris", new Guid("10000000-0000-0000-0000-000000000000"), 0.7m, "Qris" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "payment_method",
                keyColumn: "id",
                keyValue: new Guid("20000000-0000-0000-0000-00000000000e"));

            migrationBuilder.UpdateData(
                table: "payment_method",
                keyColumn: "id",
                keyValue: new Guid("20000000-0000-0000-0000-00000000000d"),
                column: "name",
                value: "Nobu");
        }
    }
}
