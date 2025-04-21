using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookmarks.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EnableInIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bookmarks_UserId_ArticleId",
                table: "Bookmarks");

            migrationBuilder.CreateIndex(
                name: "IX_Bookmarks_UserId_ArticleId_Enabled",
                table: "Bookmarks",
                columns: new[] { "UserId", "ArticleId", "Enabled" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bookmarks_UserId_ArticleId_Enabled",
                table: "Bookmarks");

            migrationBuilder.CreateIndex(
                name: "IX_Bookmarks_UserId_ArticleId",
                table: "Bookmarks",
                columns: new[] { "UserId", "ArticleId" },
                unique: true);
        }
    }
}
