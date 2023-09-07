using Microsoft.EntityFrameworkCore.Migrations;

namespace DDAC_System.Migrations.System_ClassDB
{
    public partial class NewAssignmentModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignment_AcademicClass_Class_id",
                table: "Assignment");

            migrationBuilder.DropIndex(
                name: "IX_Assignment_Class_id",
                table: "Assignment");

            migrationBuilder.DropColumn(
                name: "Class_id",
                table: "Assignment");

            migrationBuilder.AddColumn<string>(
                name: "Class_Name",
                table: "Assignment",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Class_Name",
                table: "AcademicClass",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_AcademicClass_Class_Name",
                table: "AcademicClass",
                column: "Class_Name");

            migrationBuilder.CreateIndex(
                name: "IX_Assignment_Class_Name",
                table: "Assignment",
                column: "Class_Name");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignment_AcademicClass_Class_Name",
                table: "Assignment",
                column: "Class_Name",
                principalTable: "AcademicClass",
                principalColumn: "Class_Name",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignment_AcademicClass_Class_Name",
                table: "Assignment");

            migrationBuilder.DropIndex(
                name: "IX_Assignment_Class_Name",
                table: "Assignment");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_AcademicClass_Class_Name",
                table: "AcademicClass");

            migrationBuilder.DropColumn(
                name: "Class_Name",
                table: "Assignment");

            migrationBuilder.AddColumn<int>(
                name: "Class_id",
                table: "Assignment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Class_Name",
                table: "AcademicClass",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_Assignment_Class_id",
                table: "Assignment",
                column: "Class_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignment_AcademicClass_Class_id",
                table: "Assignment",
                column: "Class_id",
                principalTable: "AcademicClass",
                principalColumn: "Class_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
