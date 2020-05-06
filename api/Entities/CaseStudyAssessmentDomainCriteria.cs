using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dta.Marketplace.Api.Entities
{
    [Table("case_study_assessment_domain_criteria")]
    public partial class CaseStudyAssessmentDomainCriteria
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("domain_criteria_id")]
        public int DomainCriteriaId { get; set; }
        [Column("case_study_assessment_id")]
        public int CaseStudyAssessmentId { get; set; }

        [ForeignKey(nameof(CaseStudyAssessmentId))]
        [InverseProperty("CaseStudyAssessmentDomainCriteria")]
        public virtual CaseStudyAssessment CaseStudyAssessment { get; set; }
        [ForeignKey(nameof(DomainCriteriaId))]
        [InverseProperty("CaseStudyAssessmentDomainCriteria")]
        public virtual DomainCriteria DomainCriteria { get; set; }
    }
}
