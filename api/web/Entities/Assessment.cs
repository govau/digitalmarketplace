using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dta.Marketplace.Api.Web.Entities
{
    [Table("assessment")]
    public partial class Assessment
    {
        public Assessment()
        {
            BriefAssessment = new HashSet<BriefAssessment>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        [Column("supplier_domain_id")]
        public int SupplierDomainId { get; set; }
        [Column("active")]
        public bool Active { get; set; }

        [ForeignKey(nameof(SupplierDomainId))]
        [InverseProperty("Assessment")]
        public virtual SupplierDomain SupplierDomain { get; set; }
        [InverseProperty("Assessment")]
        public virtual ICollection<BriefAssessment> BriefAssessment { get; set; }
    }
}
