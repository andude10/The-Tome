using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheTome.Migrations
{
    public partial class LinkToBookCover : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoverUrl",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverUrl",
                table: "Books");
        }
    }
}
