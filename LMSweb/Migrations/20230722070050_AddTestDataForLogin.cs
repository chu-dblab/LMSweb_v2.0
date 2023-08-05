using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LMSweb.Migrations
{
    /// <inheritdoc />
    public partial class AddTestDataForLogin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                table: "Students",
                columns: new[] { "StudentId", "CourseId", "GroupID", "isLeader" },
                values: new object[,]
                {
                    { "S001", null, null, false },
                    { "S002", null, null, false },
                    { "S003", null, null, false },
                    { "S004", null, null, false },
                    { "S005", null, null, false },
                    { "S006", null, null, false }
                });

            migrationBuilder.InsertData(
                table: "Teachers",
                column: "TeacherID",
                values: new object[]
                {
                    "T001",
                    "T002",
                    "T003",
                    "T004"
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
