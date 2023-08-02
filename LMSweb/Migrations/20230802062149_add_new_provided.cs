using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMSweb.Migrations
{
    /// <inheritdoc />
    public partial class add_new_provided : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Provideds",
                columns: table => new
                {
                    AnswerId = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    MissionId = table.Column<string>(type: "nvarchar(128)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provideds", x => new { x.AnswerId, x.UserId });
                    table.ForeignKey(
                        name: "FK_Provideds_Answers_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "Answers",
                        principalColumn: "AID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Provideds_Missions_MissionId",
                        column: x => x.MissionId,
                        principalTable: "Missions",
                        principalColumn: "MID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Provideds_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Provideds");
        }
    }
}
