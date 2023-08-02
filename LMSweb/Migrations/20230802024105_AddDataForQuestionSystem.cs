using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LMSweb.Migrations
{
    /// <inheritdoc />
    public partial class AddDataForQuestionSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CourseID",
                table: "Questions",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.InsertData(
                table: "ExperimentalProcedures",
                columns: new[] { "EProcedureID", "Name" },
                values: new object[,]
                {
                    { "0", "目標設置" },
                    { "1", "任務監控" },
                    { "2", "內容監控" },
                    { "3", "任務評估" },
                    { "4", "自我反思" },
                    { "5", "團隊反思" },
                    { "6", "同儕互評" }
                });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "QuestionID", "CourseID", "EProcedureID", "QContent", "QType" },
                values: new object[,]
                {
                    { "CM01", null, "2", "我們認為這份流程圖是正確的嗎？", "0" },
                    { "PE01", null, "6", "我們覺得這一組的邏輯性可以獲得幾分?", "0" },
                    { "PE02", null, "6", "我們覺得這一組的正確性可以獲得幾分?", "0" },
                    { "PE03", null, "6", "我們覺得這一組的可讀性可以獲得幾分?", "0" },
                    { "PGS01", null, "0", "我期望我們小組在這次任務可以得到幾分？", "0" },
                    { "PGS02", null, "0", "為達成目標，我將採用以下那些學習方法？", "1" },
                    { "PGS03", null, "0", "在合作學習中，我希望能積極參與以下合作任務？", "1" },
                    { "SR01", null, "4", "我期望我們小組在這次任務可以得到幾分？", "0" },
                    { "SR02", null, "4", "為達成目標，我將採用以下那些學習方法？", "1" },
                    { "SR03", null, "4", "在合作學習中，我希望能積極參與以下合作任務？", "1" },
                    { "SR04", null, "4", "依據上述反思，我覺得下個任務可以如何改進？", "2" },
                    { "TE01", null, "3", "我們有沒有依照老師指示完成任務？", "0" },
                    { "TM01", null, "1", "我們認為需要多長時間完成程式碼的撰寫？", "0" },
                    { "TR01", null, "5", "我們期望我們小組在這次任務可以得到幾分？", "0" },
                    { "TR02", null, "5", "為達成目標，我們將採用以下那些學習方法？", "1" },
                    { "TR03", null, "5", "在合作學習中，我們希望能積極參與以下合作任務？", "1" },
                    { "TR04", null, "5", "依據上述反思，我們覺得下個任務可以如何改進？", "2" }
                });

            migrationBuilder.InsertData(
                table: "Options",
                columns: new[] { "OptionID", "OContent", "QuestionID" },
                values: new object[,]
                {
                    { 1, "90~100", "PGS01" },
                    { 2, "80~90", "PGS01" },
                    { 3, "70~80", "PGS01" },
                    { 4, "60~70", "PGS01" },
                    { 5, "查找教材", "PGS02" },
                    { 6, "上網找資料", "PGS02" },
                    { 7, "與同學討論與合作", "PGS02" },
                    { 8, "詢問老師", "PGS02" },
                    { 9, "參與討論", "PGS03" },
                    { 10, "規劃流程圖", "PGS03" },
                    { 11, "撰寫程式碼", "PGS03" },
                    { 12, "10分鐘以內", "TM01" },
                    { 13, "10-15分鐘", "TM01" },
                    { 14, "15分鐘以上", "TM01" },
                    { 15, "正確", "CM01" },
                    { 16, "不完全正確", "CM01" },
                    { 17, "不正確", "CM01" },
                    { 18, "有", "TE01" },
                    { 19, "沒有", "TE01" },
                    { 20, "90~100", "SR01" },
                    { 21, "80~90", "SR01" },
                    { 22, "70~80", "SR01" },
                    { 23, "60~70", "SR01" },
                    { 24, "查找教材", "SR02" },
                    { 25, "上網找資料", "SR02" },
                    { 26, "與同學討論與合作", "SR02" },
                    { 27, "詢問老師", "SR02" },
                    { 28, "參與討論", "SR03" },
                    { 29, "規劃流程圖", "SR03" },
                    { 30, "撰寫程式碼", "SR03" },
                    { 31, "90~100", "TR01" },
                    { 32, "80~90", "TR01" },
                    { 33, "70~80", "TR01" },
                    { 34, "60~70", "TR01" },
                    { 35, "查找教材", "TR02" },
                    { 36, "上網找資料", "TR02" },
                    { 37, "與同學討論與合作", "TR02" },
                    { 38, "詢問老師", "TR02" },
                    { 39, "參與討論", "TR03" },
                    { 40, "規劃流程圖", "TR03" },
                    { 41, "撰寫程式碼", "TR03" },
                    { 42, "3", "PE01" },
                    { 43, "2", "PE01" },
                    { 44, "1", "PE01" },
                    { 45, "3", "PE02" },
                    { 46, "2", "PE02" },
                    { 47, "1", "PE02" },
                    { 48, "3", "PE03" },
                    { 49, "2", "PE03" },
                    { 50, "1", "PE03" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: "SR04");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: "TR04");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: "CM01");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: "PE01");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: "PE02");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: "PE03");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: "PGS01");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: "PGS02");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: "PGS03");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: "SR01");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: "SR02");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: "SR03");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: "TE01");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: "TM01");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: "TR01");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: "TR02");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: "TR03");

            migrationBuilder.DeleteData(
                table: "ExperimentalProcedures",
                keyColumn: "EProcedureID",
                keyValue: "0");

            migrationBuilder.DeleteData(
                table: "ExperimentalProcedures",
                keyColumn: "EProcedureID",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "ExperimentalProcedures",
                keyColumn: "EProcedureID",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "ExperimentalProcedures",
                keyColumn: "EProcedureID",
                keyValue: "3");

            migrationBuilder.DeleteData(
                table: "ExperimentalProcedures",
                keyColumn: "EProcedureID",
                keyValue: "4");

            migrationBuilder.DeleteData(
                table: "ExperimentalProcedures",
                keyColumn: "EProcedureID",
                keyValue: "5");

            migrationBuilder.DeleteData(
                table: "ExperimentalProcedures",
                keyColumn: "EProcedureID",
                keyValue: "6");

            migrationBuilder.AlterColumn<string>(
                name: "CourseID",
                table: "Questions",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128,
                oldNullable: true);
        }
    }
}
