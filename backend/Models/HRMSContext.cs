using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace backend.Models
{
    public partial class HRMSContext : DbContext
    {
        public HRMSContext()
        {
        }

        public HRMSContext(DbContextOptions<HRMSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<EditTimesheet> EditTimesheet { get; set; }
        public virtual DbSet<TimeSheet> TimeSheet { get; set; }
        public virtual DbSet<TimeSheetEntry> TimeSheetEntry { get; set; }
        public virtual DbSet<TimeSheetItem> TimeSheetItem { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=CYG365;Database=HRMS;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EditTimesheet>(entity =>
            {
                entity.HasKey(e => e.EmpId);

                entity.Property(e => e.EmpId)
                    .HasColumnName("emp_Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Company)
                    .HasColumnName("company")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Customer)
                    .HasColumnName("customer")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Project)
                    .HasColumnName("project")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Task)
                    .HasColumnName("task")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TimeSheet>(entity =>
            {
                entity.HasKey(e => e.EmpId);

                entity.ToTable("timeSheet");

                entity.Property(e => e.EmpId)
                    .HasColumnName("emp_Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.EmployeeName)
                    .HasColumnName("employee_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TimeSheetEntry>(entity =>
            {
                entity.HasKey(e => e.EmpId);

                entity.ToTable("timeSheetEntry");

                entity.Property(e => e.EmpId)
                    .HasColumnName("emp_Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Company)
                    .HasColumnName("company")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Customer)
                    .HasColumnName("customer")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeName)
                    .HasColumnName("employee_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Project)
                    .HasColumnName("project")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Task)
                    .HasColumnName("task")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Emp)
                    .WithOne(p => p.TimeSheetEntry)
                    .HasForeignKey<TimeSheetEntry>(d => d.EmpId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_timeSheetEntry_timeSheet");
            });

            modelBuilder.Entity<TimeSheetItem>(entity =>
            {
                entity.HasKey(e => e.EmpId);

                entity.ToTable("timeSheetItem");

                entity.Property(e => e.EmpId)
                    .HasColumnName("emp_Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Day)
                    .HasColumnName("day")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.From)
                    .HasColumnName("from")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Hours)
                    .HasColumnName("hours")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.To)
                    .HasColumnName("to")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Emp)
                    .WithOne(p => p.TimeSheetItem)
                    .HasForeignKey<TimeSheetItem>(d => d.EmpId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_timeSheetItem_timeSheet");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.EmailId)
                    .IsRequired()
                    .HasColumnName("email_id")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });
        }
    }
}
