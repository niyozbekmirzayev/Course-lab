using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Courselab.Data.Migrations
{
    public partial class RegistratedCoursesUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegistratedCourses_Students_StudentId",
                table: "RegistratedCourses");

            migrationBuilder.DropIndex(
                name: "IX_RegistratedCourses_StudentId",
                table: "RegistratedCourses");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "RegistratedCourses");

            migrationBuilder.AddColumn<Guid>(
                name: "RegistratedCoursesId",
                table: "Students",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_RegistratedCoursesId",
                table: "Students",
                column: "RegistratedCoursesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_RegistratedCourses_RegistratedCoursesId",
                table: "Students",
                column: "RegistratedCoursesId",
                principalTable: "RegistratedCourses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_RegistratedCourses_RegistratedCoursesId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_RegistratedCoursesId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "RegistratedCoursesId",
                table: "Students");

            migrationBuilder.AddColumn<Guid>(
                name: "StudentId",
                table: "RegistratedCourses",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_RegistratedCourses_StudentId",
                table: "RegistratedCourses",
                column: "StudentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RegistratedCourses_Students_StudentId",
                table: "RegistratedCourses",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
