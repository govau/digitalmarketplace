using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dta.Marketplace.Api.Web.Entities
{
    [Table("brief_assessment")]
    public partial class BriefAssessment
    {
        [Key]
        [Column("brief_id")]
        public int BriefId { get; set; }
        [Key]
        [Column("assessment_id")]
        public int AssessmentId { get; set; }

        [ForeignKey(nameof(AssessmentId))]
        [InverseProperty("BriefAssessment")]
        public virtual Assessment Assessment { get; set; }
        [ForeignKey(nameof(BriefId))]
        [InverseProperty("BriefAssessment")]
        public virtual Brief Brief { get; set; }
    }
}
