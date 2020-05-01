using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dta.Marketplace.Api.Web.Entities {
    public enum CaseStudyStatus {
        unassessed,
        assessed,
        rejected,
        approved
    }
    public partial class CaseStudy {
        [Required]
        [Column("status", TypeName = "case_study_status_enum")]
        public CaseStudyStatus Status { get; set; }
    }
}
