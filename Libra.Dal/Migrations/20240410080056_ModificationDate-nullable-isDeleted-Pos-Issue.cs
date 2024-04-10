using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Libra.Dal.Migrations
{
    public partial class ModificationDatenullableisDeletedPosIssue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "Id",
            //    table: "PosWeekDay");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Pos",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModificationDate",
                table: "Issues",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Issues",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Pos");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Issues");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "PosWeekDay",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModificationDate",
                table: "Issues",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
