using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMSweb.Migrations
{
    /// <inheritdoc />
    public partial class AlterForeingKeyConstrainGroupStudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Groups",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Users",
                table: "Students");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Groups",
                table: "Students",
                column: "GroupID",
                principalTable: "Groups",
                principalColumn: "GID",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Users",
                table: "Students",
                column: "StudentId",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Groups",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Users",
                table: "Students");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Groups",
                table: "Students",
                column: "GroupID",
                principalTable: "Groups",
                principalColumn: "GID");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Users",
                table: "Students",
                column: "StudentId",
                principalTable: "Users",
                principalColumn: "ID");
        }
    }
}
