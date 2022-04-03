using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Courselab.Data.Migrations
{
    public partial class AuthorsUpdatedAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegistratedCourses");

            migrationBuilder.CreateTable(
                name: "RegistratedCourse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CourseId = table.Column<Guid>(type: "uuid", nullable: false),
                    StartedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsFinished = table.Column<bool>(type: "boolean", nullable: false),
                    FinishedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    StudentId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistratedCourse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistratedCourse_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegistratedCourse_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegistratedCourse_CourseId",
                table: "RegistratedCourse",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistratedCourse_StudentId",
                table: "RegistratedCourse",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegistratedCourse");

            migrationBuilder.CreateTable(
                name: "RegistratedCourses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CourseId = table.Column<Guid>(type: "uuid", nullable: false),
                    FinishedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsFinished = table.Column<bool>(type: "boolean", nullable: false),
                    StartedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    StudentId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistratedCourses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistratedCourses_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegistratedCourses_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegistratedCourses_CourseId",
                table: "RegistratedCourses",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistratedCourses_StudentId",
                table: "RegistratedCourses",
                column: "StudentId");
        }
    }
}
