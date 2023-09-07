using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DDAC_System.Migrations.System_ClassDB
{
    public partial class finalModelSubmission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "SubmitTime",
                table: "AssignmentSubmission",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubmitTime",
                table: "AssignmentSubmission");
        }
    }
}
