using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Dta.Marketplace.Api.Entities {
    public partial class DigitalMarketplaceContext : DbContext {
        static DigitalMarketplaceContext() {
            Npgsql.NpgsqlConnection.GlobalTypeMapper.MapEnum<ApplicationStatus>();
            Npgsql.NpgsqlConnection.GlobalTypeMapper.MapEnum<ApplicationType>();
            Npgsql.NpgsqlConnection.GlobalTypeMapper.MapEnum<BodyType>();
            Npgsql.NpgsqlConnection.GlobalTypeMapper.MapEnum<CaseStudyAssessmentStatus>();
            Npgsql.NpgsqlConnection.GlobalTypeMapper.MapEnum<CaseStudyStatus>();
            Npgsql.NpgsqlConnection.GlobalTypeMapper.MapEnum<EvidenceAssessmentStatus>();
            Npgsql.NpgsqlConnection.GlobalTypeMapper.MapEnum<PermissionType>();
            Npgsql.NpgsqlConnection.GlobalTypeMapper.MapEnum<ProjectStatus>();
            Npgsql.NpgsqlConnection.GlobalTypeMapper.MapEnum<SupplierDomainPriceStatus>();
            Npgsql.NpgsqlConnection.GlobalTypeMapper.MapEnum<SupplierDomainStatus>();
            Npgsql.NpgsqlConnection.GlobalTypeMapper.MapEnum<SupplierStatus>();
            Npgsql.NpgsqlConnection.GlobalTypeMapper.MapEnum<TeamStatus>();
            Npgsql.NpgsqlConnection.GlobalTypeMapper.MapEnum<UserClaimType>();
            Npgsql.NpgsqlConnection.GlobalTypeMapper.MapEnum<UserRole>();
        }

        public DigitalMarketplaceContext() {
        }

        public DigitalMarketplaceContext(DbContextOptions<DigitalMarketplaceContext> options)
            : base(options) {
        }

        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<Agency> Agency { get; set; }
        public virtual DbSet<AgencyDomain> AgencyDomain { get; set; }
        public virtual DbSet<ApiKey> ApiKey { get; set; }
        public virtual DbSet<Application> Application { get; set; }
        public virtual DbSet<ArchivedService> ArchivedService { get; set; }
        public virtual DbSet<Assessment> Assessment { get; set; }
        public virtual DbSet<AuditEvent> AuditEvent { get; set; }
        public virtual DbSet<Brief> Brief { get; set; }
        public virtual DbSet<BriefAssessment> BriefAssessment { get; set; }
        public virtual DbSet<BriefAssessor> BriefAssessor { get; set; }
        public virtual DbSet<BriefClarificationQuestion> BriefClarificationQuestion { get; set; }
        public virtual DbSet<BriefHistory> BriefHistory { get; set; }
        public virtual DbSet<BriefQuestion> BriefQuestion { get; set; }
        public virtual DbSet<BriefResponse> BriefResponse { get; set; }
        public virtual DbSet<BriefResponseContact> BriefResponseContact { get; set; }
        public virtual DbSet<BriefResponseDownload> BriefResponseDownload { get; set; }
        public virtual DbSet<BriefUser> BriefUser { get; set; }
        public virtual DbSet<CaseStudy> CaseStudy { get; set; }
        public virtual DbSet<CaseStudyAssessment> CaseStudyAssessment { get; set; }
        public virtual DbSet<CaseStudyAssessmentDomainCriteria> CaseStudyAssessmentDomainCriteria { get; set; }
        public virtual DbSet<Contact> Contact { get; set; }
        public virtual DbSet<Council> Council { get; set; }
        public virtual DbSet<Domain> Domain { get; set; }
        public virtual DbSet<DomainCriteria> DomainCriteria { get; set; }
        public virtual DbSet<DraftService> DraftService { get; set; }
        public virtual DbSet<Evidence> Evidence { get; set; }
        public virtual DbSet<EvidenceAssessment> EvidenceAssessment { get; set; }
        public virtual DbSet<Framework> Framework { get; set; }
        public virtual DbSet<FrameworkLot> FrameworkLot { get; set; }
        public virtual DbSet<Insight> Insight { get; set; }
        public virtual DbSet<KeyValue> KeyValue { get; set; }
        public virtual DbSet<Lot> Lot { get; set; }
        public virtual DbSet<MasterAgreement> MasterAgreement { get; set; }
        public virtual DbSet<PriceSchedule> PriceSchedule { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<RecruiterInfo> RecruiterInfo { get; set; }
        public virtual DbSet<Service> Service { get; set; }
        public virtual DbSet<ServiceCategory> ServiceCategory { get; set; }
        public virtual DbSet<ServiceRole> ServiceRole { get; set; }
        public virtual DbSet<ServiceType> ServiceType { get; set; }
        public virtual DbSet<SignedAgreement> SignedAgreement { get; set; }
        public virtual DbSet<Supplier> Supplier { get; set; }
        public virtual DbSet<SupplierContact> SupplierContact { get; set; }
        public virtual DbSet<SupplierDomain> SupplierDomain { get; set; }
        public virtual DbSet<SupplierExtraLinks> SupplierExtraLinks { get; set; }
        public virtual DbSet<SupplierFramework> SupplierFramework { get; set; }
        public virtual DbSet<SupplierReference> SupplierReference { get; set; }
        public virtual DbSet<SupplierUserInviteLog> SupplierUserInviteLog { get; set; }
        public virtual DbSet<Team> Team { get; set; }
        public virtual DbSet<TeamBrief> TeamBrief { get; set; }
        public virtual DbSet<TeamMember> TeamMember { get; set; }
        public virtual DbSet<TeamMemberPermission> TeamMemberPermission { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserClaim> UserClaim { get; set; }
        public virtual DbSet<UserFramework> UserFramework { get; set; }
        public virtual DbSet<Vuser> Vuser { get; set; }
        public virtual DbSet<WebsiteLink> WebsiteLink { get; set; }
        public virtual DbSet<WorkOrder> WorkOrder { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder.UseNpgsql(System.Environment.GetEnvironmentVariable("ConnectionString"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder
                .HasPostgresEnum(null, "application_status_enum", Enum.GetNames(typeof(ApplicationStatus)))
                .HasPostgresEnum(null, "application_type_enum", Enum.GetNames(typeof(ApplicationType)))
                .HasPostgresEnum(null, "body_type_enum", Enum.GetNames(typeof(BodyType)))
                .HasPostgresEnum(null, "case_study_assessment_status_enum", Enum.GetNames(typeof(CaseStudyAssessmentStatus)))
                .HasPostgresEnum(null, "case_study_status_enum", Enum.GetNames(typeof(CaseStudyStatus)))
                .HasPostgresEnum(null, "evidence_assessment_status_enum", Enum.GetNames(typeof(EvidenceAssessmentStatus)))
                .HasPostgresEnum(null, "permission_type_enum", Enum.GetNames(typeof(PermissionType)))
                .HasPostgresEnum(null, "project_status_enum", Enum.GetNames(typeof(ProjectStatus)))
                .HasPostgresEnum(null, "supplier_domain_price_status_enum", Enum.GetNames(typeof(SupplierDomainPriceStatus)))
                .HasPostgresEnum(null, "supplier_domain_status_enum", Enum.GetNames(typeof(SupplierDomainStatus)))
                .HasPostgresEnum(null, "supplier_status_enum", Enum.GetNames(typeof(SupplierStatus)))
                .HasPostgresEnum(null, "team_status_enum", Enum.GetNames(typeof(TeamStatus)))
                .HasPostgresEnum(null, "user_claim_type_enum", Enum.GetNames(typeof(UserClaimType)))
                .HasPostgresEnum(null, "user_roles_enum", Enum.GetNames(typeof(UserRole)))
                .HasPostgresExtension("pg_trgm");

            modelBuilder.Entity<Address>(entity => {
                entity.HasOne(d => d.SupplierCodeNavigation)
                    .WithMany(p => p.Address)
                    .HasPrincipalKey(p => p.Code)
                    .HasForeignKey(d => d.SupplierCode)
                    .HasConstraintName("address_supplier_code_fkey");
            });

            modelBuilder.Entity<Agency>(entity => {
                entity.HasIndex(e => e.Domain)
                    .HasName("ix_agency_domain")
                    .IsUnique();

                entity.Property(e => e.Whitelisted).HasDefaultValueSql("true");
            });

            modelBuilder.Entity<AgencyDomain>(entity => {
                entity.HasIndex(e => e.Domain)
                    .HasName("ix_agency_domain_domain")
                    .IsUnique();

                entity.HasOne(d => d.Agency)
                    .WithMany(p => p.AgencyDomain)
                    .HasForeignKey(d => d.AgencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("agency_domain_agency_id_fkey");
            });

            modelBuilder.Entity<ApiKey>(entity => {
                entity.HasIndex(e => e.CreatedAt)
                    .HasName("ix_api_key_created_at");

                entity.HasIndex(e => e.Key)
                    .HasName("ix_api_key_key")
                    .IsUnique();

                entity.HasIndex(e => e.RevokedAt)
                    .HasName("ix_api_key_revoked_at");

                entity.HasIndex(e => e.UserId)
                    .HasName("ix_api_key_user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ApiKey)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("api_key_user_id_fkey");
            });

            modelBuilder.Entity<Application>(entity => {
                entity.HasIndex(e => e.CreatedAt)
                    .HasName("ix_application_created_at");

                entity.HasIndex(e => e.UpdatedAt)
                    .HasName("ix_application_updated_at");

                entity.HasIndex(e => e.Status)
                    .HasName("ix_application_status");

                entity.HasIndex(e => e.Type)
                    .HasName("ix_application_type");

                entity.HasOne(d => d.SupplierCodeNavigation)
                    .WithMany(p => p.Application)
                    .HasPrincipalKey(p => p.Code)
                    .HasForeignKey(d => d.SupplierCode)
                    .HasConstraintName("application_supplier_code_fkey");
            });

            modelBuilder.Entity<ArchivedService>(entity => {
                entity.HasIndex(e => e.FrameworkId)
                    .HasName("ix_archived_service_framework_id");

                entity.HasIndex(e => e.LotId)
                    .HasName("ix_archived_service_lot_id");

                entity.HasIndex(e => e.ServiceId)
                    .HasName("ix_archived_service_service_id");

                entity.HasIndex(e => e.SupplierCode)
                    .HasName("ix_archived_service_supplier_code");

                entity.HasOne(d => d.Framework)
                    .WithMany(p => p.ArchivedService)
                    .HasForeignKey(d => d.FrameworkId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("archived_service_framework_id_fkey1");

                entity.HasOne(d => d.Lot)
                    .WithMany(p => p.ArchivedService)
                    .HasForeignKey(d => d.LotId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("archived_service_lot_id_fkey");

                entity.HasOne(d => d.SupplierCodeNavigation)
                    .WithMany(p => p.ArchivedService)
                    .HasPrincipalKey(p => p.Code)
                    .HasForeignKey(d => d.SupplierCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("archived_service_supplier_code_fkey");

                entity.HasOne(d => d.FrameworkLot)
                    .WithMany(p => p.ArchivedService)
                    .HasForeignKey(d => new { d.FrameworkId, d.LotId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("archived_service_framework_id_fkey");
            });

            modelBuilder.Entity<Assessment>(entity => {
                entity.HasIndex(e => e.CreatedAt)
                    .HasName("ix_assessment_created_at");

                entity.HasOne(d => d.SupplierDomain)
                    .WithMany(p => p.Assessment)
                    .HasForeignKey(d => d.SupplierDomainId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("assessment_supplier_domain_id_fkey");
            });

            modelBuilder.Entity<AuditEvent>(entity => {
                entity.HasIndex(e => e.Acknowledged)
                    .HasName("ix_audit_event_acknowledged");

                entity.HasIndex(e => e.CreatedAt)
                    .HasName("ix_audit_event_created_at");

                entity.HasIndex(e => e.Type)
                    .HasName("ix_audit_event_type");

                entity.HasIndex(e => new { e.Type, e.Acknowledged })
                    .HasName("idx_audit_events_type_acknowledged");

                entity.HasIndex(e => new { e.ObjectType, e.ObjectId, e.Type, e.CreatedAt })
                    .HasName("idx_audit_events_object_and_type");
            });

            modelBuilder.Entity<Brief>(entity => {
                entity.HasIndex(e => e.ClosedAt)
                    .HasName("ix_brief_closed_at");

                entity.HasIndex(e => e.CreatedAt)
                    .HasName("ix_brief_created_at");

                entity.HasIndex(e => e.PublishedAt)
                    .HasName("ix_brief_published_at");

                entity.HasIndex(e => e.QuestionsClosedAt)
                    .HasName("ix_brief_questions_closed_at");

                entity.HasIndex(e => e.UpdatedAt)
                    .HasName("ix_brief_updated_at");

                entity.HasIndex(e => e.WithdrawnAt)
                    .HasName("ix_brief_withdrawn_at");

                entity.HasOne(d => d.Domain)
                    .WithMany(p => p.Brief)
                    .HasForeignKey(d => d.DomainId)
                    .HasConstraintName("brief_domain_id_fkey");

                entity.HasOne(d => d.Framework)
                    .WithMany(p => p.Brief)
                    .HasForeignKey(d => d.FrameworkId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("brief_framework_id_fkey1");

                entity.HasOne(d => d.Lot)
                    .WithMany(p => p.Brief)
                    .HasForeignKey(d => d.LotId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("brief_lot_id_fkey");

                entity.HasOne(d => d.FrameworkLot)
                    .WithMany(p => p.Brief)
                    .HasForeignKey(d => new { d.FrameworkId, d.LotId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("brief_framework_id_fkey");
            });

            modelBuilder.Entity<BriefAssessment>(entity => {
                entity.HasKey(e => new { e.BriefId, e.AssessmentId })
                    .HasName("brief_assessment_pkey");

                entity.HasOne(d => d.Assessment)
                    .WithMany(p => p.BriefAssessment)
                    .HasForeignKey(d => d.AssessmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("brief_assessment_assessment_id_fkey");

                entity.HasOne(d => d.Brief)
                    .WithMany(p => p.BriefAssessment)
                    .HasForeignKey(d => d.BriefId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("brief_assessment_brief_id_fkey");
            });

            modelBuilder.Entity<BriefAssessor>(entity => {
                entity.HasOne(d => d.Brief)
                    .WithMany(p => p.BriefAssessor)
                    .HasForeignKey(d => d.BriefId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("brief_assessor_brief_id_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.BriefAssessor)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("brief_assessor_user_id_fkey");
            });

            modelBuilder.Entity<BriefClarificationQuestion>(entity => {
                entity.HasIndex(e => e.PublishedAt)
                    .HasName("ix_brief_clarification_question_published_at");

                entity.HasOne(d => d.Brief)
                    .WithMany(p => p.BriefClarificationQuestion)
                    .HasForeignKey(d => d.BriefId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("brief_clarification_question_brief_id_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.BriefClarificationQuestion)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("brief_clarification_question_user_id_fkey");
            });

            modelBuilder.Entity<BriefHistory>(entity => {
                entity.HasIndex(e => e.BriefId)
                    .HasName("ix_brief_history_brief_id");

                entity.HasIndex(e => e.EditedAt)
                    .HasName("ix_brief_history_edited_at");

                entity.HasIndex(e => e.UserId)
                    .HasName("ix_brief_history_user_id");

                entity.HasOne(d => d.Brief)
                    .WithMany(p => p.BriefHistory)
                    .HasForeignKey(d => d.BriefId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("brief_history_brief_id_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.BriefHistory)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("brief_history_user_id_fkey");
            });

            modelBuilder.Entity<BriefQuestion>(entity => {
                entity.HasIndex(e => e.CreatedAt)
                    .HasName("ix_brief_question_created_at");

                entity.HasOne(d => d.Brief)
                    .WithMany(p => p.BriefQuestion)
                    .HasForeignKey(d => d.BriefId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("brief_question_brief_id_fkey");

                entity.HasOne(d => d.SupplierCodeNavigation)
                    .WithMany(p => p.BriefQuestion)
                    .HasPrincipalKey(p => p.Code)
                    .HasForeignKey(d => d.SupplierCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("brief_question_supplier_code_fkey");
            });

            modelBuilder.Entity<BriefResponse>(entity => {
                entity.HasIndex(e => e.CreatedAt)
                    .HasName("ix_brief_response_created_at");

                entity.HasIndex(e => e.SubmittedAt)
                    .HasName("ix_brief_response_submitted_at");

                entity.HasIndex(e => e.UpdatedAt)
                    .HasName("ix_brief_response_updated_at");

                entity.HasOne(d => d.Brief)
                    .WithMany(p => p.BriefResponse)
                    .HasForeignKey(d => d.BriefId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("brief_response_brief_id_fkey");

                entity.HasOne(d => d.SupplierCodeNavigation)
                    .WithMany(p => p.BriefResponse)
                    .HasPrincipalKey(p => p.Code)
                    .HasForeignKey(d => d.SupplierCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("brief_response_supplier_code_fkey");
            });

            modelBuilder.Entity<BriefResponseContact>(entity => {
                entity.HasOne(d => d.Brief)
                    .WithMany(p => p.BriefResponseContact)
                    .HasForeignKey(d => d.BriefId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("brief_response_brief_contact_id_fkey");

                entity.HasOne(d => d.SupplierCodeNavigation)
                    .WithMany(p => p.BriefResponseContact)
                    .HasPrincipalKey(p => p.Code)
                    .HasForeignKey(d => d.SupplierCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("brief_response_contact_supplier_code_fkey");
            });

            modelBuilder.Entity<BriefResponseDownload>(entity => {
                entity.HasIndex(e => e.CreatedAt)
                    .HasName("ix_brief_response_download_created_at");

                entity.HasOne(d => d.Brief)
                    .WithMany(p => p.BriefResponseDownload)
                    .HasForeignKey(d => d.BriefId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("brief_response_download_brief_id_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.BriefResponseDownload)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("brief_response_download_user_id_fkey");
            });

            modelBuilder.Entity<BriefUser>(entity => {
                entity.HasKey(e => new { e.BriefId, e.UserId })
                    .HasName("brief_user_pkey");

                entity.HasOne(d => d.Brief)
                    .WithMany(p => p.BriefUser)
                    .HasForeignKey(d => d.BriefId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("brief_user_brief_id_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.BriefUser)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("brief_user_user_id_fkey");
            });

            modelBuilder.Entity<CaseStudy>(entity => {
                entity.HasIndex(e => e.CreatedAt)
                    .HasName("ix_case_study_created_at");

                entity.HasOne(d => d.SupplierCodeNavigation)
                    .WithMany(p => p.CaseStudy)
                    .HasPrincipalKey(p => p.Code)
                    .HasForeignKey(d => d.SupplierCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("case_study_supplier_code_fkey");
            });

            modelBuilder.Entity<CaseStudyAssessment>(entity => {
                entity.HasOne(d => d.CaseStudy)
                    .WithMany(p => p.CaseStudyAssessment)
                    .HasForeignKey(d => d.CaseStudyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("case_study_assessment_case_study_id_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CaseStudyAssessment)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("case_study_assessment_user_id_fkey");
            });

            modelBuilder.Entity<CaseStudyAssessmentDomainCriteria>(entity => {
                entity.HasOne(d => d.CaseStudyAssessment)
                    .WithMany(p => p.CaseStudyAssessmentDomainCriteria)
                    .HasForeignKey(d => d.CaseStudyAssessmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("case_study_assessment_domain_crit_case_study_assessment_id_fkey");

                entity.HasOne(d => d.DomainCriteria)
                    .WithMany(p => p.CaseStudyAssessmentDomainCriteria)
                    .HasForeignKey(d => d.DomainCriteriaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("case_study_assessment_domain_criteria_domain_criteria_id_fkey");
            });

            modelBuilder.Entity<Council>(entity => {
                entity.HasIndex(e => e.Domain)
                    .HasName("ix_council_domain")
                    .IsUnique();
            });

            modelBuilder.Entity<DomainCriteria>(entity => {
                entity.HasOne(d => d.Domain)
                    .WithMany(p => p.DomainCriteria)
                    .HasForeignKey(d => d.DomainId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("domain_criteria_domain_id_fkey");
            });

            modelBuilder.Entity<DraftService>(entity => {
                entity.HasIndex(e => e.FrameworkId)
                    .HasName("ix_draft_service_framework_id");

                entity.HasIndex(e => e.LotId)
                    .HasName("ix_draft_service_lot_id");

                entity.HasIndex(e => e.ServiceId)
                    .HasName("ix_draft_service_service_id");

                entity.HasIndex(e => e.SupplierCode)
                    .HasName("ix_draft_service_supplier_code");

                entity.HasOne(d => d.Framework)
                    .WithMany(p => p.DraftService)
                    .HasForeignKey(d => d.FrameworkId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("draft_service_framework_id_fkey1");

                entity.HasOne(d => d.Lot)
                    .WithMany(p => p.DraftService)
                    .HasForeignKey(d => d.LotId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("draft_service_lot_id_fkey");

                entity.HasOne(d => d.SupplierCodeNavigation)
                    .WithMany(p => p.DraftService)
                    .HasPrincipalKey(p => p.Code)
                    .HasForeignKey(d => d.SupplierCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("draft_service_supplier_code_fkey");

                entity.HasOne(d => d.FrameworkLot)
                    .WithMany(p => p.DraftService)
                    .HasForeignKey(d => new { d.FrameworkId, d.LotId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("draft_service_framework_id_fkey");
            });

            modelBuilder.Entity<Evidence>(entity => {
                entity.HasIndex(e => e.ApprovedAt)
                    .HasName("ix_evidence_approved_at");

                entity.HasIndex(e => e.BriefId)
                    .HasName("ix_evidence_brief_id");

                entity.HasIndex(e => e.CreatedAt)
                    .HasName("ix_evidence_created_at");

                entity.HasIndex(e => e.DomainId)
                    .HasName("ix_evidence_domain_id");

                entity.HasIndex(e => e.RejectedAt)
                    .HasName("ix_evidence_rejected_at");

                entity.HasIndex(e => e.SubmittedAt)
                    .HasName("ix_evidence_submitted_at");

                entity.HasIndex(e => e.SupplierCode)
                    .HasName("ix_evidence_supplier_code");

                entity.HasIndex(e => e.UpdatedAt)
                    .HasName("ix_evidence_updated_at");

                entity.HasIndex(e => e.UserId)
                    .HasName("ix_evidence_user_id");

                entity.HasOne(d => d.Brief)
                    .WithMany(p => p.Evidence)
                    .HasForeignKey(d => d.BriefId)
                    .HasConstraintName("evidence_brief_id_fkey");

                entity.HasOne(d => d.Domain)
                    .WithMany(p => p.Evidence)
                    .HasForeignKey(d => d.DomainId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("evidence_domain_id_fkey");

                entity.HasOne(d => d.SupplierCodeNavigation)
                    .WithMany(p => p.Evidence)
                    .HasPrincipalKey(p => p.Code)
                    .HasForeignKey(d => d.SupplierCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("evidence_supplier_code_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Evidence)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("evidence_user_id_fkey");
            });

            modelBuilder.Entity<EvidenceAssessment>(entity => {
                entity.HasIndex(e => e.CreatedAt)
                    .HasName("ix_evidence_assessment_created_at");

                entity.HasIndex(e => e.EvidenceId)
                    .HasName("ix_evidence_assessment_evidence_id");

                entity.HasOne(d => d.Evidence)
                    .WithMany(p => p.EvidenceAssessment)
                    .HasForeignKey(d => d.EvidenceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("evidence_assessment_evidence_id_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.EvidenceAssessment)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("evidence_assessment_user_id_fkey");
            });

            modelBuilder.Entity<Framework>(entity => {
                entity.HasIndex(e => e.Framework1)
                    .HasName("ix_framework_framework");

                entity.HasIndex(e => e.Slug)
                    .HasName("ix_framework_slug")
                    .IsUnique();

                entity.HasIndex(e => e.Status)
                    .HasName("ix_framework_status");
            });

            modelBuilder.Entity<FrameworkLot>(entity => {
                entity.HasKey(e => new { e.FrameworkId, e.LotId })
                    .HasName("framework_lot_pkey");

                entity.HasOne(d => d.Framework)
                    .WithMany(p => p.FrameworkLot)
                    .HasForeignKey(d => d.FrameworkId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("framework_lot_framework_id_fkey");

                entity.HasOne(d => d.Lot)
                    .WithMany(p => p.FrameworkLot)
                    .HasForeignKey(d => d.LotId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("framework_lot_lot_id_fkey");
            });

            modelBuilder.Entity<KeyValue>(entity => {
                entity.HasIndex(e => e.Key)
                    .HasName("key_value_key_key")
                    .IsUnique();

                entity.HasIndex(e => e.UpdatedAt)
                    .HasName("ix_key_value_updated_at");
            });

            modelBuilder.Entity<Lot>(entity => {
                entity.HasIndex(e => e.Slug)
                    .HasName("ix_lot_slug");
            });

            modelBuilder.Entity<PriceSchedule>(entity => {
                entity.HasIndex(e => new { e.SupplierId, e.ServiceRoleId })
                    .HasName("price_schedule_supplier_id_service_role_id_key")
                    .IsUnique();

                entity.HasOne(d => d.ServiceRole)
                    .WithMany(p => p.PriceSchedule)
                    .HasForeignKey(d => d.ServiceRoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("price_schedule_service_role_id_fkey");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.PriceSchedule)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("price_schedule_supplier_id_fkey");
            });

            modelBuilder.Entity<Product>(entity => {
                entity.HasOne(d => d.SupplierCodeNavigation)
                    .WithMany(p => p.Product)
                    .HasPrincipalKey(p => p.Code)
                    .HasForeignKey(d => d.SupplierCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("product_supplier_code_fkey");
            });

            modelBuilder.Entity<Project>(entity => {
                entity.HasIndex(e => e.CreatedAt)
                    .HasName("ix_project_created_at");
            });

            modelBuilder.Entity<Service>(entity => {
                entity.HasIndex(e => e.FrameworkId)
                    .HasName("ix_service_framework_id");

                entity.HasIndex(e => e.LotId)
                    .HasName("ix_service_lot_id");

                entity.HasIndex(e => e.ServiceId)
                    .HasName("ix_service_service_id")
                    .IsUnique();

                entity.HasIndex(e => e.SupplierCode)
                    .HasName("ix_service_supplier_code");

                entity.HasOne(d => d.Framework)
                    .WithMany(p => p.Service)
                    .HasForeignKey(d => d.FrameworkId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("service_framework_id_fkey1");

                entity.HasOne(d => d.Lot)
                    .WithMany(p => p.Service)
                    .HasForeignKey(d => d.LotId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("service_lot_id_fkey");

                entity.HasOne(d => d.SupplierCodeNavigation)
                    .WithMany(p => p.Service)
                    .HasPrincipalKey(p => p.Code)
                    .HasForeignKey(d => d.SupplierCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("service_supplier_code_fkey");

                entity.HasOne(d => d.FrameworkLot)
                    .WithMany(p => p.Service)
                    .HasForeignKey(d => new { d.FrameworkId, d.LotId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("service_framework_id_fkey");
            });

            modelBuilder.Entity<ServiceCategory>(entity => {
                entity.HasIndex(e => e.Name)
                    .HasName("service_category_name_key")
                    .IsUnique();
            });

            modelBuilder.Entity<ServiceRole>(entity => {
                entity.HasOne(d => d.Category)
                    .WithMany(p => p.ServiceRole)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("service_role_category_id_fkey");
            });

            modelBuilder.Entity<ServiceType>(entity => {
                entity.HasIndex(e => e.FrameworkId)
                    .HasName("ix_service_type_framework_id");

                entity.HasIndex(e => e.LotId)
                    .HasName("ix_service_type_lot_id");
            });

            modelBuilder.Entity<SignedAgreement>(entity => {
                entity.HasKey(e => new { e.AgreementId, e.UserId, e.SignedAt })
                    .HasName("signed_agreement_pkey");

                entity.HasOne(d => d.Agreement)
                    .WithMany(p => p.SignedAgreement)
                    .HasForeignKey(d => d.AgreementId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("signed_agreement_agreement_id_fkey");

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.SignedAgreement)
                    .HasForeignKey(d => d.ApplicationId)
                    .HasConstraintName("signed_agreement_application_id_fkey");

                entity.HasOne(d => d.SupplierCodeNavigation)
                    .WithMany(p => p.SignedAgreement)
                    .HasPrincipalKey(p => p.Code)
                    .HasForeignKey(d => d.SupplierCode)
                    .HasConstraintName("signed_agreement_supplier_code_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SignedAgreement)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("signed_agreement_user_id_fkey");
            });

            modelBuilder.Entity<Supplier>(entity => {
                entity.HasIndex(e => e.Code)
                    .HasName("ix_supplier_code")
                    .IsUnique();

                entity.Property(e => e.Code).ValueGeneratedOnAdd();

                entity.Property(e => e.IsRecruiter).HasDefaultValueSql("false");
            });

            modelBuilder.Entity<SupplierContact>(entity => {
                entity.HasKey(e => new { e.SupplierId, e.ContactId })
                    .HasName("supplier__contact_pkey");

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.SupplierContact)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("supplier__contact_contact_id_fkey");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.SupplierContact)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("supplier__contact_supplier_id_fkey");
            });

            modelBuilder.Entity<SupplierDomain>(entity => {
                entity.HasIndex(e => e.Id)
                    .HasName("ix_supplier_domain_id")
                    .IsUnique();

                entity.HasOne(d => d.Domain)
                    .WithMany(p => p.SupplierDomain)
                    .HasForeignKey(d => d.DomainId)
                    .HasConstraintName("supplier_domain_domain_id_fkey");

                entity.HasOne(d => d.RecruiterInfo)
                    .WithMany(p => p.SupplierDomain)
                    .HasForeignKey(d => d.RecruiterInfoId)
                    .HasConstraintName("supplier_domain_recruiter_info_id_fkey");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.SupplierDomain)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("supplier_domain_supplier_id_fkey");
            });

            modelBuilder.Entity<SupplierExtraLinks>(entity => {
                entity.HasKey(e => new { e.SupplierId, e.WebsiteLinkId })
                    .HasName("supplier__extra_links_pkey");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.SupplierExtraLinks)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("supplier__extra_links_supplier_id_fkey");

                entity.HasOne(d => d.WebsiteLink)
                    .WithMany(p => p.SupplierExtraLinks)
                    .HasForeignKey(d => d.WebsiteLinkId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("supplier__extra_links_website_link_id_fkey");
            });

            modelBuilder.Entity<SupplierFramework>(entity => {
                entity.HasKey(e => new { e.SupplierCode, e.FrameworkId })
                    .HasName("supplier_framework_pkey");

                entity.HasOne(d => d.Framework)
                    .WithMany(p => p.SupplierFramework)
                    .HasForeignKey(d => d.FrameworkId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("supplier_framework_framework_id_fkey");

                entity.HasOne(d => d.SupplierCodeNavigation)
                    .WithMany(p => p.SupplierFramework)
                    .HasPrincipalKey(p => p.Code)
                    .HasForeignKey(d => d.SupplierCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("supplier_framework_supplier_code_fkey");
            });

            modelBuilder.Entity<SupplierReference>(entity => {
                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.SupplierReference)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("supplier_reference_supplier_id_fkey");
            });

            modelBuilder.Entity<SupplierUserInviteLog>(entity => {
                entity.HasKey(e => new { e.SupplierId, e.ContactId })
                    .HasName("supplier_user_invite_log_pkey");

                entity.HasIndex(e => e.InviteSent)
                    .HasName("ix_supplier_user_invite_log_invite_sent");

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.SupplierUserInviteLog)
                    .HasForeignKey(d => d.ContactId)
                    .HasConstraintName("supplier_user_invite_log_contact_id_fkey");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.SupplierUserInviteLog)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("supplier_user_invite_log_supplier_id_fkey1");

                entity.HasOne(d => d.SupplierContact)
                    .WithOne(p => p.SupplierUserInviteLog)
                    .HasForeignKey<SupplierUserInviteLog>(d => new { d.SupplierId, d.ContactId })
                    .HasConstraintName("supplier_user_invite_log_supplier_id_fkey");
            });

            modelBuilder.Entity<TeamBrief>(entity => {
                entity.HasOne(d => d.Brief)
                    .WithMany(p => p.TeamBrief)
                    .HasForeignKey(d => d.BriefId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("team_brief_brief_id_fkey");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.TeamBrief)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("team_brief_team_id_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TeamBrief)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("team_brief_user_id_fkey");
            });

            modelBuilder.Entity<TeamMember>(entity => {
                entity.HasOne(d => d.Team)
                    .WithMany(p => p.TeamMember)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("team_member_team_id_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TeamMember)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("team_member_user_id_fkey");
            });

            modelBuilder.Entity<TeamMemberPermission>(entity => {
                entity.HasIndex(e => e.Permission)
                    .HasName("ix_team_member_permission_permission");

                entity.HasOne(d => d.TeamMember)
                    .WithMany(p => p.TeamMemberPermission)
                    .HasForeignKey(d => d.TeamMemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("team_member_permission_team_member_id_fkey");
            });

            modelBuilder.Entity<User>(entity => {
                entity.HasIndex(e => e.AgencyId)
                    .HasName("ix_user_agency_id");

                entity.HasIndex(e => e.ApplicationId)
                    .HasName("ix_user_application_id");

                entity.HasIndex(e => e.EmailAddress)
                    .HasName("ix_user_email_address")
                    .IsUnique();

                entity.HasIndex(e => e.SupplierCode)
                    .HasName("ix_user_supplier_code");

                entity.HasOne(d => d.Agency)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.AgencyId)
                    .HasConstraintName("user_agency_id_fkey");

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.ApplicationId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("user_application_id_fkey");

                entity.HasOne(d => d.SupplierCodeNavigation)
                    .WithMany(p => p.User)
                    .HasPrincipalKey(p => p.Code)
                    .HasForeignKey(d => d.SupplierCode)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("user_supplier_code_fkey");
            });

            modelBuilder.Entity<UserClaim>(entity => {
                entity.HasIndex(e => e.EmailAddress)
                    .HasName("ix_user_claim_email_address");

                entity.HasIndex(e => e.Token)
                    .HasName("ix_user_claim_token");

                entity.HasIndex(e => e.Type)
                    .HasName("ix_user_claim_type");
            });

            modelBuilder.Entity<UserFramework>(entity => {
                entity.HasKey(e => new { e.UserId, e.FrameworkId })
                    .HasName("user_framework_pkey");

                entity.HasOne(d => d.Framework)
                    .WithMany(p => p.UserFramework)
                    .HasForeignKey(d => d.FrameworkId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_framework_framework_id_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserFramework)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_framework_user_id_fkey");
            });

            modelBuilder.Entity<Vuser>(entity => {
                entity.HasNoKey();
            });

            modelBuilder.Entity<WorkOrder>(entity => {
                entity.HasIndex(e => e.BriefId)
                    .HasName("work_order_brief_id_key")
                    .IsUnique();

                entity.HasIndex(e => e.CreatedAt)
                    .HasName("ix_work_order_created_at");

                entity.HasOne(d => d.Brief)
                    .WithOne(p => p.WorkOrder)
                    .HasForeignKey<WorkOrder>(d => d.BriefId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("work_order_brief_id_fkey");

                entity.HasOne(d => d.SupplierCodeNavigation)
                    .WithMany(p => p.WorkOrder)
                    .HasPrincipalKey(p => p.Code)
                    .HasForeignKey(d => d.SupplierCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("work_order_supplier_code_fkey");
            });

            modelBuilder.HasSequence("agency_domain_id_seq");

            modelBuilder.HasSequence("agency_id_seq");

            modelBuilder.HasSequence("api_key_id_seq");

            modelBuilder.HasSequence("assessment_id_seq");

            modelBuilder.HasSequence("brief_assessor_id_seq");

            modelBuilder.HasSequence("brief_history_id_seq");

            modelBuilder.HasSequence("brief_question_id_seq");

            modelBuilder.HasSequence("brief_response_answer_id_seq");

            modelBuilder.HasSequence("brief_response_contact_id_seq");

            modelBuilder.HasSequence("brief_response_download_id_seq");

            modelBuilder.HasSequence("case_study_assessment_domain_criteria_id_seq");

            modelBuilder.HasSequence("case_study_assessment_id_seq");

            modelBuilder.HasSequence("case_study_domain_criteria_id_seq");

            modelBuilder.HasSequence("council_id_seq");

            modelBuilder.HasSequence("domain_criteria_id_seq");

            modelBuilder.HasSequence("evidence_assessment_id_seq");

            modelBuilder.HasSequence("evidence_id_seq");

            modelBuilder.HasSequence("insight_id_seq");

            modelBuilder.HasSequence("key_value_id_seq");

            modelBuilder.HasSequence("location_id_seq");

            modelBuilder.HasSequence("master_agreement_id_seq");

            modelBuilder.HasSequence("product_id_seq");

            modelBuilder.HasSequence("project_id_seq");

            modelBuilder.HasSequence("recruiter_info_id_seq");

            modelBuilder.HasSequence("region_id_seq");

            modelBuilder.HasSequence("service_sub_type_id_seq");

            modelBuilder.HasSequence("service_type_id_seq");

            modelBuilder.HasSequence("service_type_price_ceiling_id_seq");

            modelBuilder.HasSequence("service_type_price_id_seq");

            modelBuilder.HasSequence("supplier_code_seq");

            modelBuilder.HasSequence("supplier_domain_id_seq");

            modelBuilder.HasSequence("team_brief_id_seq");

            modelBuilder.HasSequence("team_id_seq");

            modelBuilder.HasSequence("team_member_id_seq");

            modelBuilder.HasSequence("team_member_permission_id_seq");

            modelBuilder.HasSequence("user_claim_id_seq");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
