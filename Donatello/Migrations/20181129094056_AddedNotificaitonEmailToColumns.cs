using Microsoft.EntityFrameworkCore.Migrations;

namespace Donatello.Migrations
{
    public partial class AddedNotificaitonEmailToColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NotificationEmail",
                table: "Column",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotificationEmail",
                table: "Column");
        }
    }
}
