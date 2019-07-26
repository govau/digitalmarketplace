using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Configuration;
using Microsoft.Extensions.DependencyInjection;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dta.Marketplace.Subscribers.Logger.Worker
{
    public partial class LoggerContext : DbContext, ILoggerContext
    {
        private DbSet<LogEntry> logEntry;

        public LoggerContext()
        {
        }

        public LoggerContext(DbContextOptions<LoggerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<LogEntry> LogEntry { get => logEntry; set => logEntry = value; }

        //public virtual DbSet<LogEntry> LogEntry { get; set; } the above is full property 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Host=localhost;Port=15432;Database=logger;Username=postgres;Password=password");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<LogEntry>(entity =>
            {
                entity.ToTable("log_entry");

                entity.ForNpgsqlHasComment("Will eventually need to make this bigInt ");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('log_entry_seq'::regclass)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("time without time zone")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Data)
                    .IsRequired()
                    .HasColumnType("json");
            });

            modelBuilder.HasSequence("log_entry_seq");
        }
    }
}
