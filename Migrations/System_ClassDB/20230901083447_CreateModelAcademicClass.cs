using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DDAC_System.Migrations.System_ClassDB
{
    public partial class CreateModelAcademicClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AcademicClass",
                columns: table => new
                {
                    Class_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Class_Name = table.Column<string>(nullable: true),
                    Class_Lecturer = table.Column<string>(nullable: true),
                    ClassStartTime = table.Column<DateTime>(nullable: false),
                    ClassEndTime = table.Column<DateTime>(nullable: false),
                    Class_Location = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicClass", x => x.Class_ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AcademicClass");
        }
    }
}
