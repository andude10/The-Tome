using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheTome.Core.Migrations
{
    public partial class Snapshot03 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AboutUser",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AboutUser",
                table: "Users");
        }
    }
}
