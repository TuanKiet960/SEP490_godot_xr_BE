using System;
using System.Collections.Generic;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DBContext;

public partial class GodotxrDbContext : DbContext
{
    public GodotxrDbContext()
    {
    }

    public GodotxrDbContext(DbContextOptions<GodotxrDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Analysis> Analyses { get; set; }

    public virtual DbSet<Children> Childrens { get; set; }

    public virtual DbSet<Classroom> Classrooms { get; set; }

    public virtual DbSet<Enrollment> Enrollments { get; set; }

    public virtual DbSet<Exercise> Exercises { get; set; }

    public virtual DbSet<ExerciseType> ExerciseTypes { get; set; }

    public virtual DbSet<Lesson> Lessons { get; set; }

    public virtual DbSet<Program> Programs { get; set; }

    public virtual DbSet<Result> Results { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server =LAPTOP-UIGSU8B4\\MSSQLSEVER; database =godotxr_db;uid=sa;pwd=Kiet960@;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Analysis>(entity =>
        {
            entity.HasKey(e => e.AnalysisId).HasName("PK__Analyses__5B789DC896A545D9");

            entity.Property(e => e.AverageScore).HasDefaultValue(0.0);
            entity.Property(e => e.CompletedSessions).HasDefaultValue(0);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.LastAnalysisAt).HasColumnType("datetime");
            entity.Property(e => e.ProgressLevel)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Recommendation).HasColumnType("text");
            entity.Property(e => e.Strengths).HasColumnType("text");
            entity.Property(e => e.TotalPracticeTime).HasDefaultValue(0);
            entity.Property(e => e.TotalSessions).HasDefaultValue(0);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Weaknesses).HasColumnType("text");

            entity.HasOne(d => d.Child).WithMany(p => p.Analyses)
                .HasForeignKey(d => d.ChildId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Analyses__ChildI__4D94879B");
        });

        modelBuilder.Entity<Children>(entity =>
        {
            entity.HasKey(e => e.ChildId).HasName("PK__Children__BEFA071653CD0E09");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FullName)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.LearningLevel)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Note).HasColumnType("text");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("active");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.Children)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Childrens__UserI__44FF419A");
        });

        modelBuilder.Entity<Classroom>(entity =>
        {
            entity.HasKey(e => e.ClassId).HasName("PK__Classroo__CB1927C04F368654");

            entity.Property(e => e.ClassName)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("active");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Program).WithMany(p => p.Classrooms)
                .HasForeignKey(d => d.ProgramId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Classroom__Progr__59063A47");

            entity.HasOne(d => d.User).WithMany(p => p.Classrooms)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Classroom__UserI__5812160E");
        });

        modelBuilder.Entity<Enrollment>(entity =>
        {
            entity.HasKey(e => e.EnrollmentId).HasName("PK__Enrollme__7F68771B06086FF0");

            entity.HasIndex(e => e.EnrollmentCode, "UQ__Enrollme__670CB47EEE5F58AE").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EnrollmentCode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("active");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Child).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.ChildId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Enrollmen__Child__5FB337D6");

            entity.HasOne(d => d.Class).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Enrollmen__Class__60A75C0F");
        });

        modelBuilder.Entity<Exercise>(entity =>
        {
            entity.HasKey(e => e.ExerciseId).HasName("PK__Exercise__A074AD2FAF250229");

            entity.Property(e => e.AudioPromptUrl)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.ContentHtml).HasColumnType("text");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DifficultyLevel)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ExerciseName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Instruction).HasColumnType("text");
            entity.Property(e => e.Language)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("active");
            entity.Property(e => e.TargetSkill)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.VisualPromptUrl)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.Lesson).WithMany(p => p.Exercises)
                .HasForeignKey(d => d.LessonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Exercises__Lesso__70DDC3D8");

            entity.HasOne(d => d.Type).WithMany(p => p.Exercises)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Exercises__TypeI__71D1E811");
        });

        modelBuilder.Entity<ExerciseType>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PK__Exercise__516F03B55C216474");

            entity.ToTable("Exercise_Types");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.TypeName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Lesson>(entity =>
        {
            entity.HasKey(e => e.LessonId).HasName("PK__Lessons__B084ACD04C36FECB");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.LessonName)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("active");
            entity.Property(e => e.TargetSkill)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Program).WithMany(p => p.Lessons)
                .HasForeignKey(d => d.ProgramId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Lessons__Program__66603565");
        });

        modelBuilder.Entity<Program>(entity =>
        {
            entity.HasKey(e => e.ProgramId).HasName("PK__Programs__75256058794B234F");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Language)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ProgramName)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("active");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Result>(entity =>
        {
            entity.HasKey(e => e.ResultId).HasName("PK__Results__976902085B05A194");

            entity.Property(e => e.AttemptNumber).HasDefaultValue(1);
            entity.Property(e => e.AudioResultUrl)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.CompletedAt).HasColumnType("datetime");
            entity.Property(e => e.CompletionStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DisplayDataUrl)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.FeedbackText).HasColumnType("text");
            entity.Property(e => e.InteractionLog).HasColumnType("text");
            entity.Property(e => e.StartedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Child).WithMany(p => p.Results)
                .HasForeignKey(d => d.ChildId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Results__ChildId__76969D2E");

            entity.HasOne(d => d.Exercise).WithMany(p => p.Results)
                .HasForeignKey(d => d.ExerciseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Results__Exercis__778AC167");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1A9BADE1B7");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.RoleName)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C94A5DD16");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D1053449D2821D").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.FullName)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("active");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__RoleId__3F466844");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
