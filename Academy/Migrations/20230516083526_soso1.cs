using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Academy.Migrations
{
    /// <inheritdoc />
    public partial class soso1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_students_categories_CategoryId",
                table: "students");

            migrationBuilder.DropIndex(
                name: "IX_students_CategoryId",
                table: "students");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "students");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_students_CategoryId",
                table: "students",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_students_categories_CategoryId",
                table: "students",
                column: "CategoryId",
                principalTable: "categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
