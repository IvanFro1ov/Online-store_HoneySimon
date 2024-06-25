using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sixgram.Auth.Database.Migrations
{
    /// <inheritdoc />
    public partial class File : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "Products",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte[]>(
                name: "File",
                table: "Products",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "File",
                table: "Products");
        }
    }
}
