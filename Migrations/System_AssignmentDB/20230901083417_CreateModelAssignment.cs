using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DDAC_System.Migrations.System_AssignmentDB
{
    public partial class CreateModelAssignment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assignment",
                columns: table => new
                {
                    AssignmentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssignmentName = table.Column<string>(nullable: true),
                    AssignmentDesc = table.Column<string>(nullable: true),
                    AssignmentHandOut = table.Column<DateTime>(nullable: false),
                    AssignmentDue = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignment", x => x.AssignmentID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assignment");
        }
    }
}
