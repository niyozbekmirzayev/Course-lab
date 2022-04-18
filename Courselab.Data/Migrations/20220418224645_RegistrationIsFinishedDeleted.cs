using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Courselab.Data.Migrations
{
    public partial class RegistrationIsFinishedDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinishedDate",
                table: "Registrations");

            migrationBuilder.DropColumn(
                name: "IsFinished",
                table: "Registrations");

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "Courses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "YouTubePlayListLink",
                table: "Courses",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "YouTubePlayListLink",
                table: "Courses");

            migrationBuilder.AddColumn<DateTime>(
                name: "FinishedDate",
                table: "Registrations",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFinished",
                table: "Registrations",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
