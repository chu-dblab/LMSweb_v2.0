using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMSweb.Migrations
{
    /// <inheritdoc />
    public partial class fix_evaluationCoaching_pk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EvaluationCoachings",
                table: "EvaluationCoachings");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EvaluationCoachings",
                table: "EvaluationCoachings",
                columns: new[] { "AUID", "BUID", "MissionID" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EvaluationCoachings",
                table: "EvaluationCoachings");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EvaluationCoachings",
                table: "EvaluationCoachings",
                columns: new[] { "AUID", "BUID" });
        }
    }
}
