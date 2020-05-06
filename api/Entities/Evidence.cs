using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dta.Marketplace.Api.Entities
{
    [Table("evidence")]
    public partial class Evidence
    {
        public Evidence()
        {
            EvidenceAssessment = new HashSet<EvidenceAssessment>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("domain_id")]
        public int DomainId { get; set; }
        [Column("brief_id")]
        public int? BriefId { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("supplier_code")]
        public long SupplierCode { get; set; }
        [Column("data", TypeName = "json")]
        public string Data { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
        [Column("submitted_at")]
        public DateTime? SubmittedAt { get; set; }
        [Column("approved_at")]
        public DateTime? ApprovedAt { get; set; }
        [Column("rejected_at")]
        public DateTime? RejectedAt { get; set; }

        [ForeignKey(nameof(BriefId))]
        [InverseProperty("Evidence")]
        public virtual Brief Brief { get; set; }
        [ForeignKey(nameof(DomainId))]
        [InverseProperty("Evidence")]
        public virtual Domain Domain { get; set; }
        public virtual Supplier SupplierCodeNavigation { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("Evidence")]
        public virtual User User { get; set; }
        [InverseProperty("Evidence")]
        public virtual ICollection<EvidenceAssessment> EvidenceAssessment { get; set; }
    }
}
