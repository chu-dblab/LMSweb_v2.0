using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMSweb.Migrations
{
    /// <inheritdoc />
    public partial class fix_Provided_Pkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Provideds",
                table: "Provideds");

            migrationBuilder.AlterColumn<string>(
                name: "MissionID",
                table: "Provideds",
                type: "nvarchar(128)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)")
                .Annotation("Relational:ColumnOrder", 2);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Provideds",
                table: "Provideds",
                columns: new[] { "AnswerID", "UserID", "MissionID" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Provideds",
                table: "Provideds");

            migrationBuilder.AlterColumn<string>(
                name: "MissionID",
                table: "Provideds",
                type: "nvarchar(128)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)")
                .OldAnnotation("Relational:ColumnOrder", 2);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Provideds",
                table: "Provideds",
                columns: new[] { "AnswerID", "UserID" });
        }
    }
}
