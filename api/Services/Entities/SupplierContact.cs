using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dta.Marketplace.Api.Services.Entities
{
    [Table("supplier__contact")]
    public partial class SupplierContact
    {
        [Key]
        [Column("supplier_id")]
        public int SupplierId { get; set; }
        [Key]
        [Column("contact_id")]
        public int ContactId { get; set; }

        [ForeignKey(nameof(ContactId))]
        [InverseProperty("SupplierContact")]
        public virtual Contact Contact { get; set; }
        [ForeignKey(nameof(SupplierId))]
        [InverseProperty("SupplierContact")]
        public virtual Supplier Supplier { get; set; }
        [InverseProperty("SupplierContact")]
        public virtual SupplierUserInviteLog SupplierUserInviteLog { get; set; }
    }
}
