using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMSweb.Migrations
{
    /// <inheritdoc />
    public partial class add_code : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExecutionContent_Groups_GroupID",
                table: "ExecutionContent");

            migrationBuilder.DropForeignKey(
                name: "FK_ExecutionContent_Missions_MissionID",
                table: "ExecutionContent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExecutionContent",
                table: "ExecutionContent");

            migrationBuilder.RenameTable(
                name: "ExecutionContent",
                newName: "ExecutionContents");

            migrationBuilder.RenameIndex(
                name: "IX_ExecutionContent_MissionID",
                table: "ExecutionContents",
                newName: "IX_ExecutionContents_MissionID");

            migrationBuilder.RenameIndex(
                name: "IX_ExecutionContent_GroupID",
                table: "ExecutionContents",
                newName: "IX_ExecutionContents_GroupID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExecutionContents",
                table: "ExecutionContents",
                columns: new[] { "MissionID", "GroupID" });

            migrationBuilder.AddForeignKey(
                name: "FK_ExecutionContents_Groups_GroupID",
                table: "ExecutionContents",
                column: "GroupID",
                principalTable: "Groups",
                principalColumn: "GID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExecutionContents_Missions_MissionID",
                table: "ExecutionContents",
                column: "MissionID",
                principalTable: "Missions",
                principalColumn: "MID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExecutionContents_Groups_GroupID",
                table: "ExecutionContents");

            migrationBuilder.DropForeignKey(
                name: "FK_ExecutionContents_Missions_MissionID",
                table: "ExecutionContents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExecutionContents",
                table: "ExecutionContents");

            migrationBuilder.RenameTable(
                name: "ExecutionContents",
                newName: "ExecutionContent");

            migrationBuilder.RenameIndex(
                name: "IX_ExecutionContents_MissionID",
                table: "ExecutionContent",
                newName: "IX_ExecutionContent_MissionID");

            migrationBuilder.RenameIndex(
                name: "IX_ExecutionContents_GroupID",
                table: "ExecutionContent",
                newName: "IX_ExecutionContent_GroupID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExecutionContent",
                table: "ExecutionContent",
                columns: new[] { "MissionID", "GroupID" });

            migrationBuilder.AddForeignKey(
                name: "FK_ExecutionContent_Groups_GroupID",
                table: "ExecutionContent",
                column: "GroupID",
                principalTable: "Groups",
                principalColumn: "GID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExecutionContent_Missions_MissionID",
                table: "ExecutionContent",
                column: "MissionID",
                principalTable: "Missions",
                principalColumn: "MID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
