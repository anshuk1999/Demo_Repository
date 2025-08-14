using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DEMO.Data.Entities;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AdminLogin> AdminLogins { get; set; }

    public virtual DbSet<CategoryName> CategoryNames { get; set; }

    public virtual DbSet<LoanCategory> LoanCategories { get; set; }

    public virtual DbSet<Registration> Registrations { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SubCategoryName> SubCategoryNames { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-RULCTAQ5\\SQLEXPRESS;Database=db_theloanmallm2n;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdminLogin>(entity =>
        {
            entity.ToTable("AdminLogin");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.EmailId).HasMaxLength(100);
            entity.Property(e => e.FirmName).HasMaxLength(100);
            entity.Property(e => e.MobileNumber).HasMaxLength(15);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<CategoryName>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Category__3214EC072E144699");

            entity.ToTable("Category_Name");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Status).HasDefaultValue(true);
        });

        modelBuilder.Entity<LoanCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LoanCate__3214EC07FF1F86ED");

            entity.ToTable("LoanCategory");

            entity.Property(e => e.CategoryName).HasMaxLength(100);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LogoPath).HasMaxLength(255);
            entity.Property(e => e.SubCategoryName).HasMaxLength(100);
        });

        modelBuilder.Entity<Registration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Registra__3214EC073EA39D0C");

            entity.ToTable("Registration");

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.Country).HasMaxLength(100);
            entity.Property(e => e.District).HasMaxLength(100);
            entity.Property(e => e.EmployeeEmail).HasMaxLength(150);
            entity.Property(e => e.EmployeeMobile).HasMaxLength(15);
            entity.Property(e => e.EmployeeName).HasMaxLength(100);
            entity.Property(e => e.EmployeeSalary).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.State).HasMaxLength(100);

            entity.HasOne(d => d.Category).WithMany(p => p.Registrations)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Registration_Category");

            entity.HasOne(d => d.SubCategory).WithMany(p => p.Registrations)
                .HasForeignKey(d => d.SubCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Registration_SubCategory");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__8AFACE1A3821DE1D");

            entity.ToTable("Role");

            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.RoleName).HasMaxLength(100);
        });

        modelBuilder.Entity<SubCategoryName>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SubCateg__3214EC07BB188DD3");

            entity.ToTable("SubCategory_Name");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasDefaultValue(true);
            entity.Property(e => e.SubName).HasMaxLength(100);

            entity.HasOne(d => d.Category).WithMany(p => p.SubCategoryNames)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SubCategory_Category");
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Vehicle__3214EC07E01EC946");

            entity.ToTable("Vehicle");

            entity.Property(e => e.AuthorizedNameAlt).HasMaxLength(100);
            entity.Property(e => e.AuthorizedNamePan).HasMaxLength(100);
            entity.Property(e => e.AuthorizedNameRc).HasMaxLength(100);
            entity.Property(e => e.AuthorizedNumberAlt).HasMaxLength(50);
            entity.Property(e => e.AuthorizedNumberPan).HasMaxLength(50);
            entity.Property(e => e.ChasisNumber).HasMaxLength(100);
            entity.Property(e => e.EngineNumber).HasMaxLength(100);
            entity.Property(e => e.ExpiryDateAlt).HasColumnType("datetime");
            entity.Property(e => e.ExpiryDatePan).HasColumnType("datetime");
            entity.Property(e => e.ExpiryDateRc).HasColumnType("datetime");
            entity.Property(e => e.FileUploadAlt).HasMaxLength(255);
            entity.Property(e => e.FileUploadPan).HasMaxLength(255);
            entity.Property(e => e.FileUploadRc).HasMaxLength(255);
            entity.Property(e => e.Financier).HasMaxLength(100);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Maker).HasMaxLength(100);
            entity.Property(e => e.ManufacturingYear).HasColumnType("datetime");
            entity.Property(e => e.OwnerName).HasMaxLength(100);
            entity.Property(e => e.RcNumber).HasMaxLength(100);
            entity.Property(e => e.RegistrationDate).HasColumnType("datetime");
            entity.Property(e => e.VehicleModel).HasMaxLength(100);
            entity.Property(e => e.VehicleNumber).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
