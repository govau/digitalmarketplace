using System;

using Microsoft.Extensions.Options;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dta.Marketplace.Subscribers.Logger.Worker
{
    public partial class LoggerContext : DbContext, ILoggerContext
    {

        public LoggerContext(DbContextOptions<LoggerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<LogEntry> LogEntry { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured){
                optionsBuilder.UseNpgsql("Host=localhost;Port=15432;Database=logger;Username=postgres;Password=password");
            }
        }

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

        //     modelBuilder.Entity<LogEntry>(entity =>
        //     {
        //         entity.ToTable("log_entry");

        //         entity.Property(e => e.Id)
        //             .HasColumnName("id")
        //             .HasDefaultValueSql("nextval('log_entry_seq'::regclass)");

        //         entity.Property(e => e.CreatedAt)
        //             .HasColumnName("created_at")
        //             .HasColumnType("time without time zone")
        //             .HasDefaultValueSql("now()");

        //         entity.Property(e => e.Data)
        //             .HasColumnName("data")
        //             .HasColumnType("json");
        //     });

        //     modelBuilder.HasSequence("log_entry_seq");
        // }
    }
}