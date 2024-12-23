﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace HairHarmony_BusinessObject
{
    public partial class HairContext : DbContext
    {
        public HairContext()
        {
        }
        public HairContext(string connectionString)
        {
            this.Database.SetConnectionString(connectionString);
        }

        public HairContext(DbContextOptions<HairContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Appointment> Appointments { get; set; } = null!;
        public virtual DbSet<Feedback> Feedbacks { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Service> Services { get; set; } = null!;
        public virtual DbSet<Shift> Shifts { get; set; } = null!;
        public virtual DbSet<StylistService> StylistServices { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(GetConnectionString());
            }
        }
        private string GetConnectionString()
        {
            IConfiguration config = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", true, true)
                        .Build();
            var strConn = config["ConnectionStrings:DefaultConnectionStringDB"];

            return strConn;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("accounts");

                entity.Property(e => e.AccountId)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("accountID");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Level)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("level");

                entity.Property(e => e.LoyaltyPoints)
                    .HasColumnName("loyaltyPoints")
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Phone)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.Property(e => e.RoleId).HasColumnName("roleID");

                entity.Property(e => e.Salary)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("salary");
            });

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.ToTable("appointments");

                entity.Property(e => e.AppointmentId)
                    .ValueGeneratedNever()
                    .HasColumnName("appointmentID");

                entity.Property(e => e.AppointmentDate)
                    .HasColumnType("datetime")
                    .HasColumnName("appointmentDate");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("customerID");

                entity.Property(e => e.Status)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("fk_appoint_accounts_cus");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.ToTable("feedback");

                entity.Property(e => e.FeedbackId)
                    .ValueGeneratedNever()
                    .HasColumnName("feedbackID");

                entity.Property(e => e.AppointmentId).HasColumnName("appointmentID");

                entity.Property(e => e.Comments)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("comments");

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.Property(e => e.ServiceId).HasColumnName("serviceID");

                entity.Property(e => e.StylistId)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("stylistID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => new { d.ServiceId, d.AppointmentId, d.StylistId })
                    .HasConstraintName("fk_feedback_orders");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => new { e.ServiceId, e.AppointmentId, e.StylistId });

                entity.ToTable("orders");

                entity.Property(e => e.ServiceId).HasColumnName("serviceID");

                entity.Property(e => e.AppointmentId).HasColumnName("appointmentID");

                entity.Property(e => e.StylistId)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("stylistID");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("price");

                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.AppointmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_order_appointment");

                entity.HasOne(d => d.S)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => new { d.StylistId, d.ServiceId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_order_stylist_service");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.ToTable("services");

                entity.Property(e => e.ServiceId)
                    .ValueGeneratedNever()
                    .HasColumnName("serviceID");

                entity.Property(e => e.Duration).HasColumnName("duration");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("price");

                entity.Property(e => e.ServiceName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("serviceName");
            });

            modelBuilder.Entity<Shift>(entity =>
            {
                entity.ToTable("shifts");

                entity.Property(e => e.ShiftId).HasColumnName("shiftID");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.EndTime).HasColumnName("endTime");

                entity.Property(e => e.StartTime).HasColumnName("startTime");

                entity.Property(e => e.StylistId)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("stylistID");

                entity.HasOne(d => d.Stylist)
                    .WithMany(p => p.Shifts)
                    .HasForeignKey(d => d.StylistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__shifts__stylistI__5535A963");
            });

            modelBuilder.Entity<StylistService>(entity =>
            {
                entity.HasKey(e => new { e.StylistId, e.ServiceId })
                    .HasName("pk_sty_service");

                entity.ToTable("stylist_service");

                entity.Property(e => e.StylistId)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("stylistID");

                entity.Property(e => e.ServiceId).HasColumnName("serviceID");

                entity.Property(e => e.CommissionRate).HasColumnName("commissionRate");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.StylistServices)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_service");

                entity.HasOne(d => d.Stylist)
                    .WithMany(p => p.StylistServices)
                    .HasForeignKey(d => d.StylistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_stylist");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
