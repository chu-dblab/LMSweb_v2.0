using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMSweb.Migrations
{
    /// <inheritdoc />
    public partial class InitalDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExperimentalProcedures",
                columns: table => new
                {
                    EProcedureID = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExperimentalProcedures", x => x.EProcedureID);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    GID = table.Column<int>(type: "int", nullable: false),
                    GName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.GID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    UPassword = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    RoleName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    TeacherID = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.TeacherID);
                    table.ForeignKey(
                        name: "FK_Teachers_Users",
                        column: x => x.TeacherID,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CID = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    TeacherID = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    CName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    TestType = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CID);
                    table.ForeignKey(
                        name: "FK_Courses_Teachers",
                        column: x => x.TeacherID,
                        principalTable: "Teachers",
                        principalColumn: "TeacherID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Missions",
                columns: table => new
                {
                    MID = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    MName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Detail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentAction = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CourseID = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Missions", x => x.MID);
                    table.ForeignKey(
                        name: "FK_Missions_Courses",
                        column: x => x.CourseID,
                        principalTable: "Courses",
                        principalColumn: "CID");
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    QuestionID = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    EProcedureID = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    CourseID = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    QType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QContent = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.QuestionID);
                    table.ForeignKey(
                        name: "FK_Questions_Courses",
                        column: x => x.CourseID,
                        principalTable: "Courses",
                        principalColumn: "CID");
                    table.ForeignKey(
                        name: "FK_Questions_ExperimentalProcedures",
                        column: x => x.EProcedureID,
                        principalTable: "ExperimentalProcedures",
                        principalColumn: "EProcedureID");
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentID = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    GroupID = table.Column<int>(type: "int", nullable: true),
                    CourseID = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    isLeader = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.StudentID);
                    table.ForeignKey(
                        name: "FK_Student_Student",
                        column: x => x.CourseID,
                        principalTable: "Courses",
                        principalColumn: "CID");
                    table.ForeignKey(
                        name: "FK_Students_Groups",
                        column: x => x.GroupID,
                        principalTable: "Groups",
                        principalColumn: "GID");
                    table.ForeignKey(
                        name: "FK_Students_Users",
                        column: x => x.StudentID,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Executions",
                columns: table => new
                {
                    GroupID = table.Column<int>(type: "int", nullable: false),
                    MissionID = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    CurrentStatus = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Executions", x => new { x.GroupID, x.MissionID });
                    table.ForeignKey(
                        name: "FK_Executions_Groups",
                        column: x => x.GroupID,
                        principalTable: "Groups",
                        principalColumn: "GID");
                    table.ForeignKey(
                        name: "FK_Executions_Missions",
                        column: x => x.MissionID,
                        principalTable: "Missions",
                        principalColumn: "MID");
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    AID = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Acontent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ATime = table.Column<DateTime>(type: "datetime", nullable: false),
                    QuestionID = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.AID);
                    table.ForeignKey(
                        name: "FK_Answers_Questions",
                        column: x => x.QuestionID,
                        principalTable: "Questions",
                        principalColumn: "QuestionID");
                });

            migrationBuilder.CreateTable(
                name: "Options",
                columns: table => new
                {
                    OptionID = table.Column<int>(type: "int", nullable: false),
                    OContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuestionID = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Options", x => x.OptionID);
                    table.ForeignKey(
                        name: "FK_Options_Questions",
                        column: x => x.QuestionID,
                        principalTable: "Questions",
                        principalColumn: "QuestionID");
                });

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
                name: "IX_Answers_QuestionID",
                table: "Answers",
                column: "QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_TeacherID",
                table: "Courses",
                column: "TeacherID");

            migrationBuilder.CreateIndex(
                name: "IX_Executions_MissionID",
                table: "Executions",
                column: "MissionID");

            migrationBuilder.CreateIndex(
                name: "IX_Missions_CourseID",
                table: "Missions",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Options_QuestionID",
                table: "Options",
                column: "QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_Provideds_UserID",
                table: "Provideds",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_CourseID",
                table: "Questions",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_EProcedureID",
                table: "Questions",
                column: "EProcedureID");

            migrationBuilder.CreateIndex(
                name: "IX_Students_CourseID",
                table: "Students",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_GroupID",
                table: "Students",
                column: "GroupID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Executions");

            migrationBuilder.DropTable(
                name: "Options");

            migrationBuilder.DropTable(
                name: "Provideds");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Missions");

            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "ExperimentalProcedures");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
