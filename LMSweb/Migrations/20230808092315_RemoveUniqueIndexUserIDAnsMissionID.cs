using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMSweb.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUniqueIndexUserIDAnsMissionID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Provideds_Answers_AnswerId",
                table: "Provideds");

            migrationBuilder.DropForeignKey(
                name: "FK_Provideds_Missions_MissionId",
                table: "Provideds");

            migrationBuilder.DropForeignKey(
                name: "FK_Provideds_Users_UserId",
                table: "Provideds");

            migrationBuilder.DropIndex(
                name: "IX_Provideds_MissionId",
                table: "Provideds");

            migrationBuilder.DropIndex(
                name: "IX_Provideds_UserId",
                table: "Provideds");

            migrationBuilder.RenameColumn(
                name: "MissionId",
                table: "Provideds",
                newName: "MissionID");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Provideds",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "AnswerId",
                table: "Provideds",
                newName: "AnswerID");

            migrationBuilder.CreateIndex(
                name: "IX_Provideds_MissionID",
                table: "Provideds",
                column: "MissionID");

            migrationBuilder.CreateIndex(
                name: "IX_Provideds_UserID",
                table: "Provideds",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Provideds_Answers_AnswerID",
                table: "Provideds",
                column: "AnswerID",
                principalTable: "Answers",
                principalColumn: "AID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Provideds_Missions_MissionID",
                table: "Provideds",
                column: "MissionID",
                principalTable: "Missions",
                principalColumn: "MID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Provideds_Users_UserID",
                table: "Provideds",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Provideds_Answers_AnswerID",
                table: "Provideds");

            migrationBuilder.DropForeignKey(
                name: "FK_Provideds_Missions_MissionID",
                table: "Provideds");

            migrationBuilder.DropForeignKey(
                name: "FK_Provideds_Users_UserID",
                table: "Provideds");

            migrationBuilder.DropIndex(
                name: "IX_Provideds_MissionID",
                table: "Provideds");

            migrationBuilder.DropIndex(
                name: "IX_Provideds_UserID",
                table: "Provideds");

            migrationBuilder.RenameColumn(
                name: "MissionID",
                table: "Provideds",
                newName: "MissionId");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Provideds",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "AnswerID",
                table: "Provideds",
                newName: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_Provideds_MissionId",
                table: "Provideds",
                column: "MissionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Provideds_UserId",
                table: "Provideds",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Provideds_Answers_AnswerId",
                table: "Provideds",
                column: "AnswerId",
                principalTable: "Answers",
                principalColumn: "AID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Provideds_Missions_MissionId",
                table: "Provideds",
                column: "MissionId",
                principalTable: "Missions",
                principalColumn: "MID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Provideds_Users_UserId",
                table: "Provideds",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
