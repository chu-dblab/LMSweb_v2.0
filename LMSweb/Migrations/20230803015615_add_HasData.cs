using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LMSweb.Migrations
{
    /// <inheritdoc />
    public partial class add_HasData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ExperimentalProcedures",
                columns: new[] { "EProcedureID", "Name" },
                values: new object[,]
                {
                    { "0", "目標設置" },
                    { "1", "任務監控" },
                    { "2", "自我反思" },
                    { "3", "目標設置" },
                    { "4", "任務監控" },
                    { "5", "團隊反思" },
                    { "6", "同儕互評" },
                    { "7", "同儕回饋" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "ID", "Gender", "Name", "RoleName", "UPassword" },
                values: new object[,]
                {
                    { "S001", "男", "林世楷", "Student", "c5e0e70cb9001fc326a9d5b3c39c1d3b48919bf3adacc633729bfff7c27f1d26" },
                    { "S002", "男", "李國禎", "Student", "c842f8af9e82946217a5f35c046c0470ce855b145a093448295d758810c68303" },
                    { "S003", "女", "許盈琪", "Student", "f598aee2eda0ebd461a60eb24ecc6378674be0d06a591ef8f25b201e4f619e48" },
                    { "S004", "男", "Kevin", "Student", "db3ea858db39f2f6eafd7ad39f7798428d9e6244a430919f84c7dc8b905081ad" },
                    { "S005", "女", "Vivian", "Student", "09fd191dc08a0375f4f10fd8ce970d8193a0b475bb3d75db4b8221e8f0d74979" },
                    { "S006", "女", "Amy", "Student", "19a85017e5a5057f9cb3104e7afde89aea6c4d74f544ba5eaeaab256bcf937af" },
                    { "T001", null, "Lee", "Teacher", "15152d459354c17470fbeba5c03aa9b0790b237b04f190aba04b2a3d1afe64bf" },
                    { "T002", null, "曾老師", "Teacher", "9ba7d0652682e1fe75b90bd1ea8a1a69e679a0039c80fc9c85e10e2ff7ddc793" },
                    { "T003", null, "李偉老師", "Teacher", "963606fbc3791a6c3053264f977ce910821a69680e5e41de99e6b3f04d7d0471" },
                    { "T004", null, "焰超老師", "Teacher", "efcc4b59f003f0a56d6869d748cf3d31c3a4d36a6a8b9bbe60cb2171294b896c" }
                });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "QuestionID", "CourseId", "EProcedureID", "QContent", "QType" },
                values: new object[,]
                {
                    { "CM01A", null, "1", "我認為這份流程圖是正確的嗎？", "0" },
                    { "CM01B", null, "4", "我們認為這份流程圖是正確的嗎？", "0" },
                    { "PC01", null, "7", "該組評價合理嗎?", "0" },
                    { "PE01", null, "6", "我們覺得這一組的邏輯性可以獲得幾分?", "0" },
                    { "PE02", null, "6", "我們覺得這一組的正確性可以獲得幾分?", "0" },
                    { "PE03", null, "6", "我們覺得這一組的可讀性可以獲得幾分?", "0" },
                    { "PGS01A", null, "0", "我期望這次小組任務可以獲得幾分?", "0" },
                    { "PGS01B", null, "3", "我們期望這次小組任務可以獲得幾分?", "0" },
                    { "PGS02A", null, "0", "為達成目標，我將採用以下哪些學習方法? (可複選)", "1" },
                    { "PGS02B", null, "3", "為達成目標，我們小組將採用以下哪些學習方法? (可複選)", "1" },
                    { "PGS03A", null, "0", "在合作學習中，我會積極參與哪些合作任務? (可複選)", "1" },
                    { "PGS03B", null, "3", "在合作學習中，我們小組會積極參與哪些合作任務? (可複選)", "1" },
                    { "SR01A", null, "2", "我覺得這次小組任務可以獲得幾分?", "0" },
                    { "SR01B", null, "5", "我們覺得這次小組任務可以獲得幾分?", "0" },
                    { "SR02A", null, "2", "在本次學習任務中，我採用了以下哪些學習方法? (可複選)", "1" },
                    { "SR02B", null, "5", "在本次學習任務中，我們小組採用了以下哪些學習方法? (可複選)", "1" },
                    { "SR03A", null, "2", "在本次學習任務中，我積極參與了以下哪些合作任務? (可複選)", "1" },
                    { "SR03B", null, "5", "在本次學習任務中，我們小組積極參與了以下哪些合作任務? (可複選)", "1" },
                    { "SR04A", null, "2", "依據上述反思，我覺得下個任務可以如何改進?", "2" },
                    { "SR04B", null, "5", "依據上述反思，我們小組覺得下個任務可以如何改進?", "2" },
                    { "TE01A", null, "2", "我們有沒有依照老師指示完成任務？", "0" },
                    { "TE01B", null, "5", "我們有沒有依照老師指示完成任務？", "0" },
                    { "TM01A", null, "1", "我認為這次任務需要多長時間完成程式碼的撰寫？", "0" },
                    { "TM01B", null, "4", "我們認為這次任務需要多長時間完成程式碼的撰寫？", "0" },
                    { "TR01B", null, "2", "我們期望我們小組在這次任務可以得到幾分？", "0" },
                    { "TR02B", null, "2", "為達成目標，我們將採用以下那些學習方法？", "1" },
                    { "TR03B", null, "2", "在合作學習中，我們希望能積極參與以下合作任務？", "1" },
                    { "TR04B", null, "2", "依據上述反思，我們覺得下個任務可以如何改進？", "2" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "StudentId", "CourseId", "GroupID", "isLeader", "StudentName" },
                values: new object[,]
                {
                    { "S001", null, null, false, "林世楷" },
                    { "S002", null, null, false, "李國禎" },
                    { "S003", null, null, false, "許盈琪" },
                    { "S004", null, null, false, "Kevin" },
                    { "S005", null, null, false, "Vivian" },
                    { "S006", null, null, false, "Amy" }
                });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "TeacherID", "TeacherName" },
                values: new object[,]
                {
                    { "T001", "Lee" },
                    { "T002", "曾老師" },
                    { "T003", "李偉老師" },
                    { "T004", "焰超老師" }
                });

            migrationBuilder.InsertData(
                table: "Options",
                columns: new[] { "OptionID", "OContent", "QuestionID" },
                values: new object[,]
                {
                    { 1, "90分以上", "PGS01A" },
                    { 2, "80~90分", "PGS01A" },
                    { 3, "70~80分", "PGS01A" },
                    { 4, "70分以下", "PGS01A" },
                    { 5, "小組成員一起合作 ", "PGS02A" },
                    { 6, "上網找資料", "PGS02A" },
                    { 7, "參考其他小組的解決方案 ", "PGS02A" },
                    { 8, "查看教材", "PGS02A" },
                    { 9, "請教老師或同學", "PGS02A" },
                    { 10, "小組成員一起討論", "PGS03A" },
                    { 11, "合作規劃流程圖", "PGS03A" },
                    { 12, "合作撰寫程式碼", "PGS03A" },
                    { 13, "10分鐘以內", "TM01A" },
                    { 14, "10-15分鐘", "TM01A" },
                    { 15, "15分鐘以上", "TM01A" },
                    { 16, "正確", "CM01A" },
                    { 17, "不完全正確", "CM01A" },
                    { 18, "不正確", "CM01A" },
                    { 19, "有", "TE01A" },
                    { 20, "沒有", "TE01A" },
                    { 21, "90分以上", "SR01A" },
                    { 22, "80~90分", "SR01A" },
                    { 23, "70~80分", "SR01A" },
                    { 24, "60~70分", "SR01A" },
                    { 25, "60分以下", "SR01A" },
                    { 26, "小組成員一起合作 ", "SR02A" },
                    { 27, "上網找資料", "SR02A" },
                    { 28, "參考其他小組的解決方案", "SR02A" },
                    { 29, "查看教材", "SR02A" },
                    { 30, "請教老師或同學", "SR02A" },
                    { 31, "小組成員一起討論", "SR03A" },
                    { 32, "合作規劃流程圖", "SR03A" },
                    { 33, "合作撰寫程式碼", "SR03A" },
                    { 34, "90分以上", "PGS01B" },
                    { 35, "80~90分", "PGS01B" },
                    { 36, "70~80分", "PGS01B" },
                    { 37, "70分以下", "PGS01B" },
                    { 38, "小組成員一起合作 ", "PGS02B" },
                    { 39, "上網找資料", "PGS02B" },
                    { 40, "參考其他小組的解決方案 ", "PGS02B" },
                    { 41, "查看教材", "PGS02B" },
                    { 42, "請教老師或同學", "PGS02B" },
                    { 43, "小組成員一起討論", "PGS03B" },
                    { 44, "合作規劃流程圖", "PGS03B" },
                    { 45, "合作撰寫程式碼", "PGS03B" },
                    { 46, "10分鐘以內", "TM01B" },
                    { 47, "10-15分鐘", "TM01B" },
                    { 48, "15分鐘以上", "TM01B" },
                    { 49, "正確", "CM01B" },
                    { 50, "不完全正確", "CM01B" },
                    { 51, "不正確", "CM01B" },
                    { 52, "有", "TE01B" },
                    { 53, "沒有", "TE01B" },
                    { 54, "90分以上", "TR01B" },
                    { 55, "80~90分", "TR01B" },
                    { 56, "70~80分", "TR01B" },
                    { 57, "60~70分", "TR01B" },
                    { 58, "60分以下", "TR01B" },
                    { 59, "小組成員一起合作 ", "TR02B" },
                    { 60, "上網找資料", "TR02B" },
                    { 61, "參考其他小組的解決方案 ", "TR02B" },
                    { 62, "查看教材", "TR02B" },
                    { 63, "請教老師或同學", "TR02B" },
                    { 64, "小組成員一起討論", "TR03B" },
                    { 65, "合作規劃流程圖", "TR03B" },
                    { 66, "合作撰寫程式碼", "TR03B" },
                    { 67, "3", "PE01" },
                    { 68, "2", "PE01" },
                    { 69, "1", "PE01" },
                    { 70, "3", "PE02" },
                    { 71, "2", "PE02" },
                    { 72, "1", "PE02" },
                    { 73, "3", "PE03" },
                    { 74, "2", "PE03" },
                    { 75, "1", "PE03" },
                    { 76, "非常合理", "PC01" },
                    { 77, "合理", "PC01" },
                    { 78, "不合理", "PC01" },
                    { 79, "非常不合理", "PC01" }
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
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionID",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: "SR01B");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: "SR02B");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: "SR03B");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: "SR04A");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: "SR04B");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: "TR04B");

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: "S001");

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: "S002");

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: "S003");

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: "S004");

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: "S005");

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: "S006");

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "TeacherID",
                keyValue: "T001");

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "TeacherID",
                keyValue: "T002");

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "TeacherID",
                keyValue: "T003");

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "TeacherID",
                keyValue: "T004");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: "CM01A");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: "CM01B");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: "PC01");

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
                keyValue: "PGS01A");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: "PGS01B");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: "PGS02A");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: "PGS02B");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: "PGS03A");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: "PGS03B");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: "SR01A");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: "SR02A");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: "SR03A");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: "TE01A");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: "TE01B");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: "TM01A");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: "TM01B");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: "TR01B");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: "TR02B");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: "TR03B");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "ID",
                keyValue: "S001");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "ID",
                keyValue: "S002");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "ID",
                keyValue: "S003");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "ID",
                keyValue: "S004");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "ID",
                keyValue: "S005");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "ID",
                keyValue: "S006");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "ID",
                keyValue: "T001");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "ID",
                keyValue: "T002");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "ID",
                keyValue: "T003");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "ID",
                keyValue: "T004");

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

            migrationBuilder.DeleteData(
                table: "ExperimentalProcedures",
                keyColumn: "EProcedureID",
                keyValue: "7");
        }
    }
}
