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

    public virtual DbSet<LoanCategory> LoanCategories { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<UserTable> UserTables { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-IQ0K3E5;Database=db_theloanmallm2n;Trusted_Connection=True;TrustServerCertificate=True;");

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

        modelBuilder.Entity<LoanCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LoanCate__3214EC07FF1F86ED");

            entity.Property(e => e.CategoryName).HasMaxLength(100);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LogoPath).HasMaxLength(255);
            entity.Property(e => e.SubCategoryName).HasMaxLength(100);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__8AFACE1A99F80374");

            entity.ToTable("Role");

            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.RoleName).HasMaxLength(100);
        });

        modelBuilder.Entity<UserTable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserTabl__3214EC27DAFD2A29");

            entity.ToTable("UserTable");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AadharBack)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.AadharFront)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.AccountHolderName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.AccountNumber)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.BankName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.BranchName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CurrentAddress)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CurrentCity)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CurrentDistrict)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CurrentPincode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CurrentState)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EmailId)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Ifsccode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("IFSCCode");
            entity.Property(e => e.MemberId)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.MobileNumber)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PanCard)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Passbook)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PermanentAddress)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PermanentCity)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PermanentDistrict)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PermanentPincode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.PermanentState)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Photo)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Vehicles__3214EC07A566E403");

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
