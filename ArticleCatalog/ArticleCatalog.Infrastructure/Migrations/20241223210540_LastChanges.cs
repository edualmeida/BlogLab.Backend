using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Articles.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LastChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedOnUtc",
                table: "Articles",
                newName: "CreatedOnUTC");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedOnUTC",
                table: "Articles",
                newName: "CreatedOnUtc");
        }
    }
}
