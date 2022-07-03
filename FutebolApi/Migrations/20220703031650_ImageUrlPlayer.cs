using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FutebolApi.Migrations
{
    public partial class ImageUrlPlayer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Players",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Players");
        }
    }
}
