using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMSweb.Migrations
{
    /// <inheritdoc />
    public partial class ChangeT005PWD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: "T005",
                column: "UPassword",
                value: "7486dac1e0973fbf19799c2bbcedc7ef676eac0aee859908cedf073617cc6a63");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: "T005",
                column: "UPassword",
                value: "369be97f36a54ac72f4e7e3a69ca6860b6bf17d148b0686d0ff9e18a1bd32249");
        }
    }
}
