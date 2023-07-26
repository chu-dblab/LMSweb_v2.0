using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMSweb.Migrations
{
    /// <inheritdoc />
    public partial class AddNameToStudentAndTeacher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TeacherName",
                table: "Teachers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StudentName",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "StudentID",
                keyValue: "S001",
                column: "StudentName",
                value: "林世楷");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "StudentID",
                keyValue: "S002",
                column: "StudentName",
                value: "李國禎");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "StudentID",
                keyValue: "S003",
                column: "StudentName",
                value: "許盈琪");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "StudentID",
                keyValue: "S004",
                column: "StudentName",
                value: "Kevin");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "StudentID",
                keyValue: "S005",
                column: "StudentName",
                value: "Vivian");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "StudentID",
                keyValue: "S006",
                column: "StudentName",
                value: "Amy");

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "TeacherID",
                keyValue: "T001",
                column: "TeacherName",
                value: "Lee");

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "TeacherID",
                keyValue: "T002",
                column: "TeacherName",
                value: "曾老師");

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "TeacherID",
                keyValue: "T003",
                column: "TeacherName",
                value: "李偉老師");

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "TeacherID",
                keyValue: "T004",
                column: "TeacherName",
                value: "焰超老師");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TeacherName",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "StudentName",
                table: "Students");
        }
    }
}
