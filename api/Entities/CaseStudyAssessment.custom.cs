using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dta.Marketplace.Api.Entities {
    public enum CaseStudyAssessmentStatus {
        unassessed,
        approved,
        rejected
    }
    public partial class CaseStudyAssessment {
        [Required]
        [Column("status", TypeName = "case_study_assessment_status_enum")]
        public CaseStudyAssessmentStatus Status { get; set; }
    }
}
