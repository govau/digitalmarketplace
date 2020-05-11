using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dta.Marketplace.Api.Services.Entities
{
    [Table("supplier_domain")]
    public partial class SupplierDomain
    {
        public SupplierDomain()
        {
            Assessment = new HashSet<Assessment>();
        }

        [Column("supplier_id")]
        public int? SupplierId { get; set; }
        [Column("domain_id")]
        public int? DomainId { get; set; }
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("recruiter_info_id")]
        public int? RecruiterInfoId { get; set; }

        [ForeignKey(nameof(DomainId))]
        [InverseProperty("SupplierDomain")]
        public virtual Domain Domain { get; set; }
        [ForeignKey(nameof(RecruiterInfoId))]
        [InverseProperty("SupplierDomain")]
        public virtual RecruiterInfo RecruiterInfo { get; set; }
        [ForeignKey(nameof(SupplierId))]
        [InverseProperty("SupplierDomain")]
        public virtual Supplier Supplier { get; set; }
        [InverseProperty("SupplierDomain")]
        public virtual ICollection<Assessment> Assessment { get; set; }
    }
}
