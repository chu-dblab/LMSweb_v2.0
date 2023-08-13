using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMSweb.Migrations
{
    /// <inheritdoc />
    public partial class AddFKCourseAndGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CourseID",
                table: "Groups",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_CourseID",
                table: "Groups",
                column: "CourseID");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Groups",
                table: "Groups",
                column: "CourseID",
                principalTable: "Courses",
                principalColumn: "CID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Groups",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_CourseID",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "CourseID",
                table: "Groups");
        }
    }
}
