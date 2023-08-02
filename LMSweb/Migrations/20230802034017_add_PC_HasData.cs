using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LMSweb.Migrations
{
    /// <inheritdoc />
    public partial class add_PC_HasData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ExperimentalProcedures",
                columns: new[] { "EProcedureID", "Name" },
                values: new object[] { "7", "同儕回饋" });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "QuestionID", "CourseID", "EProcedureID", "QContent", "QType" },
                values: new object[] { "PC01", null, "7", "該組評價合理嗎?", "0" });

            migrationBuilder.InsertData(
                table: "Options",
                columns: new[] { "OptionID", "OContent", "QuestionID" },
                values: new object[,]
                {
                    { 51, "非常合理", "PC01" },
                    { 52, "合理", "PC01" },
                    { 53, "不合理", "PC01" },
                    { 54, "非常不合理", "PC01" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: "PC01");

            migrationBuilder.DeleteData(
                table: "ExperimentalProcedures",
                keyColumn: "EProcedureID",
                keyValue: "7");
        }
    }
}
