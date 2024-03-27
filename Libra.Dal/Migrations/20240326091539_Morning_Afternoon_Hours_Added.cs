using Microsoft.EntityFrameworkCore.Migrations;

namespace Libra.Dal.Migrations
{
    public partial class Morning_Afternoon_Hours_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AfternoonClosing",
                table: "Pos",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AfternoonOpening",
                table: "Pos",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MorningClosing",
                table: "Pos",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MorningOpening",
                table: "Pos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AfternoonClosing",
                table: "Pos");

            migrationBuilder.DropColumn(
                name: "AfternoonOpening",
                table: "Pos");

            migrationBuilder.DropColumn(
                name: "MorningClosing",
                table: "Pos");

            migrationBuilder.DropColumn(
                name: "MorningOpening",
                table: "Pos");
        }
    }
}
