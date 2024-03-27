using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Libra.Dal.Migrations
{
    public partial class removed_fields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "ConnectionType",
                columns: new[] { "Id", "ConnectType" },
                values: new object[] { 1, "Remote" });

            migrationBuilder.InsertData(
                table: "ConnectionType",
                columns: new[] { "Id", "ConnectType" },
                values: new object[] { 2, "Physical" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ConnectionType",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ConnectionType",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AddColumn<DateTime>(
                name: "AfternoonClosing",
                table: "Pos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "AfternoonOpening",
                table: "Pos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "MorningClosing",
                table: "Pos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "MorningOpening",
                table: "Pos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
