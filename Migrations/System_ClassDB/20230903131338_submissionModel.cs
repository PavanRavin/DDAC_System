using Microsoft.EntityFrameworkCore.Migrations;

namespace DDAC_System.Migrations.System_ClassDB
{
    public partial class submissionModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssignmentSubmission",
                columns: table => new
                {
                    SubmitID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubmitName = table.Column<string>(nullable: true),
                    AssignmentID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentSubmission", x => x.SubmitID);
                    table.ForeignKey(
                        name: "FK_AssignmentSubmission_Assignment_AssignmentID",
                        column: x => x.AssignmentID,
                        principalTable: "Assignment",
                        principalColumn: "AssignmentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentSubmission_AssignmentID",
                table: "AssignmentSubmission",
                column: "AssignmentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssignmentSubmission");
        }
    }
}
