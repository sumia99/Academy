using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Academy.Migrations
{
    /// <inheritdoc />
    public partial class fmig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_aboutUs",
                table: "aboutUs");

            migrationBuilder.RenameTable(
                name: "aboutUs",
                newName: "AboutUs");

            migrationBuilder.AddColumn<int>(
                name: "Page",
                table: "AboutUs",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AboutUs",
                table: "AboutUs",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AboutUs",
                table: "AboutUs");

            migrationBuilder.DropColumn(
                name: "Page",
                table: "AboutUs");

            migrationBuilder.RenameTable(
                name: "AboutUs",
                newName: "aboutUs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_aboutUs",
                table: "aboutUs",
                column: "Id");
        }
    }
}
