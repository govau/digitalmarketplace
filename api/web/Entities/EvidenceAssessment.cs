using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dta.Marketplace.Api.Web.Entities
{
    [Table("evidence_assessment")]
    public partial class EvidenceAssessment
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("evidence_id")]
        public int EvidenceId { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        [Column("data", TypeName = "json")]
        public string Data { get; set; }

        [ForeignKey(nameof(EvidenceId))]
        [InverseProperty("EvidenceAssessment")]
        public virtual Evidence Evidence { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("EvidenceAssessment")]
        public virtual User User { get; set; }
    }
}
