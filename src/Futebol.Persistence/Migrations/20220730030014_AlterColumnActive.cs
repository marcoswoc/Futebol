using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Futebol.Persistence.Migrations
{
    public partial class AlterColumnActive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "bit",
                table: "Rounds",
                newName: "Active");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Active",
                table: "Rounds",
                newName: "bit");
        }
    }
}
