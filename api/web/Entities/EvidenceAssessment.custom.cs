using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dta.Marketplace.Api.Web.Entities {
    public enum EvidenceAssessmentStatus {
        unassessed,
        assessed,
        rejected,
        approved
    }
    public partial class EvidenceAssessment {
        [Required]
        [Column("status", TypeName = "evidence_assessment_status_enum")]
        public EvidenceAssessmentStatus Status { get; set; }
    }
}
