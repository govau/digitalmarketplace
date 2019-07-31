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
        private readonly IOptions<AppConfig> _config;

        public LoggerContext(IOptions<AppConfig> config)
        {
            _config = config;
        }

        public LoggerContext(DbContextOptions<LoggerContext> options, IOptions<AppConfig> config)
            : base(options)
        {
            _config = config;
        }

        public virtual DbSet<LogEntry> LogEntry { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured){
                optionsBuilder.UseNpgsql($"Host={_config.Value.DbHost};Port={_config.Value.DbPort};Database={_config.Value.DbName};Username={_config.Value.DbUsername};Password={_config.Value.DbPassword}");
            }
    
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<LogEntry>(entity =>
            {
                entity.ToTable("log_entry");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('log_entry_seq'::regclass)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("time without time zone")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Data)
                    .HasColumnName("data")
                    .HasColumnType("json");
            });

            modelBuilder.HasSequence("log_entry_seq");
        }
    }
}