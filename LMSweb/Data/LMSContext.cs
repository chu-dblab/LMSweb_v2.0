﻿using LMSweb.Models;
using LMSweb.Services;
using Microsoft.EntityFrameworkCore;

namespace LMSweb.Data;

public partial class LMSContext : DbContext
{
    public LMSContext()
    {
    }

    public LMSContext(DbContextOptions<LMSContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Answer> Answers { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Execution> Executions { get; set; }

    public virtual DbSet<ExperimentalProcedure> ExperimentalProcedures { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<Mission> Missions { get; set; }

    public virtual DbSet<Option> Options { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    if (!optionsBuilder.IsConfigured)
    //    {
    //        optionsBuilder.UseSqlServer("Data Source=localhost;Database=LMSContext;Trusted_Connection=True;TrustServerCertificate=True;");
    //    }        
    //    base.OnConfiguring(optionsBuilder);
    //}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Answer>(entity =>
        {
            entity.HasKey(e => e.Aid);

            entity.Property(e => e.Aid)
                .HasMaxLength(128)
                .HasColumnName("AID");
            entity.Property(e => e.Atime)
                .HasColumnType("datetime")
                .HasColumnName("ATime");
            entity.Property(e => e.QuestionId)
                .HasMaxLength(128)
                .HasColumnName("QuestionID");

            entity.HasOne(d => d.Question).WithMany(p => p.Answers)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Answers_Questions");

            entity.HasMany(d => d.Users).WithMany(p => p.Answers)
                .UsingEntity<Dictionary<string, object>>(
                    "Provided",
                    r => r.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Provideds_Users"),
                    l => l.HasOne<Answer>().WithMany()
                        .HasForeignKey("AnswerId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Provideds_Answers"),
                    j =>
                    {
                        j.HasKey("AnswerId", "UserId");
                        j.ToTable("Provideds");
                        j.IndexerProperty<string>("AnswerId")
                            .HasMaxLength(128)
                            .HasColumnName("AnswerID");
                        j.IndexerProperty<string>("UserId")
                            .HasMaxLength(128)
                            .HasColumnName("UserID");
                    });
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Cid);

            entity.Property(e => e.Cid)
                .HasMaxLength(128)
                .HasColumnName("CID");
            entity.Property(e => e.Cname)
                .HasMaxLength(128)
                .HasColumnName("CName");
            entity.Property(e => e.CreateTime).HasColumnType("datetime");
            entity.Property(e => e.TeacherId)
                .HasMaxLength(128)
                .HasColumnName("TeacherID");
            entity.Property(e => e.TestType).HasMaxLength(128);

            entity.HasOne(d => d.Teacher).WithMany(p => p.Courses)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Courses_Teachers");
        });

        modelBuilder.Entity<Execution>(entity =>
        {
            entity.HasKey(e => new { e.GroupId, e.MissionId });

            entity.Property(e => e.GroupId).HasColumnName("GroupID");
            entity.Property(e => e.MissionId)
                .HasMaxLength(128)
                .HasColumnName("MissionID");

            entity.HasOne(d => d.Group).WithMany(p => p.Executions)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Executions_Groups");

            entity.HasOne(d => d.Mission).WithMany(p => p.Executions)
                .HasForeignKey(d => d.MissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Executions_Missions");
        });

        modelBuilder.Entity<ExperimentalProcedure>(entity =>
        {
            entity.HasKey(e => e.EprocedureId);

            entity.Property(e => e.EprocedureId)
                .HasMaxLength(128)
                .HasColumnName("EProcedureID");
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.Gid);

            entity.Property(e => e.Gid)
                .ValueGeneratedNever()
                .HasColumnName("GID");
            entity.Property(e => e.Gname)
                .HasMaxLength(128)
                .HasColumnName("GName");
        });

        modelBuilder.Entity<Mission>(entity =>
        {
            entity.HasKey(e => e.Mid);

            entity.Property(e => e.Mid)
                .HasMaxLength(128)
                .HasColumnName("MID");
            entity.Property(e => e.CourseId)
                .HasMaxLength(128)
                .HasColumnName("CourseID");
            entity.Property(e => e.CurrentAction).HasMaxLength(128);
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.Mname)
                .HasMaxLength(128)
                .HasColumnName("MName");
            entity.Property(e => e.StartDate).HasColumnType("datetime");

            entity.HasOne(d => d.Course).WithMany(p => p.Missions)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK_Missions_Courses");
        });

        modelBuilder.Entity<Option>(entity =>
        {
            entity.Property(e => e.OptionId)
                .ValueGeneratedNever()
                .HasColumnName("OptionID");
            entity.Property(e => e.Ocontent).HasColumnName("OContent");
            entity.Property(e => e.QuestionId)
                .HasMaxLength(128)
                .HasColumnName("QuestionID");

            entity.HasOne(d => d.Question).WithMany(p => p.Options)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Options_Questions");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.Property(e => e.QuestionId)
                .HasMaxLength(128)
                .HasColumnName("QuestionID");
            entity.Property(e => e.CourseId)
                .HasMaxLength(128)
                .HasColumnName("CourseID");
            entity.Property(e => e.EprocedureId)
                .HasMaxLength(128)
                .HasColumnName("EProcedureID");
            entity.Property(e => e.Qcontent).HasColumnName("QContent");
            entity.Property(e => e.Qtype).HasColumnName("QType");

            entity.HasOne(d => d.Course).WithMany(p => p.Questions)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Questions_Courses");

            entity.HasOne(d => d.Eprocedure).WithMany(p => p.Questions)
                .HasForeignKey(d => d.EprocedureId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Questions_ExperimentalProcedures");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK_Student");

            entity.Property(e => e.StudentId)
                .HasMaxLength(128)
                .HasColumnName("StudentID");
            entity.Property(e => e.CourseId)
                .HasMaxLength(128)
                .HasColumnName("CourseID");
            entity.Property(e => e.GroupId).HasColumnName("GroupID");
            entity.Property(e => e.IsLeader).HasColumnName("isLeader");

            entity.HasOne(d => d.Course).WithMany(p => p.Students)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK_Student_Student");

            entity.HasOne(d => d.Group).WithMany(p => p.Students)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("FK_Students_Groups");

            entity.HasOne(d => d.User).WithOne(p => p.Student)
                .HasForeignKey<Student>(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Students_Users");
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.Property(e => e.TeacherId)
                .HasMaxLength(128)
                .HasColumnName("TeacherID");

            entity.HasOne(d => d.User).WithOne(p => p.Teacher)
                .HasForeignKey<Teacher>(d => d.TeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Teachers_Users");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Id)
                .HasMaxLength(128)
                .HasColumnName("ID");
            entity.Property(e => e.Gender).HasMaxLength(128);
            entity.Property(e => e.Name).HasMaxLength(128);
            entity.Property(e => e.RoleName).HasMaxLength(128);
            entity.Property(e => e.Upassword)
                .HasMaxLength(128)
                .HasColumnName("UPassword");
        });

        /* 測試資料 */
        var test_users = new List<User>
        {
            new User
            {
                Id = "S001",
                Upassword = HashHelper.SHA256Hash("S001"),
                Name = "林世楷",
                RoleName = "Student",
                Gender = "男"
            },
            new User
            {
                Id = "S002",
                Upassword = HashHelper.SHA256Hash("S002"),
                Name = "李國禎",
                RoleName = "Student",
                Gender = "男"
            },
            new User
            {
                Id = "S003",
                Upassword = HashHelper.SHA256Hash("S003"),
                Name = "許盈琪",
                RoleName = "Student",
                Gender = "女"
            },
            new User
            {
                Id = "S004",
                Upassword = HashHelper.SHA256Hash("S004"),
                Name = "Kevin",
                RoleName = "Student",
                Gender = "男"
            },
            new User
            {
                Id = "S005",
                Upassword = HashHelper.SHA256Hash("S005"),
                Name = "Vivian",
                RoleName = "Student",
                Gender = "女"
            },
            new User
            {
                Id = "S006",
                Upassword = HashHelper.SHA256Hash("S006"),
                Name = "Amy",
                RoleName = "Student",
                Gender = "女"
            },
            new User
            {
                Id = "T001",
                Upassword = HashHelper.SHA256Hash("T00001"),
                Name = "Lee",
                RoleName = "Teacher"
            },
            new User
            {
                Id = "T002",
                Upassword = HashHelper.SHA256Hash("T002"),
                Name = "曾老師",
                RoleName = "Teacher"
            },
            new User
            {
                Id = "T003",
                Upassword = HashHelper.SHA256Hash("T003"),
                Name = "李偉老師",
                RoleName = "Teacher"
            },
            new User
            {
                Id = "T004",
                Upassword = HashHelper.SHA256Hash("Kevin1004"),
                Name = "焰超老師",
                RoleName = "Teacher"
            }
        };
        modelBuilder.Entity<User>().HasData(test_users);

        var test_students = new List<Student>
        {
            new Student
            {
                StudentId = "S001",
                StudentName = "林世楷"
            },
            new Student
            {
                StudentId = "S002",
                StudentName = "李國禎"
            },
            new Student
            {
                StudentId = "S003",
                StudentName = "許盈琪"
            },
            new Student
            {
                StudentId = "S004",
                StudentName = "Kevin"
            },
            new Student
            {
                StudentId = "S005",
                StudentName = "Vivian"
            },
            new Student
            {
                StudentId = "S006",
                StudentName = "Amy"
            }
        };
        modelBuilder.Entity<Student>().HasData(test_students);

        var test_teachers = new List<Teacher>
        {
            new Teacher
            {
                TeacherId = "T001",
                TeacherName = "Lee"
            },
            new Teacher
            {
                TeacherId = "T002",
                TeacherName = "曾老師"
            },
            new Teacher
            {
                TeacherId = "T003",
                TeacherName = "李偉老師"
            },
            new Teacher
            {
                TeacherId = "T004",
                TeacherName = "焰超老師"
            }
        };
        modelBuilder.Entity<Teacher>().HasData(test_teachers);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
