using Microsoft.EntityFrameworkCore.Migrations;

namespace Donatello.Migrations
{
    public partial class Addingcardnotes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Card",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Card");
        }
    }
}
