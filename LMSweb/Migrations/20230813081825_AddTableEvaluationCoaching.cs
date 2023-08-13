using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMSweb.Migrations
{
    /// <inheritdoc />
    public partial class AddTableEvaluationCoaching : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EvaluationCoachings",
                columns: table => new
                {
                    AUID = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, comment: "給評價者編號"),
                    BUID = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, comment: "接受評價者編號"),
                    MissionID = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationCoachings", x => new { x.AUID, x.BUID });
                    table.ForeignKey(
                        name: "FK_EvaluationCoaching_AUsers",
                        column: x => x.AUID,
                        principalTable: "Users",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_EvaluationCoaching_BUsers",
                        column: x => x.BUID,
                        principalTable: "Users",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_EvaluationCoaching_Missions",
                        column: x => x.MissionID,
                        principalTable: "Missions",
                        principalColumn: "MID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationCoachings_MissionID",
                table: "EvaluationCoachings",
                column: "MissionID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EvaluationCoachings");
        }
    }
}
