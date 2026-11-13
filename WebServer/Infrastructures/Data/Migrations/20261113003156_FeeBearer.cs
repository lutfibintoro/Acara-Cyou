using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebServer.Infrastructures.Data.Migrations
{
    /// <inheritdoc />
    public partial class FeeBearer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "admin_fee_bearer",
                table: "payment_detail",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "admin_fee_bearer",
                table: "payment_detail");
        }
    }
}
