using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace eisync_api.Models;

public partial class EisyncDbContext : DbContext
{
    public EisyncDbContext()
    {
    }

    public EisyncDbContext(DbContextOptions<EisyncDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appliance> Appliances { get; set; }

    public virtual DbSet<ApplianceGoal> ApplianceGoals { get; set; }

    public virtual DbSet<CostEstimation> CostEstimations { get; set; }

    public virtual DbSet<EnergyRate> EnergyRates { get; set; }

    public virtual DbSet<Goal> Goals { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=NISILA;Initial Catalog=eisync_db;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appliance>(entity =>
        {
            entity.ToTable("appliance");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("id");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("datetime")
                .HasColumnName("createdOn");
            entity.Property(e => e.DeviceBrand)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("deviceBrand");
            entity.Property(e => e.DeviceModel)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("deviceModel");
            entity.Property(e => e.DeviceType)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("deviceType");
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.OnHours).HasColumnName("onHours");
            entity.Property(e => e.Power).HasColumnName("power");
            entity.Property(e => e.UserId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("userId");
            entity.Property(e => e.Voltage).HasColumnName("voltage");

            entity.HasOne(d => d.User).WithMany(p => p.Appliances)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_appliance_user");
        });

        modelBuilder.Entity<ApplianceGoal>(entity =>
        {
            entity.HasKey(e => new { e.GoalId, e.ApplianceId });

            entity.ToTable("applianceGoal");

            entity.Property(e => e.GoalId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("goalId");
            entity.Property(e => e.ApplianceId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("applianceId");
            entity.Property(e => e.OffTime).HasColumnName("offTime");
            entity.Property(e => e.OnHours).HasColumnName("onHours");
            entity.Property(e => e.OnTime).HasColumnName("onTime");

            entity.HasOne(d => d.Appliance).WithMany(p => p.ApplianceGoals)
                .HasForeignKey(d => d.ApplianceId)
                .HasConstraintName("FK_applianceGoal_appliance");

            entity.HasOne(d => d.Goal).WithMany(p => p.ApplianceGoals)
                .HasForeignKey(d => d.GoalId)
                .HasConstraintName("FK_applianceGoal_goal");
        });

        modelBuilder.Entity<CostEstimation>(entity =>
        {
            entity.ToTable("costEstimation");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("id");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("createdDate");
            entity.Property(e => e.CurrencyType)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("currencyType");
            entity.Property(e => e.EstimatedCost).HasColumnName("estimatedCost");
            entity.Property(e => e.FromDate)
                .HasColumnType("datetime")
                .HasColumnName("fromDate");
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.ToDate)
                .HasColumnType("datetime")
                .HasColumnName("toDate");
            entity.Property(e => e.UserId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.CostEstimations)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_costEstimation_user");

            entity.HasMany(d => d.Appliances).WithMany(p => p.CostEstimations)
                .UsingEntity<Dictionary<string, object>>(
                    "CostEstimationAppliance",
                    r => r.HasOne<Appliance>().WithMany()
                        .HasForeignKey("ApplianceId")
                        .HasConstraintName("FK_costEstimationAppliance_appliance"),
                    l => l.HasOne<CostEstimation>().WithMany()
                        .HasForeignKey("CostEstimationId")
                        .HasConstraintName("FK_costEstimationAppliance_costEstimation"),
                    j =>
                    {
                        j.HasKey("CostEstimationId", "ApplianceId");
                        j.ToTable("costEstimationAppliance");
                    });
        });

        modelBuilder.Entity<EnergyRate>(entity =>
        {
            entity.ToTable("energyRates");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("id");
            entity.Property(e => e.Charge).HasColumnName("charge");
            entity.Property(e => e.CostEstimationId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("costEstimationId");
            entity.Property(e => e.FixedCharge).HasColumnName("fixedCharge");
            entity.Property(e => e.FromUnit).HasColumnName("fromUnit");
            entity.Property(e => e.Order).HasColumnName("order");
            entity.Property(e => e.ToUnit).HasColumnName("toUnit");

            entity.HasOne(d => d.CostEstimation).WithMany(p => p.EnergyRates)
                .HasForeignKey(d => d.CostEstimationId)
                .HasConstraintName("FK_energyRates_costEstimation");
        });

        modelBuilder.Entity<Goal>(entity =>
        {
            entity.ToTable("goal");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("id");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("endDate");
            entity.Property(e => e.GoalAmount).HasColumnName("goalAmount");
            entity.Property(e => e.GoalName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("goalName");
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("startDate");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.UserId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.Goals)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_goal_user");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("id");
            entity.Property(e => e.Country)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("country");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("datetime")
                .HasColumnName("createdOn");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.LoginPassword)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("loginPassword");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("phoneNumber");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
