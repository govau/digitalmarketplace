using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dta.Marketplace.Api.Web.Entities
{
    [Table("supplier_user_invite_log")]
    public partial class SupplierUserInviteLog
    {
        [Key]
        [Column("supplier_id")]
        public int SupplierId { get; set; }
        [Key]
        [Column("contact_id")]
        public int ContactId { get; set; }
        [Column("invite_sent")]
        public DateTime InviteSent { get; set; }

        [ForeignKey(nameof(ContactId))]
        [InverseProperty("SupplierUserInviteLog")]
        public virtual Contact Contact { get; set; }
        [ForeignKey(nameof(SupplierId))]
        [InverseProperty("SupplierUserInviteLog")]
        public virtual Supplier Supplier { get; set; }
        [ForeignKey("SupplierId,ContactId")]
        [InverseProperty("SupplierUserInviteLog")]
        public virtual SupplierContact SupplierContact { get; set; }
    }
}
