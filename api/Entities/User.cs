using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dta.Marketplace.Api.Entities
{
    [Table("user")]
    public partial class User
    {
        public User()
        {
            ApiKey = new HashSet<ApiKey>();
            BriefAssessor = new HashSet<BriefAssessor>();
            BriefClarificationQuestion = new HashSet<BriefClarificationQuestion>();
            BriefHistory = new HashSet<BriefHistory>();
            BriefResponseDownload = new HashSet<BriefResponseDownload>();
            BriefUser = new HashSet<BriefUser>();
            CaseStudyAssessment = new HashSet<CaseStudyAssessment>();
            Evidence = new HashSet<Evidence>();
            EvidenceAssessment = new HashSet<EvidenceAssessment>();
            SignedAgreement = new HashSet<SignedAgreement>();
            TeamBrief = new HashSet<TeamBrief>();
            TeamMember = new HashSet<TeamMember>();
            UserFramework = new HashSet<UserFramework>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("name", TypeName = "character varying")]
        public string Name { get; set; }
        [Required]
        [Column("email_address", TypeName = "character varying")]
        public string EmailAddress { get; set; }
        [Column("phone_number", TypeName = "character varying")]
        public string PhoneNumber { get; set; }
        [Required]
        [Column("password", TypeName = "character varying")]
        public string Password { get; set; }
        [Column("active")]
        public bool Active { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
        [Column("password_changed_at")]
        public DateTime PasswordChangedAt { get; set; }
        [Column("logged_in_at")]
        public DateTime? LoggedInAt { get; set; }
        [Column("failed_login_count")]
        public int FailedLoginCount { get; set; }
        [Column("supplier_code")]
        public long? SupplierCode { get; set; }
        [Column("terms_accepted_at")]
        public DateTime TermsAcceptedAt { get; set; }
        [Column("application_id")]
        public long? ApplicationId { get; set; }
        [Column("agency_id")]
        public int? AgencyId { get; set; }

        [ForeignKey(nameof(AgencyId))]
        [InverseProperty("User")]
        public virtual Agency Agency { get; set; }
        [ForeignKey(nameof(ApplicationId))]
        [InverseProperty("User")]
        public virtual Application Application { get; set; }
        public virtual Supplier SupplierCodeNavigation { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<ApiKey> ApiKey { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<BriefAssessor> BriefAssessor { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<BriefClarificationQuestion> BriefClarificationQuestion { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<BriefHistory> BriefHistory { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<BriefResponseDownload> BriefResponseDownload { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<BriefUser> BriefUser { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<CaseStudyAssessment> CaseStudyAssessment { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Evidence> Evidence { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<EvidenceAssessment> EvidenceAssessment { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<SignedAgreement> SignedAgreement { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<TeamBrief> TeamBrief { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<TeamMember> TeamMember { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<UserFramework> UserFramework { get; set; }
    }
}
