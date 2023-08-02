using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMSweb.Migrations
{
    /// <inheritdoc />
    public partial class remove_answer_user : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Provideds");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Provideds",
                columns: table => new
                {
                    AnswerID = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provideds", x => new { x.AnswerID, x.UserID });
                    table.ForeignKey(
                        name: "FK_Provideds_Answers",
                        column: x => x.AnswerID,
                        principalTable: "Answers",
                        principalColumn: "AID");
                    table.ForeignKey(
                        name: "FK_Provideds_Users",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Provideds_UserID",
                table: "Provideds",
                column: "UserID");
        }
    }
}
