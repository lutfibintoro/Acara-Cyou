using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebServer.Infrastructures.Data.Migrations
{
    /// <inheritdoc />
    public partial class EmailStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_email_send",
                table: "order",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_email_send",
                table: "order");
        }
    }
}
