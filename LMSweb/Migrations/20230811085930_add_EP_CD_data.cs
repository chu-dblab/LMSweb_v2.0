using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LMSweb.Migrations
{
    /// <inheritdoc />
    public partial class add_EP_CD_data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ExperimentalProcedures",
                columns: new[] { "EProcedureID", "Name" },
                values: new object[,]
                {
                    { "C", "寫程式碼" },
                    { "D", "畫流程圖" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ExperimentalProcedures",
                keyColumn: "EProcedureID",
                keyValue: "C");

            migrationBuilder.DeleteData(
                table: "ExperimentalProcedures",
                keyColumn: "EProcedureID",
                keyValue: "D");
        }
    }
}
