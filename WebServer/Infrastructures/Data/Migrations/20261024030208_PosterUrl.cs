using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebServer.Infrastructures.Data.Migrations
{
    /// <inheritdoc />
    public partial class PosterUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "relative_path",
                table: "poster",
                newName: "full_path_url");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "full_path_url",
                table: "poster",
                newName: "relative_path");
        }
    }
}
