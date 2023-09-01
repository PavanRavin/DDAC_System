using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DDAC_System.Migrations.System_ClassDB
{
    public partial class ViewClassSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ViewClas",
                columns: table => new
                {
                    ViewClass_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ViewClass_Name = table.Column<string>(nullable: true),
                    ViewClass_Lecturer = table.Column<string>(nullable: true),
                    ViewClassStartTime = table.Column<DateTime>(nullable: false),
                    ViewClassEndTime = table.Column<DateTime>(nullable: false),
                    ViewClass_Location = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViewClas", x => x.ViewClass_ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ViewClas");
        }
    }
}
