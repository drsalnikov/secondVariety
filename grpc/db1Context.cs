using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace grpcserv1
{
    public partial class db1Context : DbContext
    {
        public db1Context()
        {
        }

        public db1Context(DbContextOptions<db1Context> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
        public virtual DbSet<Dependence> Dependences { get; set; } = null!;
        public virtual DbSet<Object> Objects { get; set; } = null!;
        public virtual DbSet<Narabotka> Narabotkas { get; set; } = null!;
        public virtual DbSet<Request> Requests { get; set; } = null!;
        public virtual DbSet<Telemetry> Telemetries { get; set; } = null!;
        public virtual DbSet<TrainingResult> TrainingResults { get; set; } = null!;
        public virtual DbSet<TrainingResult4> TrainingResult4s { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var wbld = WebApplication.CreateBuilder() ; 
                var constr = wbld.Configuration.GetSection("ConnectionStrings:DefaultConnection").Value ;

                optionsBuilder.UseNpgsql(constr,(optbld) => {
                      optbld.CommandTimeout(6000) ;
                      optbld.EnableRetryOnFailure(5) ;    
                 //     optbld.MaxBatchSize(5000000) ; 
                });
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "AspNetUserRole",
                        l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                        r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");

                            j.ToTable("AspNetUserRoles");

                            j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                        });
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Dependence>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("dependence");

                entity.Property(e => e.KodObject).HasColumnName("kod_object");

                entity.Property(e => e.Type1).HasColumnName("type_1");

                entity.Property(e => e.Type2).HasColumnName("type_2");
            });

            modelBuilder.Entity<Object>(entity =>
            {
                entity.ToTable("objects");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.DateFrom).HasColumnName("date_from");

                entity.Property(e => e.ErrorPeriod).HasColumnName("error_period");

                entity.Property(e => e.ErrorRate)
                    .HasColumnName("error_rate")
                    .HasDefaultValueSql("2");

                entity.Property(e => e.Kod).HasColumnName("kod");

                entity.Property(e => e.Koef1).HasColumnName("koef_1");

                entity.Property(e => e.Koef2).HasColumnName("koef_2");

                entity.Property(e => e.LastTo).HasColumnName("last_to");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.NarFrom).HasColumnName("nar_from");

                entity.Property(e => e.NextTo).HasColumnName("next_to");

                entity.Property(e => e.PlanYear).HasColumnName("plan_year");

                entity.Property(e => e.SredNar).HasColumnName("sred_nar");

                entity.Property(e => e.SredNarPlan).HasColumnName("sred_nar_plan");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.TekNar).HasColumnName("tek_nar");

                entity.Property(e => e.ToNar).HasColumnName("to_nar");

                entity.Property(e => e.ToTime).HasColumnName("to_time");

                entity.Property(e => e.TrainingFrom).HasColumnName("training_from");

                entity.Property(e => e.TrainingTo).HasColumnName("training_to");

                entity.Property(e => e.WarningFrom).HasColumnName("warning_from");

                entity.Property(e => e.WarningSensor).HasColumnName("warning_sensor");

                entity.Property(e => e.WarningTime)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("warning_time");

                entity.Property(e => e.WarningType)
                    .HasColumnName("warning_type")
                    .HasDefaultValueSql("0");
            });

            modelBuilder.Entity<Narabotka>(entity =>
            {
                entity.ToTable("opertime");

                entity.HasIndex(e => e.KodObject, "fki_FK_narabotka_objects");

                entity.HasIndex(e => e.KodObject, "fki_object_fk");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(null, null, null, 1000000L, true);

                entity.Property(e => e.Data).HasColumnName("data");

                entity.Property(e => e.KodObject).HasColumnName("kod_object");

                entity.Property(e => e.Val).HasColumnName("val");

                entity.HasOne(d => d.KodObjectNavigation)
                    .WithMany(p => p.Opertimes)
                    .HasForeignKey(d => d.KodObject)
                    .HasConstraintName("FK_narabotka_objects");
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.ToTable("request");

                entity.HasIndex(e => e.KodObject, "fki_FK_request_objects");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Comment).HasColumnName("comment");

                entity.Property(e => e.Data).HasColumnName("data");

                entity.Property(e => e.DateFrom).HasColumnName("date_from");

                entity.Property(e => e.DateFromFakt).HasColumnName("date_from_fakt");

                entity.Property(e => e.DateTo).HasColumnName("date_to");

                entity.Property(e => e.DateToFakt).HasColumnName("date_to_fakt");

                entity.Property(e => e.KodObject).HasColumnName("kod_object");

                entity.Property(e => e.Num).HasColumnName("num");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.KodObjectNavigation)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.KodObject)
                    .HasConstraintName("FK_request_objects");
            });

            modelBuilder.Entity<Telemetry>(entity =>
            {
                entity.ToTable("telemetry");

                entity.HasIndex(e => e.KodObject, "fki_FK_telemetry_objects");

                entity.HasIndex(e => e.KodObject, "fki_fk_object");

                entity.HasIndex(e => e.Id, "telemetry_id_uindex")
                    .IsUnique();

                entity.HasIndex(e => new { e.KodObject, e.Type, e.Period }, "telemetry_kod_object_type_period_index");

                entity.HasIndex(e => new { e.Type, e.Period }, "test_type_period_index");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.KodObject).HasColumnName("kod_object");

                entity.Property(e => e.Period)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("period");

                entity.Property(e => e.Type).HasColumnName("type");

                entity.Property(e => e.Value).HasColumnName("value");

                entity.HasOne(d => d.KodObjectNavigation)
                    .WithMany(p => p.Telemetries)
                    .HasForeignKey(d => d.KodObject)
                    .HasConstraintName("FK_telemetry_objects");
            });

            modelBuilder.Entity<TrainingResult>(entity =>
            {
                entity.ToTable("training_result");

                entity.HasIndex(e => e.KodObject, "fki_FK_training_result_objects");

                entity.HasIndex(e => e.Id, "training_result_id_uindex")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.GradVal).HasColumnName("grad_val");

                entity.Property(e => e.KodObject).HasColumnName("kod_object");

                entity.Property(e => e.MaxVal).HasColumnName("max_val");

                entity.Property(e => e.MedVal).HasColumnName("med_val");

                entity.Property(e => e.MinVal).HasColumnName("min_val");

                entity.Property(e => e.Type).HasColumnName("type");

                entity.HasOne(d => d.KodObjectNavigation)
                    .WithMany(p => p.TrainingResults)
                    .HasForeignKey(d => d.KodObject)
                    .HasConstraintName("FK_training_result_objects");
            });

            modelBuilder.Entity<TrainingResult4>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("training_result_4");

                entity.HasIndex(e => e.KodObject, "training_result_4_kod_object_index");

                entity.Property(e => e.KodObject).HasColumnName("kod_object");

                entity.Property(e => e.Type1).HasColumnName("type_1");

                entity.Property(e => e.Type1Value).HasColumnName("type_1_value");

                entity.Property(e => e.Type2).HasColumnName("type_2");

                entity.Property(e => e.Type2Max).HasColumnName("type_2_max");

                entity.Property(e => e.Type2Min).HasColumnName("type_2_min");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
