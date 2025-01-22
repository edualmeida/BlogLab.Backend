using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Articles.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveColorColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Colors_ColorId",
                table: "Articles");

            migrationBuilder.DropIndex(
                name: "IX_Articles_ColorId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "ColorId",
                table: "Articles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ColorId",
                table: "Articles",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Articles_ColorId",
                table: "Articles",
                column: "ColorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Colors_ColorId",
                table: "Articles",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
