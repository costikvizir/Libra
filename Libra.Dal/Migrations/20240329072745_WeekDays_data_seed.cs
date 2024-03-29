using Microsoft.EntityFrameworkCore.Migrations;

namespace Libra.Dal.Migrations
{
    public partial class WeekDays_data_seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // This migration should be revised

			migrationBuilder.DropForeignKey(
                name: "FK_PosWeekDay_WeekDay_WeekDayId",
                table: "PosWeekDay");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PosWeekDay",
                table: "PosWeekDay");

            migrationBuilder.DropIndex(
                name: "IX_PosWeekDay_PosId",
                table: "PosWeekDay");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WeekDay",
                table: "WeekDays");

            migrationBuilder.DropColumn(
                name: "DaysClosed",
                table: "Pos");

            //migrationBuilder.RenameTable(
            //    name: "WeekDays",
            //    newName: "WeekDays");

			// Drop the column
			migrationBuilder.DropColumn(
				name: "Id",
				table: "PosWeekDay");

			// Recreate the column without the identity property
			migrationBuilder.AddColumn<int>(
				name: "Id",
				table: "PosWeekDay",
				nullable: false);

			migrationBuilder.AddPrimaryKey(
	            name: "PK_PosWeekDay",
	            table: "PosWeekDay",
	            column: "Id");

			//migrationBuilder.AlterColumn<int>(
			//             name: "Id",
			//             table: "PosWeekDay",
			//             nullable: false,
			//             oldClrType: typeof(int),
			//             oldType: "int")
			//             .OldAnnotation("SqlServer:Identity", "1, 1");

			//migrationBuilder.AddPrimaryKey(
			//    name: "PK_PosWeekDay",
			//    table: "PosWeekDay",
			//    columns: new[] { "PosId", "WeekDayId" });

			migrationBuilder.CreateIndex(
	            name: "IX_PosWeekDay_PosId_WeekDayId",  // The name of the index
	            table: "PosWeekDay",   // The table to which the index should be added
	            columns: new[] { "PosId", "WeekDayId" },  // The columns that make up the index
	            unique: true  // Specifies that the combination of PosId and WeekDayId must be unique
);

			// Drop the column
			migrationBuilder.DropColumn(
				name: "Id",
				table: "WeekDays");

			// Recreate the column without the identity property
			migrationBuilder.AddColumn<int>(
				name: "Id",
				table: "WeekDays",
				nullable: false);

			migrationBuilder.AddPrimaryKey(
                name: "PK_WeekDays",
                table: "WeekDays",
                column: "Id");

            migrationBuilder.InsertData(
                table: "WeekDays",
                columns: new[] { "Id", "Day" },
                values: new object[,]
                {
                    { 1, "Mon" },
                    { 2, "Tue" },
                    { 3, "Wed" },
                    { 4, "Thu" },
                    { 5, "Fri" },
                    { 6, "Sat" },
                    { 7, "Sun" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_PosWeekDay_WeekDays_WeekDayId",
                table: "PosWeekDay",
                column: "WeekDayId",
                principalTable: "WeekDays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PosWeekDay_WeekDays_WeekDayId",
                table: "PosWeekDay");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PosWeekDay",
                table: "PosWeekDay");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WeekDays",
                table: "WeekDays");

            migrationBuilder.DeleteData(
                table: "WeekDays",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "WeekDays",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "WeekDays",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "WeekDays",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "WeekDays",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "WeekDays",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "WeekDays",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.RenameTable(
                name: "WeekDays",
                newName: "WeekDay");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "PosWeekDay",
                type: "int",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "DaysClosed",
                table: "Pos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PosWeekDay",
                table: "PosWeekDay",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WeekDay",
                table: "WeekDay",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PosWeekDay_PosId",
                table: "PosWeekDay",
                column: "PosId");

            migrationBuilder.AddForeignKey(
                name: "FK_PosWeekDay_WeekDay_WeekDayId",
                table: "PosWeekDay",
                column: "WeekDayId",
                principalTable: "WeekDay",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
