using Microsoft.EntityFrameworkCore.Migrations;

namespace Courselab.Data.Migrations
{
    public partial class LocalizationDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameEn",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "NameRu",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "NameUz",
                table: "Courses",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Courses",
                newName: "NameUz");

            migrationBuilder.AddColumn<string>(
                name: "NameEn",
                table: "Courses",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameRu",
                table: "Courses",
                type: "text",
                nullable: true);
        }
    }
}
