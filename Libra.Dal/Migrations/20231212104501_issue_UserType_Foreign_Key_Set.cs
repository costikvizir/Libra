using Microsoft.EntityFrameworkCore.Migrations;

namespace Libra.Dal.Migrations
{
    public partial class issue_UserType_Foreign_Key_Set : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_userTypes_UserTypeId",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_userTypes_UserTypeId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_userTypes",
                table: "userTypes");

            migrationBuilder.DropIndex(
                name: "IX_Issues_UserTypeId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "UserTypeId",
                table: "Issues");

            migrationBuilder.RenameTable(
                name: "userTypes",
                newName: "UserTypes");

            migrationBuilder.AlterColumn<int>(
                name: "AssignedId",
                table: "Issues",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTypes",
                table: "UserTypes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_AssignedId",
                table: "Issues",
                column: "AssignedId");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_UserTypes_AssignedId",
                table: "Issues",
                column: "AssignedId",
                principalTable: "UserTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserTypes_UserTypeId",
                table: "Users",
                column: "UserTypeId",
                principalTable: "UserTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_UserTypes_AssignedId",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserTypes_UserTypeId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTypes",
                table: "UserTypes");

            migrationBuilder.DropIndex(
                name: "IX_Issues_AssignedId",
                table: "Issues");

            migrationBuilder.RenameTable(
                name: "UserTypes",
                newName: "userTypes");

            migrationBuilder.AlterColumn<int>(
                name: "AssignedId",
                table: "Issues",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "UserTypeId",
                table: "Issues",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_userTypes",
                table: "userTypes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_UserTypeId",
                table: "Issues",
                column: "UserTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_userTypes_UserTypeId",
                table: "Issues",
                column: "UserTypeId",
                principalTable: "userTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_userTypes_UserTypeId",
                table: "Users",
                column: "UserTypeId",
                principalTable: "userTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
