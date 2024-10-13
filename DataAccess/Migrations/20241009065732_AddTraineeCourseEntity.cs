using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddTraineeCourseEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Results_Courses_CourseId",
                table: "Results");

            migrationBuilder.DropForeignKey(
                name: "FK_Results_Trainees_TraineeId",
                table: "Results");

            migrationBuilder.DropIndex(
                name: "IX_Results_CourseId",
                table: "Results");

            migrationBuilder.DropIndex(
                name: "IX_Results_TraineeId",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "TraineeId",
                table: "Results");

            migrationBuilder.CreateTable(
                name: "TraineeCourses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TraineeId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TraineeCourses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TraineeCourses_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TraineeCourses_Trainees_TraineeId",
                        column: x => x.TraineeId,
                        principalTable: "Trainees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TraineeCourses_CourseId",
                table: "TraineeCourses",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_TraineeCourses_TraineeId",
                table: "TraineeCourses",
                column: "TraineeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TraineeCourses");

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "Results",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TraineeId",
                table: "Results",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Results_CourseId",
                table: "Results",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_TraineeId",
                table: "Results",
                column: "TraineeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Courses_CourseId",
                table: "Results",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Trainees_TraineeId",
                table: "Results",
                column: "TraineeId",
                principalTable: "Trainees",
                principalColumn: "Id");
        }
    }
}
