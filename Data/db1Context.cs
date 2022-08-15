using Microsoft.EntityFrameworkCore;
using SecondVariety.Models;

namespace SecondVariety.Data
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

        public virtual DbSet<Narabotka> Narabotkas { get; set; } = null!;
        public virtual DbSet<SecondVariety.Models.Object> Objects { get; set; } = null!;
        public virtual DbSet<Request> Requests { get; set; } = null!;
        public virtual DbSet<Test> Tests { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                //ConfigurationManager.ConnectionStrings["BloggingDatabase"]

                optionsBuilder.UseNpgsql("Host=rc1b-rl9fvpwjlmvph5d3.mdb.yandexcloud.net:6432;Database=db1;Username=user2;Password=Y!rsgq9itH8P8R4");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Narabotka>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("narabotka");

                entity.Property(e => e.Data).HasColumnName("data");

                entity.Property(e => e.KodObject).HasColumnName("kod_object");

                entity.Property(e => e.Val).HasColumnName("val");
            });

            modelBuilder.Entity<SecondVariety.Models.Object>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("objects");

                entity.Property(e => e.DateFrom).HasColumnName("date_from");

                entity.Property(e => e.Kod).HasColumnName("kod");

                entity.Property(e => e.Koef1).HasColumnName("koef_1");

                entity.Property(e => e.Koef2).HasColumnName("koef_2");

                entity.Property(e => e.LastTo).HasColumnName("last_to");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.NarFrom).HasColumnName("nar_from");

                entity.Property(e => e.NextTo).HasColumnName("next_to");

                entity.Property(e => e.PlanYear).HasColumnName("plan_year");

                entity.Property(e => e.SredNar).HasColumnName("sred_nar");

                entity.Property(e => e.TekNar).HasColumnName("tek_nar");

                entity.Property(e => e.ToNar).HasColumnName("to_nar");

                entity.Property(e => e.ToTime).HasColumnName("to_time");
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("request");

                entity.Property(e => e.Comment).HasColumnName("comment");

                entity.Property(e => e.Data).HasColumnName("data");

                entity.Property(e => e.DateFrom).HasColumnName("date_from");

                entity.Property(e => e.DateFromFakt).HasColumnName("date_from_fakt");

                entity.Property(e => e.DateTo).HasColumnName("date_to");

                entity.Property(e => e.DateToFakt).HasColumnName("date_to_fakt");

                entity.Property(e => e.KodObject).HasColumnName("kod_object");

                entity.Property(e => e.Num).HasColumnName("num");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<Test>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("test");

                entity.Property(e => e.Column1)
                    .HasColumnType("character varying")
                    .HasColumnName("column1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
