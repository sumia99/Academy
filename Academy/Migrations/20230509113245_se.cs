using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Academy.Migrations
{
    /// <inheritdoc />
    public partial class se : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "courses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_students_CategoryId",
                table: "students",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_courses_StudentId",
                table: "courses",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_courses_students_StudentId",
                table: "courses",
                column: "StudentId",
                principalTable: "students",
                principalColumn: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_students_categories_CategoryId",
                table: "students",
                column: "CategoryId",
                principalTable: "categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_courses_students_StudentId",
                table: "courses");

            migrationBuilder.DropForeignKey(
                name: "FK_students_categories_CategoryId",
                table: "students");

            migrationBuilder.DropIndex(
                name: "IX_students_CategoryId",
                table: "students");

            migrationBuilder.DropIndex(
                name: "IX_courses_StudentId",
                table: "courses");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "students");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "courses");
        }
    }
}
