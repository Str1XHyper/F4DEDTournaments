using Microsoft.EntityFrameworkCore.Migrations;

namespace F4DEDTournaments.Migrations
{
    public partial class AddedELo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Elo",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Elo",
                table: "AspNetUsers");
        }
    }
}
