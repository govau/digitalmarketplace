using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dta.Marketplace.Api.Entities
{
    [Table("case_study_assessment")]
    public partial class CaseStudyAssessment
    {
        public CaseStudyAssessment()
        {
            CaseStudyAssessmentDomainCriteria = new HashSet<CaseStudyAssessmentDomainCriteria>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("case_study_id")]
        public int CaseStudyId { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("comment", TypeName = "character varying")]
        public string Comment { get; set; }

        [ForeignKey(nameof(CaseStudyId))]
        [InverseProperty("CaseStudyAssessment")]
        public virtual CaseStudy CaseStudy { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("CaseStudyAssessment")]
        public virtual User User { get; set; }
        [InverseProperty("CaseStudyAssessment")]
        public virtual ICollection<CaseStudyAssessmentDomainCriteria> CaseStudyAssessmentDomainCriteria { get; set; }
    }
}
