using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dta.Marketplace.Api.Web.Entities
{
    [Table("case_study")]
    public partial class CaseStudy
    {
        public CaseStudy()
        {
            CaseStudyAssessment = new HashSet<CaseStudyAssessment>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("data", TypeName = "json")]
        public string Data { get; set; }
        [Column("supplier_code")]
        public long SupplierCode { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        public virtual Supplier SupplierCodeNavigation { get; set; }
        [InverseProperty("CaseStudy")]
        public virtual ICollection<CaseStudyAssessment> CaseStudyAssessment { get; set; }
    }
}
