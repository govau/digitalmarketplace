using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dta.Marketplace.Api.Web.Entities
{
    [Table("domain_criteria")]
    public partial class DomainCriteria
    {
        public DomainCriteria()
        {
            CaseStudyAssessmentDomainCriteria = new HashSet<CaseStudyAssessmentDomainCriteria>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("name", TypeName = "character varying")]
        public string Name { get; set; }
        [Column("domain_id")]
        public int DomainId { get; set; }

        [ForeignKey(nameof(DomainId))]
        [InverseProperty("DomainCriteria")]
        public virtual Domain Domain { get; set; }
        [InverseProperty("DomainCriteria")]
        public virtual ICollection<CaseStudyAssessmentDomainCriteria> CaseStudyAssessmentDomainCriteria { get; set; }
    }
}
