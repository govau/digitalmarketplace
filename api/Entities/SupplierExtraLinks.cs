using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dta.Marketplace.Api.Entities
{
    [Table("supplier__extra_links")]
    public partial class SupplierExtraLinks
    {
        [Key]
        [Column("supplier_id")]
        public int SupplierId { get; set; }
        [Key]
        [Column("website_link_id")]
        public int WebsiteLinkId { get; set; }

        [ForeignKey(nameof(SupplierId))]
        [InverseProperty("SupplierExtraLinks")]
        public virtual Supplier Supplier { get; set; }
        [ForeignKey(nameof(WebsiteLinkId))]
        [InverseProperty("SupplierExtraLinks")]
        public virtual WebsiteLink WebsiteLink { get; set; }
    }
}
