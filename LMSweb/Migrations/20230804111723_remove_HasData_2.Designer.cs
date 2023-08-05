﻿// <auto-generated />
using System;
using LMSweb.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LMSweb.Migrations
{
    [DbContext(typeof(LMSContext))]
    [Migration("20230804111723_remove_HasData_2")]
    partial class remove_HasData_2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LMSweb.Models.Answer", b =>
                {
                    b.Property<string>("Aid")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("AID");

                    b.Property<string>("Acontent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Atime")
                        .HasColumnType("datetime")
                        .HasColumnName("ATime");

                    b.Property<string>("QuestionId")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("QuestionID");

                    b.HasKey("Aid");

                    b.HasIndex("QuestionId");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("LMSweb.Models.Course", b =>
                {
                    b.Property<string>("Cid")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("CID");

                    b.Property<string>("Cname")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("CName");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime");

                    b.Property<string>("TeacherId")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("TeacherID");

                    b.Property<int>("TestType")
                        .HasMaxLength(128)
                        .HasColumnType("int");

                    b.HasKey("Cid");

                    b.HasIndex("TeacherId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("LMSweb.Models.Execution", b =>
                {
                    b.Property<int>("GroupId")
                        .HasColumnType("int")
                        .HasColumnName("GroupID");

                    b.Property<string>("MissionId")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("MissionID");

                    b.Property<string>("CurrentStatus")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GroupId", "MissionId");

                    b.HasIndex("MissionId");

                    b.ToTable("Executions");
                });

            modelBuilder.Entity("LMSweb.Models.ExperimentalProcedure", b =>
                {
                    b.Property<string>("EprocedureId")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("EProcedureID");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EprocedureId");

                    b.ToTable("ExperimentalProcedures");
                });

            modelBuilder.Entity("LMSweb.Models.Group", b =>
                {
                    b.Property<int>("Gid")
                        .HasColumnType("int")
                        .HasColumnName("GID");

                    b.Property<string>("Gname")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("GName");

                    b.HasKey("Gid");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("LMSweb.Models.Mission", b =>
                {
                    b.Property<string>("Mid")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("MID");

                    b.Property<string>("CourseId")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("CourseId");

                    b.Property<string>("CurrentAction")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Detail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Mname")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("MName");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime");

                    b.HasKey("Mid");

                    b.HasIndex("CourseId");

                    b.ToTable("Missions");
                });

            modelBuilder.Entity("LMSweb.Models.Option", b =>
                {
                    b.Property<int>("OptionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OptionID"));

                    b.Property<string>("Ocontent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("OContent");

                    b.Property<string>("QuestionId")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("QuestionID");

                    b.HasKey("OptionID");

                    b.HasIndex("QuestionId");

                    b.ToTable("Options");
                });

            modelBuilder.Entity("LMSweb.Models.Provided", b =>
                {
                    b.Property<string>("AnswerId")
                        .HasColumnType("nvarchar(128)")
                        .HasColumnOrder(0);

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(128)")
                        .HasColumnOrder(1);

                    b.Property<string>("MissionId")
                        .IsRequired()
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("AnswerId", "UserId");

                    b.HasIndex("MissionId")
                        .IsUnique();

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Provideds");
                });

            modelBuilder.Entity("LMSweb.Models.Question", b =>
                {
                    b.Property<string>("QuestionId")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("QuestionID");

                    b.Property<string>("CourseId")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("CourseId");

                    b.Property<string>("EprocedureId")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("EProcedureID");

                    b.Property<string>("Qcontent")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("QContent");

                    b.Property<string>("Qtype")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("QType");

                    b.HasKey("QuestionId");

                    b.HasIndex("CourseId");

                    b.HasIndex("EprocedureId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("LMSweb.Models.Student", b =>
                {
                    b.Property<string>("StudentId")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("StudentId");

                    b.Property<string>("CourseId")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("CourseId");

                    b.Property<int?>("GroupId")
                        .HasColumnType("int")
                        .HasColumnName("GroupID");

                    b.Property<bool>("IsLeader")
                        .HasColumnType("bit")
                        .HasColumnName("isLeader");

                    b.Property<string>("StudentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StudentId")
                        .HasName("PK_Student");

                    b.HasIndex("CourseId");

                    b.HasIndex("GroupId");

                    b.ToTable("Students");

                    b.HasData(
                        new
                        {
                            StudentId = "S001",
                            IsLeader = false,
                            StudentName = "林世楷"
                        },
                        new
                        {
                            StudentId = "S002",
                            IsLeader = false,
                            StudentName = "李國禎"
                        },
                        new
                        {
                            StudentId = "S003",
                            IsLeader = false,
                            StudentName = "許盈琪"
                        },
                        new
                        {
                            StudentId = "S004",
                            IsLeader = false,
                            StudentName = "Kevin"
                        },
                        new
                        {
                            StudentId = "S005",
                            IsLeader = false,
                            StudentName = "Vivian"
                        },
                        new
                        {
                            StudentId = "S006",
                            IsLeader = false,
                            StudentName = "Amy"
                        });
                });

            modelBuilder.Entity("LMSweb.Models.Teacher", b =>
                {
                    b.Property<string>("TeacherId")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("TeacherID");

                    b.Property<string>("TeacherName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TeacherId");

                    b.ToTable("Teachers");

                    b.HasData(
                        new
                        {
                            TeacherId = "T001",
                            TeacherName = "Lee"
                        },
                        new
                        {
                            TeacherId = "T002",
                            TeacherName = "曾老師"
                        },
                        new
                        {
                            TeacherId = "T003",
                            TeacherName = "李偉老師"
                        },
                        new
                        {
                            TeacherId = "T004",
                            TeacherName = "焰超老師"
                        });
                });

            modelBuilder.Entity("LMSweb.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("ID");

                    b.Property<string>("Gender")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Upassword")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("UPassword");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = "S001",
                            Gender = "男",
                            Name = "林世楷",
                            RoleName = "Student",
                            Upassword = "c5e0e70cb9001fc326a9d5b3c39c1d3b48919bf3adacc633729bfff7c27f1d26"
                        },
                        new
                        {
                            Id = "S002",
                            Gender = "男",
                            Name = "李國禎",
                            RoleName = "Student",
                            Upassword = "c842f8af9e82946217a5f35c046c0470ce855b145a093448295d758810c68303"
                        },
                        new
                        {
                            Id = "S003",
                            Gender = "女",
                            Name = "許盈琪",
                            RoleName = "Student",
                            Upassword = "f598aee2eda0ebd461a60eb24ecc6378674be0d06a591ef8f25b201e4f619e48"
                        },
                        new
                        {
                            Id = "S004",
                            Gender = "男",
                            Name = "Kevin",
                            RoleName = "Student",
                            Upassword = "db3ea858db39f2f6eafd7ad39f7798428d9e6244a430919f84c7dc8b905081ad"
                        },
                        new
                        {
                            Id = "S005",
                            Gender = "女",
                            Name = "Vivian",
                            RoleName = "Student",
                            Upassword = "09fd191dc08a0375f4f10fd8ce970d8193a0b475bb3d75db4b8221e8f0d74979"
                        },
                        new
                        {
                            Id = "S006",
                            Gender = "女",
                            Name = "Amy",
                            RoleName = "Student",
                            Upassword = "19a85017e5a5057f9cb3104e7afde89aea6c4d74f544ba5eaeaab256bcf937af"
                        },
                        new
                        {
                            Id = "T001",
                            Name = "Lee",
                            RoleName = "Teacher",
                            Upassword = "15152d459354c17470fbeba5c03aa9b0790b237b04f190aba04b2a3d1afe64bf"
                        },
                        new
                        {
                            Id = "T002",
                            Name = "曾老師",
                            RoleName = "Teacher",
                            Upassword = "9ba7d0652682e1fe75b90bd1ea8a1a69e679a0039c80fc9c85e10e2ff7ddc793"
                        },
                        new
                        {
                            Id = "T003",
                            Name = "李偉老師",
                            RoleName = "Teacher",
                            Upassword = "963606fbc3791a6c3053264f977ce910821a69680e5e41de99e6b3f04d7d0471"
                        },
                        new
                        {
                            Id = "T004",
                            Name = "焰超老師",
                            RoleName = "Teacher",
                            Upassword = "efcc4b59f003f0a56d6869d748cf3d31c3a4d36a6a8b9bbe60cb2171294b896c"
                        });
                });

            modelBuilder.Entity("LMSweb.Models.Answer", b =>
                {
                    b.HasOne("LMSweb.Models.Question", "Question")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId")
                        .IsRequired()
                        .HasConstraintName("FK_Answers_Questions");

                    b.Navigation("Question");
                });

            modelBuilder.Entity("LMSweb.Models.Course", b =>
                {
                    b.HasOne("LMSweb.Models.Teacher", "Teacher")
                        .WithMany("Courses")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK_Courses_Teachers");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("LMSweb.Models.Execution", b =>
                {
                    b.HasOne("LMSweb.Models.Group", "Group")
                        .WithMany("Executions")
                        .HasForeignKey("GroupId")
                        .IsRequired()
                        .HasConstraintName("FK_Executions_Groups");

                    b.HasOne("LMSweb.Models.Mission", "Mission")
                        .WithMany("Executions")
                        .HasForeignKey("MissionId")
                        .IsRequired()
                        .HasConstraintName("FK_Executions_Missions");

                    b.Navigation("Group");

                    b.Navigation("Mission");
                });

            modelBuilder.Entity("LMSweb.Models.Mission", b =>
                {
                    b.HasOne("LMSweb.Models.Course", "Course")
                        .WithMany("Missions")
                        .HasForeignKey("CourseId")
                        .HasConstraintName("FK_Missions_Courses");

                    b.Navigation("Course");
                });

            modelBuilder.Entity("LMSweb.Models.Option", b =>
                {
                    b.HasOne("LMSweb.Models.Question", "Question")
                        .WithMany("Options")
                        .HasForeignKey("QuestionId")
                        .IsRequired()
                        .HasConstraintName("FK_Options_Questions");

                    b.Navigation("Question");
                });

            modelBuilder.Entity("LMSweb.Models.Provided", b =>
                {
                    b.HasOne("LMSweb.Models.Answer", "Answer")
                        .WithMany("Provideds")
                        .HasForeignKey("AnswerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LMSweb.Models.Mission", "Mission")
                        .WithOne("Provided")
                        .HasForeignKey("LMSweb.Models.Provided", "MissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LMSweb.Models.User", "User")
                        .WithOne("Provided")
                        .HasForeignKey("LMSweb.Models.Provided", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Answer");

                    b.Navigation("Mission");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LMSweb.Models.Question", b =>
                {
                    b.HasOne("LMSweb.Models.Course", "Course")
                        .WithMany("Questions")
                        .HasForeignKey("CourseId")
                        .HasConstraintName("FK_Questions_Courses");

                    b.HasOne("LMSweb.Models.ExperimentalProcedure", "Eprocedure")
                        .WithMany("Questions")
                        .HasForeignKey("EprocedureId")
                        .IsRequired()
                        .HasConstraintName("FK_Questions_ExperimentalProcedures");

                    b.Navigation("Course");

                    b.Navigation("Eprocedure");
                });

            modelBuilder.Entity("LMSweb.Models.Student", b =>
                {
                    b.HasOne("LMSweb.Models.Course", "Course")
                        .WithMany("Students")
                        .HasForeignKey("CourseId")
                        .HasConstraintName("FK_Student_Student");

                    b.HasOne("LMSweb.Models.Group", "Group")
                        .WithMany("Students")
                        .HasForeignKey("GroupId")
                        .HasConstraintName("FK_Students_Groups");

                    b.HasOne("LMSweb.Models.User", "User")
                        .WithOne("Student")
                        .HasForeignKey("LMSweb.Models.Student", "StudentId")
                        .IsRequired()
                        .HasConstraintName("FK_Students_Users");

                    b.Navigation("Course");

                    b.Navigation("Group");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LMSweb.Models.Teacher", b =>
                {
                    b.HasOne("LMSweb.Models.User", "User")
                        .WithOne("Teacher")
                        .HasForeignKey("LMSweb.Models.Teacher", "TeacherId")
                        .IsRequired()
                        .HasConstraintName("FK_Teachers_Users");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LMSweb.Models.Answer", b =>
                {
                    b.Navigation("Provideds");
                });

            modelBuilder.Entity("LMSweb.Models.Course", b =>
                {
                    b.Navigation("Missions");

                    b.Navigation("Questions");

                    b.Navigation("Students");
                });

            modelBuilder.Entity("LMSweb.Models.ExperimentalProcedure", b =>
                {
                    b.Navigation("Questions");
                });

            modelBuilder.Entity("LMSweb.Models.Group", b =>
                {
                    b.Navigation("Executions");

                    b.Navigation("Students");
                });

            modelBuilder.Entity("LMSweb.Models.Mission", b =>
                {
                    b.Navigation("Executions");

                    b.Navigation("Provided");
                });

            modelBuilder.Entity("LMSweb.Models.Question", b =>
                {
                    b.Navigation("Answers");

                    b.Navigation("Options");
                });

            modelBuilder.Entity("LMSweb.Models.Teacher", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("LMSweb.Models.User", b =>
                {
                    b.Navigation("Provided");

                    b.Navigation("Student");

                    b.Navigation("Teacher");
                });
#pragma warning restore 612, 618
        }
    }
}
