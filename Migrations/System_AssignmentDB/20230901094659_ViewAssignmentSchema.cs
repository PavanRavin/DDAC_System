using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DDAC_System.Migrations.System_AssignmentDB
{
    public partial class ViewAssignmentSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ViewAssignment",
                columns: table => new
                {
                    ViewAssignmentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ViewAssignmentName = table.Column<string>(nullable: true),
                    ViewAssignmentDesc = table.Column<string>(nullable: true),
                    ViewAssignmentHandOut = table.Column<DateTime>(nullable: false),
                    ViewAssignmentDue = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViewAssignment", x => x.ViewAssignmentID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ViewAssignment");
        }
    }
}
