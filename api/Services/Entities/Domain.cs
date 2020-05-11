using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dta.Marketplace.Api.Services.Entities
{
    [Table("domain")]
    public partial class Domain
    {
        public Domain()
        {
            Brief = new HashSet<Brief>();
            DomainCriteria = new HashSet<DomainCriteria>();
            Evidence = new HashSet<Evidence>();
            SupplierDomain = new HashSet<SupplierDomain>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("name", TypeName = "character varying")]
        public string Name { get; set; }
        [Column("ordering")]
        public int Ordering { get; set; }
        [Column("price_minimum", TypeName = "numeric")]
        public decimal PriceMinimum { get; set; }
        [Column("price_maximum", TypeName = "numeric")]
        public decimal PriceMaximum { get; set; }
        [Column("criteria_needed", TypeName = "numeric")]
        public decimal CriteriaNeeded { get; set; }

        [InverseProperty("Domain")]
        public virtual ICollection<Brief> Brief { get; set; }
        [InverseProperty("Domain")]
        public virtual ICollection<DomainCriteria> DomainCriteria { get; set; }
        [InverseProperty("Domain")]
        public virtual ICollection<Evidence> Evidence { get; set; }
        [InverseProperty("Domain")]
        public virtual ICollection<SupplierDomain> SupplierDomain { get; set; }
    }
}
