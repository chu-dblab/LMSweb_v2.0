using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMSweb.Migrations
{
    /// <inheritdoc />
    public partial class AlterForeignKeyConstrainAndAddNewUserData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CourseID",
                table: "Groups",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "ID", "Gender", "Name", "RoleName", "UPassword" },
                values: new object[] { "T005", null, "俊興老師", "Teacher", "369be97f36a54ac72f4e7e3a69ca6860b6bf17d148b0686d0ff9e18a1bd32249" });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "TeacherID", "TeacherName" },
                values: new object[] { "T005", "俊興老師" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "TeacherID",
                keyValue: "T005");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "ID",
                keyValue: "T005");

            migrationBuilder.AlterColumn<string>(
                name: "CourseID",
                table: "Groups",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);
        }
    }
}
