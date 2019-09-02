using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Dta.Marketplace.Subscribers.Email.Logger.Worker {
    public partial class EmailLoggerContext : DbContext {
        public EmailLoggerContext() {
        }

        public EmailLoggerContext(DbContextOptions<EmailLoggerContext> options)
            : base(options) {
        }

        public virtual DbSet<EmailLogging> EmailLogging { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder.UseNpgsql("Host=localhost;Port=15432;Database=emaillogger;Username=postgres;Password=password");
            }
        }
    }
}
