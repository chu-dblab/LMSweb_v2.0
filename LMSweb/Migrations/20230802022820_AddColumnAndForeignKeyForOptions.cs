using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMSweb.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnAndForeignKeyForOptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OID",
                table: "Options",
                newName: "OptionID");

            migrationBuilder.AddColumn<string>(
                name: "QuestionID",
                table: "Options",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Options",
                table: "Options",
                column: "OptionID");

            migrationBuilder.CreateIndex(
                name: "IX_Options_QuestionID",
                table: "Options",
                column: "QuestionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Options_Questions",
                table: "Options",
                column: "QuestionID",
                principalTable: "Questions",
                principalColumn: "QuestionID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Options_Questions",
                table: "Options");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Options",
                table: "Options");

            migrationBuilder.DropIndex(
                name: "IX_Options_QuestionID",
                table: "Options");

            migrationBuilder.DropColumn(
                name: "QuestionID",
                table: "Options");

            migrationBuilder.RenameColumn(
                name: "OptionID",
                table: "Options",
                newName: "OID");
        }
    }
}
