using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleCore.Common.Migrations
{
    /// <inheritdoc />
    public partial class _20250704003 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "UserIninfoSet",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "UserIninfoSet");
        }
    }
}
