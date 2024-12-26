using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using University.Entities.Models;

namespace University.DataAccess;

public partial class ApplicationDbContext : DbContext
{

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
   
    public DbSet<CourseViewModel> CourseViewModel { get; set; }
    public DbSet<CourseDetailViewModel> CourseDetailViewModel { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<DepartmentCourse> DepartmentCourses { get; set; }

    public virtual DbSet<Employing> Employings { get; set; }

    public virtual DbSet<Enrollment> Enrollments { get; set; }

    public virtual DbSet<Faculty> Faculties { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<Student> Students { get; set; }
    public virtual DbSet<Teaching> Teachings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=.;Database=UniversityMS;Trusted_Connection=True;TrustServerCertificate=true;MultipleActiveResultSets=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("Course_pk");

            entity.HasIndex(e => e.CourseName, "Course_uq").IsUnique();

            entity.Property(e => e.CourseId)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("Course_ID");
            entity.Property(e => e.CourseName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Course_Name");
            entity.Property(e => e.Description)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.LapHours)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Lap_Hours");
            entity.Property(e => e.Mark)
                .HasMaxLength(3)
                .IsUnicode(false);

            entity.HasMany(d => d.Staff).WithMany(p => p.Courses)
                .UsingEntity<Dictionary<string, object>>(
                    "Teaching",
                    r => r.HasOne<Staff>().WithMany()
                        .HasForeignKey("StaffId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Teach_S_FK"),
                    l => l.HasOne<Course>().WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Teach_C_FK"),
                    j =>
                    {
                        j.HasKey("CourseId", "StaffId").HasName("Teach_PK");
                        j.ToTable("Teaching");
                        j.IndexerProperty<string>("CourseId")
                            .HasMaxLength(6)
                            .IsUnicode(false)
                            .HasColumnName("Course_id");
                        j.IndexerProperty<int>("StaffId").HasColumnName("Staff_id");
                    });
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("Department_pk");

            entity.HasIndex(e => e.DepartmentName, "Department_uq").IsUnique();

            entity.Property(e => e.DepartmentId).HasColumnName("Department_ID");
            entity.Property(e => e.Building)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.DepartmentName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Department_Name");
            entity.Property(e => e.Description)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DepartmentCourse>(entity =>
        {
            entity.HasKey(e => new { e.DepartmentId, e.CourseId }).HasName("DepartmentCourses_pk");

            entity.Property(e => e.DepartmentId).HasColumnName("Department_id");
            entity.Property(e => e.CourseId)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("Course_id");
            entity.Property(e => e.Level)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Semester)
                .HasMaxLength(6)
                .IsUnicode(false);

            entity.HasOne(d => d.Course).WithMany(p => p.DepartmentCourses)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("DepartmentCourses_FK");

            entity.HasOne(d => d.Department).WithMany(p => p.DepartmentCourses)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("DepartmentCourses_D_FK");
        });

        modelBuilder.Entity<Employing>(entity =>
        {
            entity.HasKey(e => new { e.FacultyId, e.DepartmentId, e.EmployeeId }).HasName("Employing_PK");

            entity.ToTable("Employing");

            entity.Property(e => e.FacultyId).HasColumnName("Faculty_id");
            entity.Property(e => e.DepartmentId).HasColumnName("Department_id");
            entity.Property(e => e.EmployeeId).HasColumnName("Employee_id");

            entity.HasOne(d => d.Department).WithMany(p => p.Employings)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Employing_D_FK");

            entity.HasOne(d => d.Employee).WithMany(p => p.Employings)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Employing_S_FK");

            entity.HasOne(d => d.Faculty).WithMany(p => p.Employings)
                .HasForeignKey(d => d.FacultyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Employing_F_FK");
        });

        modelBuilder.Entity<Enrollment>(entity =>
        {
            entity.HasKey(e => new { e.StudentId, e.CourseId }).HasName("Enrollments_pk");

            entity.Property(e => e.StudentId).HasColumnName("Student_id");
            entity.Property(e => e.CourseId)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("Course_id");
            entity.Property(e => e.Grade)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.Course).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Enrollments_C_FK");

            entity.HasOne(d => d.Student).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Enrollments_S_FK");
        });

        modelBuilder.Entity<Faculty>(entity =>
        {
            entity.HasKey(e => e.FacultyId).HasName("Faculty_pk");

            entity.ToTable("Faculty");

            entity.HasIndex(e => e.Email, "Faculty_Email_uq").IsUnique();

            entity.HasIndex(e => e.Location, "Faculty_Location_uq").IsUnique();

            entity.HasIndex(e => e.FacultyName, "Faculty_Name_uq").IsUnique();

            entity.HasIndex(e => e.Phone, "Faculty_phone_uq").IsUnique();

            entity.Property(e => e.FacultyId).HasColumnName("Faculty_ID");
            entity.Property(e => e.Description)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.FacultyName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Faculty_Name");
            entity.Property(e => e.Location).HasMaxLength(50);
            entity.Property(e => e.Phone)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.Type)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("Staff_pk");

            entity.Property(e => e.EmployeeId).HasColumnName("Employee_ID");
            entity.Property(e => e.SSN)
                .HasMaxLength(14)
                .HasColumnName("SSN");
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .HasColumnName("First_Name");
            entity.Property(e => e.LastName)
                .HasMaxLength(20)
                .HasColumnName("Last_Name");
            entity.Property(e => e.Position)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Salary).HasColumnType("decimal(18, 0)");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("Students_PK");

            entity.HasIndex(e => e.Ssn, "Students_SSN").IsUnique();

            entity.HasIndex(e => e.Email, "Students_email_uq").IsUnique();

            entity.HasIndex(e => e.Phone, "Students_phone_uq").IsUnique();

            entity.Property(e => e.StudentId).HasColumnName("Student_ID");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EnrollYear).HasColumnName("enrollYear");
            entity.Property(e => e.FacultyId).HasColumnName("Faculty_id");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("First_Name");
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("Last_Name");
            entity.Property(e => e.Phone)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.Ssn)
                .HasMaxLength(14)
                .IsUnicode(false)
                .HasColumnName("SSN");

            entity.HasOne(d => d.Faculty).WithMany(p => p.Students)
                .HasForeignKey(d => d.FacultyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Students_FK");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
