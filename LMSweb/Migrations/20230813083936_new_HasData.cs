using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LMSweb.Migrations
{
    /// <inheritdoc />
    public partial class new_HasData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "QuestionID", "CourseId", "EProcedureID", "QContent", "QType" },
                values: new object[,]
                {
                    { "PCA01", null, "7", "我們想稱讚這組給的評語", "2" },
                    { "PCA02", null, "7", "我們想批評這組給的評語", "2" },
                    { "PCC03", null, "7", "我們想指出這組給的評語有錯誤的地方", "2" },
                    { "PCIR08", null, "7", "我們想說跟這組的評語無關的回饋", "2" },
                    { "PCM07", null, "7", "我們想請這組想想看如何改進他們的評語", "2" },
                    { "PEA01", null, "6", "我們想稱讚這組的流程圖或程式碼", "2" },
                    { "PEA02", null, "6", "我們想批評這組的流程圖或程式碼", "2" },
                    { "PEC03", null, "6", "我們想指出這組有錯誤的地方", "2" },
                    { "PEIR08", null, "6", "我們想說跟這組的作品無關的評論", "2" },
                    { "PEM07", null, "6", "我們想請這組想想看如何改進他們的流程圖或程式碼", "2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: "PCA01");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: "PCA02");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: "PCC03");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: "PCIR08");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: "PCM07");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: "PEA01");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: "PEA02");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: "PEC03");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: "PEIR08");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: "PEM07");
        }
    }
}
