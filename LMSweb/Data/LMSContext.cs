using LMSweb.Models;
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
    //public virtual DbSet<Provided> Provideds { get; set; }
    public virtual DbSet<ExecutionContent> ExecutionContents { get; set; }
    public virtual DbSet<EvaluationCoaching> EvaluationCoachings { get; set; }

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
            entity.Property(e => e.UserId)
                .HasMaxLength(128)
                .HasColumnName("UserID");
            entity.Property(e => e.MissionId)
                .HasMaxLength(128)
                .HasColumnName("MissionID");

            entity.HasOne(d => d.Question).WithMany(p => p.Answers)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Answers_Questions");
            entity.HasOne(d => d.Mission).WithMany(p => p.Answers)
                .HasForeignKey(d => d.MissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Answers_Missions");
            entity.HasOne(d => d.User).WithMany(p => p.Answers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Answers_Users");
            
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
            entity.Property(e => e.CourseId)
                .HasMaxLength(128)
                .HasColumnName("CourseID");

            entity.HasOne(d => d.Course)
                .WithMany(d => d.Groups)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Courses_Groups");
        });

        modelBuilder.Entity<Mission>(entity =>
        {
            entity.HasKey(e => e.Mid);

            entity.Property(e => e.Mid)
                .HasMaxLength(128)
                .HasColumnName("MID");
            entity.Property(e => e.CourseId)
                .HasMaxLength(128)
                .HasColumnName("CourseId");
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
            entity.HasKey(e => e.OptionID);
            entity.Property(e => e.OptionID).ValueGeneratedOnAdd();
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
                .HasColumnName("CourseId");
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
                .HasColumnName("StudentId");
            entity.Property(e => e.CourseId)
                .HasMaxLength(128)
                .HasColumnName("CourseId");
            entity.Property(e => e.GroupId).HasColumnName("GroupID");
            entity.Property(e => e.IsLeader).HasColumnName("isLeader");

            entity.HasOne(d => d.Course).WithMany(p => p.Students)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK_Student_Student");

            entity.HasOne(d => d.Group).WithMany(p => p.Students)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Students_Groups");

            entity.HasOne(d => d.User).WithOne(p => p.Student)
                .HasForeignKey<Student>(d => d.StudentId)
                .OnDelete(DeleteBehavior.Cascade)
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

        //modelBuilder.Entity<Provided>(entity =>
        //{
        //    entity.HasKey(e => new {e.AnswerId, e.UserId, e.MissionId});
        //    entity.Property(e => e.AnswerId).HasColumnName("AnswerID");
        //    entity.Property(e => e.UserId).HasColumnName("UserID");
        //    entity.Property(e => e.MissionId).HasColumnName("MissionID");

        //});
        //modelBuilder.Entity<Provided>().HasIndex(e=>e.MissionId).IsUnique(false);
        //modelBuilder.Entity<Provided>().HasIndex(e=>e.UserId).IsUnique(false);

        modelBuilder.Entity<ExecutionContent>(entitl =>
        {
            entitl.HasKey(e => e.Id);
            entitl.Property(e => e.Id).ValueGeneratedOnAdd();
            entitl.Property(e => e.MissionId).HasColumnName("MissionID");
            entitl.Property(e => e.GroupId).HasColumnName("GroupID");
        });
        modelBuilder.Entity<ExecutionContent>().HasIndex(e=>e.MissionId).IsUnique(false);
        modelBuilder.Entity<ExecutionContent>().HasIndex(e=>e.GroupId).IsUnique(false);

        modelBuilder.Entity<EvaluationCoaching>(entity =>
        {
            entity.HasKey(e => new {e.AUID, e.BUID, e.MissionId});
            entity.Property(e => e.AUID)
                .HasMaxLength(128)
                .HasColumnName("AUID");
            entity.Property(e => e.BUID)
                .HasMaxLength(128)
                .HasColumnName("BUID");
            entity.Property(e => e.MissionId)
                .HasMaxLength(128)
                .HasColumnName("MissionID");
            entity.HasIndex(e=>e.MissionId).IsUnique(false);

            entity.Property(e => e.Evaluation)
                .HasColumnType("nvarchar(MAX)");
            entity.Property(e => e.Coaching)
                .HasColumnType("nvarchar(MAX)");

            entity.HasOne(d => d.AUser).WithMany(p => p.EvaluationCoachingAUsers)
               .HasForeignKey(d => d.AUID)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_EvaluationCoaching_AUsers");

            entity.HasOne(d => d.BUser).WithMany(p => p.EvaluationCoachingBUsers)
                .HasForeignKey(d => d.BUID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EvaluationCoaching_BUsers");

            entity.HasOne(d => d.Mission).WithMany(p => p.EvaluationCoachings)
                .HasForeignKey(d => d.MissionId)
                .HasConstraintName("FK_EvaluationCoaching_Missions");
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
            },
            new User
            {
                Id = "T005",
                Upassword = HashHelper.SHA256Hash("mfps5221"),
                Name = "俊興老師",
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
            },
            new Teacher
            {
                TeacherId = "T005",
                TeacherName = "俊興老師"
            }
        };
        modelBuilder.Entity<Teacher>().HasData(test_teachers);

        var test_experimental = new List<ExperimentalProcedure>
        {
            new ExperimentalProcedure
            {
                EprocedureId = "D",
                Name = "畫流程圖"
            },
            new ExperimentalProcedure
            {
                EprocedureId = "C",
                Name = "寫程式碼"
            },
            new ExperimentalProcedure
            {
                EprocedureId = "0",
                Name = "目標設置"
            },
            new ExperimentalProcedure
            {
                EprocedureId = "1",
                Name = "任務監控"
            },
            new ExperimentalProcedure
            {
                EprocedureId = "2",
                Name = "自我反思"
            },
            new ExperimentalProcedure
            {
                EprocedureId = "3",
                Name = "目標設置"
            },
            new ExperimentalProcedure
            {
                EprocedureId = "4",
                Name = "任務監控"
            },
            new ExperimentalProcedure
            {
                EprocedureId = "5",
                Name = "團隊反思"
            },
            new ExperimentalProcedure
            {
                EprocedureId = "6",
                Name = "同儕互評"
            },
            new ExperimentalProcedure
            {
                EprocedureId = "7",
                Name = "同儕回饋"
            },
        };
        modelBuilder.Entity<ExperimentalProcedure>().HasData(test_experimental);

        var test_Question = new List<Question>
        {
            // 實驗組一 A
            new Question
            {
                QuestionId = "PGS01A",
                EprocedureId = "0",
                Qtype = "0",
                Qcontent = "我期望這次小組任務可以獲得幾分?"
            },
            new Question
            {
                QuestionId = "PGS02A",
                EprocedureId = "0",
                Qtype = "1",
                Qcontent = "為達成目標，我將採用以下哪些學習方法? (可複選)"
            },
            new Question
            {
                QuestionId = "PGS03A",
                EprocedureId = "0",
                Qtype = "1",
                Qcontent = "在合作學習中，我會積極參與哪些合作任務? (可複選)"
            },
            new Question
            {
                QuestionId = "TM01A",
                EprocedureId = "1",
                Qtype = "0",
                Qcontent = "我認為這次任務需要多長時間完成程式碼的撰寫？"
            },
            new Question
            {
                QuestionId = "CM01A",
                EprocedureId = "1",
                Qtype = "0",
                Qcontent = "我認為這份流程圖是正確的嗎？"
            },
            new Question
            {
                QuestionId = "TE01A",
                EprocedureId = "2",
                Qtype = "0",
                Qcontent = "我們有沒有依照老師指示完成任務？"
            },
            new Question
            {
                QuestionId = "SR01A",
                EprocedureId = "2",
                Qtype = "0",
                Qcontent = "我覺得這次小組任務可以獲得幾分?"
            },
            new Question
            {
                QuestionId = "SR02A",
                EprocedureId = "2",
                Qtype = "1",
                Qcontent = "在本次學習任務中，我採用了以下哪些學習方法? (可複選)"
            },
            new Question
            {
                QuestionId = "SR03A",
                EprocedureId = "2",
                Qtype = "1",
                Qcontent = "在本次學習任務中，我積極參與了以下哪些合作任務? (可複選)"
            },
            new Question
            {
                QuestionId = "SR04A",
                EprocedureId = "2",
                Qtype = "2",
                Qcontent = "依據上述反思，我覺得下個任務可以如何改進?"
            },
            // 實驗組三 B
            new Question
            {
                QuestionId = "PGS01B",
                EprocedureId = "3",
                Qtype = "0",
                Qcontent = "我們期望這次小組任務可以獲得幾分?"
            },
            new Question
            {
                QuestionId = "PGS02B",
                EprocedureId = "3",
                Qtype = "1",
                Qcontent = "為達成目標，我們小組將採用以下哪些學習方法? (可複選)"
            },
            new Question
            {
                QuestionId = "PGS03B",
                EprocedureId = "3",
                Qtype = "1",
                Qcontent = "在合作學習中，我們小組會積極參與哪些合作任務? (可複選)"
            },
            new Question
            {
                QuestionId = "TM01B",
                EprocedureId = "4",
                Qtype = "0",
                Qcontent = "我們認為這次任務需要多長時間完成程式碼的撰寫？"
            },
            new Question
            {
                QuestionId = "CM01B",
                EprocedureId = "4",
                Qtype = "0",
                Qcontent = "我們認為這份流程圖是正確的嗎？"
            },
            new Question
            {
                QuestionId = "TE01B",
                EprocedureId = "5",
                Qtype = "0",
                Qcontent = "我們有沒有依照老師指示完成任務？"
            },
            new Question
            {
                QuestionId = "TR01B",
                EprocedureId = "5",
                Qtype = "0",
                Qcontent = "我們覺得這次小組任務可以獲得幾分?"
            },
            new Question
            {
                QuestionId = "TR02B",
                EprocedureId = "5",
                Qtype = "1",
                Qcontent = "在本次學習任務中，我們小組採用了以下哪些學習方法? (可複選)"
            },
            new Question
            {
                QuestionId = "TR03B",
                EprocedureId = "5",
                Qtype = "1",
                Qcontent = "在本次學習任務中，我們小組積極參與了以下哪些合作任務? (可複選)"
            },
            new Question
            {
                QuestionId = "TR04B",
                EprocedureId = "5",
                Qtype = "2",
                Qcontent = "依據上述反思，我們小組覺得下個任務可以如何改進?"
            },
            new Question
            {
                QuestionId = "PE01",
                EprocedureId = "6",
                Qtype = "0",
                Qcontent = "我們覺得這一組的邏輯性可以獲得幾分?"
            },
            new Question
            {
                QuestionId = "PE02",
                EprocedureId = "6",
                Qtype = "0",
                Qcontent = "我們覺得這一組的正確性可以獲得幾分?"
            },
            new Question
            {
                QuestionId = "PE03",
                EprocedureId = "6",
                Qtype = "0",
                Qcontent = "我們覺得這一組的可讀性可以獲得幾分?"
            },
            new Question
            {
                QuestionId = "PEA01",
                EprocedureId = "6",
                Qtype = "2",
                Qcontent = "我們想稱讚這組的流程圖或程式碼"
            },
            new Question
            {
                QuestionId = "PEA02",
                EprocedureId = "6",
                Qtype = "2",
                Qcontent = "我們想批評這組的流程圖或程式碼"
            },
            new Question
            {
                QuestionId = "PEC03",
                EprocedureId = "6",
                Qtype = "2",
                Qcontent = "我們想指出這組有錯誤的地方"
            },
            new Question
            {
                QuestionId = "PEM07",
                EprocedureId = "6",
                Qtype = "2",
                Qcontent = "我們想請這組想想看如何改進他們的流程圖或程式碼"
            },
            new Question
            {
                QuestionId = "PEIR08",
                EprocedureId = "6",
                Qtype = "2",
                Qcontent = "我們想說跟這組的作品無關的評論"
            },
            new Question
            {
                QuestionId = "PC01",
                EprocedureId = "7",
                Qtype = "0",
                Qcontent = "該組評價合理嗎?"
            },
            new Question
            {
                QuestionId = "PCA01",
                EprocedureId = "7",
                Qtype = "2",
                Qcontent = "我們想稱讚這組給的評語"
            },
            new Question
            {
                QuestionId = "PCA02",
                EprocedureId = "7",
                Qtype = "2",
                Qcontent = "我們想批評這組給的評語"
            },
            new Question
            {
                QuestionId = "PCC03",
                EprocedureId = "7",
                Qtype = "2",
                Qcontent = "我們想指出這組給的評語有錯誤的地方"
            },
            new Question
            {
                QuestionId = "PCM07",
                EprocedureId = "7",
                Qtype = "2",
                Qcontent = "我們想請這組想想看如何改進他們的評語"
            },
            new Question
            {
                QuestionId = "PCIR08",
                EprocedureId = "7",
                Qtype = "2",
                Qcontent = "我們想說跟這組的評語無關的回饋"
            },
        };
        modelBuilder.Entity<Question>().HasData(test_Question);

        var test_option = new List<Option>
        {
            // PGS01A 選項
            new Option
            {
                Ocontent = "90分以上",
                QuestionId = "PGS01A"
            },
            new Option
            {
                Ocontent = "80~90分",
                QuestionId = "PGS01A"
            },
            new Option
            {
                Ocontent = "70~80分",
                QuestionId = "PGS01A"
            },
            new Option
            {
                Ocontent = "70分以下",
                QuestionId = "PGS01A"
            },
            // PGS02A 選項
            new Option
            {
                Ocontent = "小組成員一起合作 ",
                QuestionId = "PGS02A"
            },
            new Option
            {
                Ocontent = "上網找資料",
                QuestionId = "PGS02A"
            },
            new Option
            {
                Ocontent = "參考其他小組的解決方案 ",
                QuestionId = "PGS02A"
            },
            new Option
            {
                Ocontent = "查看教材",
                QuestionId = "PGS02A"
            },
            new Option
            {
                Ocontent = "請教老師或同學",
                QuestionId = "PGS02A"
            },
            // PGS03A 選項
            new Option
            {
                Ocontent = "小組成員一起討論",
                QuestionId = "PGS03A"
            },
            new Option
            {
                Ocontent = "合作規劃流程圖",
                QuestionId = "PGS03A"
            },
            new Option
            {
                Ocontent = "合作撰寫程式碼",
                QuestionId = "PGS03A"
            },
            // TM01A 選項
            new Option
            {
                Ocontent = "10分鐘以內",
                QuestionId = "TM01A"
            },
            new Option
            {
                Ocontent = "10-15分鐘",
                QuestionId = "TM01A"
            },
            new Option
            {
                Ocontent = "15分鐘以上",
                QuestionId = "TM01A"
            },
            // CM01A 選項
            new Option
            {
                Ocontent = "正確",
                QuestionId = "CM01A"
            },
            new Option
            {
                Ocontent = "不完全正確",
                QuestionId = "CM01A"
            },
            new Option
            {
                Ocontent = "不正確",
                QuestionId = "CM01A"
            },
            // TE01A 選項
            new Option
            {
                Ocontent = "有",
                QuestionId = "TE01A"
            },
            new Option
            {
                Ocontent = "沒有",
                QuestionId = "TE01A"
            },
            // SR01A 選項
            new Option
            {
                Ocontent = "90分以上",
                QuestionId = "SR01A"
            },
            new Option
            {
                Ocontent = "80~90分",
                QuestionId = "SR01A"
            },
            new Option
            {
                Ocontent = "70~80分",
                QuestionId = "SR01A"
            },
            new Option
            {
                Ocontent = "60~70分",
                QuestionId = "SR01A"
            },
            new Option
            {
                Ocontent = "60分以下",
                QuestionId = "SR01A"
            },
            // SR02A 選項
            new Option
            {
                Ocontent = "小組成員一起合作 ",
                QuestionId = "SR02A"
            },
            new Option
            {
                Ocontent = "上網找資料",
                QuestionId = "SR02A"
            },
            new Option
            {
                Ocontent = "參考其他小組的解決方案",
                QuestionId = "SR02A"
            },
            new Option
            {
                Ocontent = "查看教材",
                QuestionId = "SR02A"
            },
            new Option
            {
                Ocontent = "請教老師或同學",
                QuestionId = "SR02A"
            },
            // SR03A 選項
            new Option
            {
                Ocontent = "小組成員一起討論",
                QuestionId = "SR03A"
            },
            new Option
            {
                Ocontent = "合作規劃流程圖",
                QuestionId = "SR03A"
            },
            new Option
            {
                Ocontent = "合作撰寫程式碼",
                QuestionId = "SR03A"
            },

            // PGS01B 選項
            new Option
            {
                Ocontent = "90分以上",
                QuestionId = "PGS01B"
            },
            new Option
            {
                Ocontent = "80~90分",
                QuestionId = "PGS01B"
            },
            new Option
            {
                Ocontent = "70~80分",
                QuestionId = "PGS01B"
            },
            new Option
            {
                Ocontent = "70分以下",
                QuestionId = "PGS01B"
            },
            // PGS02B 選項
            new Option
            {
                Ocontent = "小組成員一起合作 ",
                QuestionId = "PGS02B"
            },
            new Option
            {
                Ocontent = "上網找資料",
                QuestionId = "PGS02B"
            },
            new Option
            {
                Ocontent = "參考其他小組的解決方案 ",
                QuestionId = "PGS02B"
            },
            new Option
            {
                Ocontent = "查看教材",
                QuestionId = "PGS02B"
            },
            new Option
            {
                Ocontent = "請教老師或同學",
                QuestionId = "PGS02B"
            },
            // PGS03B 選項
            new Option
            {
                Ocontent = "小組成員一起討論",
                QuestionId = "PGS03B"
            },
            new Option
            {
                Ocontent = "合作規劃流程圖",
                QuestionId = "PGS03B"
            },
            new Option
            {
                Ocontent = "合作撰寫程式碼",
                QuestionId = "PGS03B"
            },
            // TM01B 選項
            new Option
            {
                Ocontent = "10分鐘以內",
                QuestionId = "TM01B"
            },
            new Option
            {
                Ocontent = "10-15分鐘",
                QuestionId = "TM01B"
            },
            new Option
            {
                Ocontent = "15分鐘以上",
                QuestionId = "TM01B"
            },
            // CM01B 選項
            new Option
            {
                Ocontent = "正確",
                QuestionId = "CM01B"
            },
            new Option
            {
                Ocontent = "不完全正確",
                QuestionId = "CM01B"
            },
            new Option
            {
                Ocontent = "不正確",
                QuestionId = "CM01B"
            },
            // TE01B 選項
            new Option
            {
                Ocontent = "有",
                QuestionId = "TE01B"
            },
            new Option
            {
                Ocontent = "沒有",
                QuestionId = "TE01B"
            },
            // TR01B 選項
            new Option
            {
                Ocontent = "90分以上",
                QuestionId = "TR01B"
            },
            new Option
            {
                Ocontent = "80~90分",
                QuestionId = "TR01B"
            },
            new Option
            {
                Ocontent = "70~80分",
                QuestionId = "TR01B"
            },
            new Option
            {
                Ocontent = "60~70分",
                QuestionId = "TR01B"
            },
            new Option
            {
                Ocontent = "60分以下",
                QuestionId = "TR01B"
            },
            // TR02B 選項
            new Option
            {
                Ocontent = "小組成員一起合作 ",
                QuestionId = "TR02B"
            },
            new Option
            {
                Ocontent = "上網找資料",
                QuestionId = "TR02B"
            },
            new Option
            {
                Ocontent = "參考其他小組的解決方案 ",
                QuestionId = "TR02B"
            },
            new Option
            {
                Ocontent = "查看教材",
                QuestionId = "TR02B"
            },
            new Option
            {
                Ocontent = "請教老師或同學",
                QuestionId = "TR02B"
            },
            // TR03 選項
            new Option
            {
                Ocontent = "小組成員一起討論",
                QuestionId = "TR03B"
            },
            new Option
            {
                Ocontent = "合作規劃流程圖",
                QuestionId = "TR03B"
            },
            new Option
            {
                Ocontent = "合作撰寫程式碼",
                QuestionId = "TR03B"
            },


            // PE01 選項
            new Option
            {
                Ocontent = "3",
                QuestionId = "PE01"
            },
            new Option
            {
                Ocontent = "2",
                QuestionId = "PE01"
            },
            new Option
            {
                Ocontent = "1",
                QuestionId = "PE01"
            },
            // PE02 選項
            new Option
            {
                Ocontent = "3",
                QuestionId = "PE02"
            },
            new Option
            {
                Ocontent = "2",
                QuestionId = "PE02"
            },
            new Option
            {
                Ocontent = "1",
                QuestionId = "PE02"
            },
            // PE03 選項
            new Option
            {
                Ocontent = "3",
                QuestionId = "PE03"
            },
            new Option
            {
                Ocontent = "2",
                QuestionId = "PE03"
            },
            new Option
            {
                Ocontent = "1",
                QuestionId = "PE03"
            },
            // PC01 選項
            new Option
            {
                Ocontent = "非常合理",
                QuestionId = "PC01"
            },
            new Option
            {
                Ocontent = "合理",
                QuestionId = "PC01"
            },
            new Option
            {
                Ocontent = "不合理",
                QuestionId = "PC01"
            },
            new Option
            {
                Ocontent = "非常不合理",
                QuestionId = "PC01"
            },
        };

        var k = 1;
        foreach (var option in test_option)
        {
            option.OptionID = k;
            k++;
        }

        modelBuilder.Entity<Option>().HasData(test_option);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
