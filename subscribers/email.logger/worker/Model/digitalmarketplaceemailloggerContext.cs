using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Dta.Marketplace.Subscribers.Email.Logger.Worker
{
    public partial class digitalmarketplaceemailloggerContext : DbContext
    {
        public digitalmarketplaceemailloggerContext()
        {
        }

        public digitalmarketplaceemailloggerContext(DbContextOptions<digitalmarketplaceemailloggerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<EmailLogging> EmailLogging { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=digitalmarketplaceemaillogger;Username=postgres;Password=password");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<EmailLogging>(entity =>
            {
                entity.ToTable("email_logging");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Data)
                    .HasColumnName("data")
                    .HasColumnType("json");

                entity.Property(e => e.Message_Id)
                    .HasColumnName("message_id");
                    
                entity.Property(e => e.Notification_Type)
                    .HasColumnName("notification_type")
                    .HasColumnType("character varying");

                entity.Property(e => e.DateTimeSent)
                    .HasColumnName("date_time_sent");
                    
                entity.Property(e => e.Subject)
                    .HasColumnName("subject")
                    .HasColumnType("character varying");
                    
            });
        }
    }
}