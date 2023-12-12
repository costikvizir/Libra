using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Libra.Dal.Migrations
{
    public partial class First_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConnectionType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConnectType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConnectionType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IssueTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IssueLevel = table.Column<int>(nullable: false),
                    ParrentIssue = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    InsertDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IssueStatus = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "userTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Telephone = table.Column<string>(nullable: true),
                    Cellphone = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    CityId = table.Column<int>(nullable: false),
                    Model = table.Column<string>(nullable: true),
                    Brand = table.Column<string>(nullable: true),
                    ConnectionTypeId = table.Column<int>(nullable: false),
                    MorningOpening = table.Column<DateTime>(nullable: false),
                    MorningClosing = table.Column<DateTime>(nullable: false),
                    AfternoonOpening = table.Column<DateTime>(nullable: false),
                    AfternoonClosing = table.Column<DateTime>(nullable: false),
                    DaysClosed = table.Column<string>(nullable: true),
                    InsertDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pos_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pos_ConnectionType_ConnectionTypeId",
                        column: x => x.ConnectionTypeId,
                        principalTable: "ConnectionType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Login = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Telephone = table.Column<string>(nullable: true),
                    UserTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_userTypes_UserTypeId",
                        column: x => x.UserTypeId,
                        principalTable: "userTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Issues",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PosId = table.Column<int>(nullable: false),
                    TypeId = table.Column<int>(nullable: false),
                    SubTypeId = table.Column<int>(nullable: false),
                    ProblemId = table.Column<int>(nullable: false),
                    Priority = table.Column<string>(nullable: true),
                    StatusId = table.Column<int>(nullable: false),
                    Memo = table.Column<string>(nullable: true),
                    UserCreatedId = table.Column<int>(nullable: false),
                    AssignedId = table.Column<int>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    AssignedDate = table.Column<DateTime>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModificationDate = table.Column<DateTime>(nullable: false),
                    Solution = table.Column<string>(nullable: true),
                    UserTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Issues_Pos_PosId",
                        column: x => x.PosId,
                        principalTable: "Pos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Issues_IssueTypes_ProblemId",
                        column: x => x.ProblemId,
                        principalTable: "IssueTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Issues_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Issues_IssueTypes_SubTypeId",
                        column: x => x.SubTypeId,
                        principalTable: "IssueTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Issues_IssueTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "IssueTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Issues_Users_UserCreatedId",
                        column: x => x.UserCreatedId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Issues_userTypes_UserTypeId",
                        column: x => x.UserTypeId,
                        principalTable: "userTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IssueId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    Action = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    InsertDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Logs_Issues_IssueId",
                        column: x => x.IssueId,
                        principalTable: "Issues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Logs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Issues_PosId",
                table: "Issues",
                column: "PosId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_ProblemId",
                table: "Issues",
                column: "ProblemId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_StatusId",
                table: "Issues",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_SubTypeId",
                table: "Issues",
                column: "SubTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_TypeId",
                table: "Issues",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_UserCreatedId",
                table: "Issues",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_UserTypeId",
                table: "Issues",
                column: "UserTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_IssueId",
                table: "Logs",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_UserId",
                table: "Logs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Pos_CityId",
                table: "Pos",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Pos_ConnectionTypeId",
                table: "Pos",
                column: "ConnectionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserTypeId",
                table: "Users",
                column: "UserTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "Issues");

            migrationBuilder.DropTable(
                name: "Pos");

            migrationBuilder.DropTable(
                name: "IssueTypes");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "ConnectionType");

            migrationBuilder.DropTable(
                name: "userTypes");
        }
    }
}
