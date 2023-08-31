using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMSweb.Migrations
{
    /// <inheritdoc />
    public partial class Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Provideds");

            migrationBuilder.AddColumn<string>(
                name: "MissionID",
                table: "Answers",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Answers",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_MissionID",
                table: "Answers",
                column: "MissionID");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_UserID",
                table: "Answers",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Missions",
                table: "Answers",
                column: "MissionID",
                principalTable: "Missions",
                principalColumn: "MID");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Users",
                table: "Answers",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Missions",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Users",
                table: "Answers");

            migrationBuilder.DropIndex(
                name: "IX_Answers_MissionID",
                table: "Answers");

            migrationBuilder.DropIndex(
                name: "IX_Answers_UserID",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "MissionID",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Answers");

            migrationBuilder.CreateTable(
                name: "Provideds",
                columns: table => new
                {
                    AnswerID = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    MissionID = table.Column<string>(type: "nvarchar(128)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provideds", x => new { x.AnswerID, x.UserID, x.MissionID });
                    table.ForeignKey(
                        name: "FK_Provideds_Answers_AnswerID",
                        column: x => x.AnswerID,
                        principalTable: "Answers",
                        principalColumn: "AID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Provideds_Missions_MissionID",
                        column: x => x.MissionID,
                        principalTable: "Missions",
                        principalColumn: "MID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Provideds_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Provideds_MissionID",
                table: "Provideds",
                column: "MissionID");

            migrationBuilder.CreateIndex(
                name: "IX_Provideds_UserID",
                table: "Provideds",
                column: "UserID");
        }
    }
}
