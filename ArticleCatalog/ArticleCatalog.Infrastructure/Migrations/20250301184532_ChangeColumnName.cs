using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Articles.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedOnUTC",
                table: "Thumbnails",
                newName: "CreatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedOnUTC",
                table: "Categories",
                newName: "CreatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedOnUTC",
                table: "Articles",
                newName: "CreatedOnUtc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedOnUtc",
                table: "Thumbnails",
                newName: "CreatedOnUTC");

            migrationBuilder.RenameColumn(
                name: "CreatedOnUtc",
                table: "Categories",
                newName: "CreatedOnUTC");

            migrationBuilder.RenameColumn(
                name: "CreatedOnUtc",
                table: "Articles",
                newName: "CreatedOnUTC");
        }
    }
}
