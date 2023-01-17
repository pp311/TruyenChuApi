using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebTruyenChu_Backend.Migrations
{
    public partial class removechapterindex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChapterIndex",
                table: "Chapters");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChapterIndex",
                table: "Chapters",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
