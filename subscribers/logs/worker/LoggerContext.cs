using System;

using Microsoft.Extensions.Options;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dta.Marketplace.Subscribers.Logger.Worker {
    public partial class LoggerContext : DbContext {
        public LoggerContext() {
        }

        public LoggerContext(DbContextOptions<LoggerContext> options)
            : base(options) {
        }

        public virtual DbSet<LogEntry> LogEntry { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder.UseNpgsql("Host=localhost;Port=15432;Database=logger;Username=postgres;Password=password");
            }
        }
    }
}