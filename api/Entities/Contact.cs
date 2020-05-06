using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dta.Marketplace.Api.Entities
{
    [Table("contact")]
    public partial class Contact
    {
        public Contact()
        {
            SupplierContact = new HashSet<SupplierContact>();
            SupplierUserInviteLog = new HashSet<SupplierUserInviteLog>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("contact_for", TypeName = "character varying")]
        public string ContactFor { get; set; }
        [Required]
        [Column("name", TypeName = "character varying")]
        public string Name { get; set; }
        [Column("role", TypeName = "character varying")]
        public string Role { get; set; }
        [Column("email", TypeName = "character varying")]
        public string Email { get; set; }
        [Column("phone", TypeName = "character varying")]
        public string Phone { get; set; }
        [Column("fax", TypeName = "character varying")]
        public string Fax { get; set; }

        [InverseProperty("Contact")]
        public virtual ICollection<SupplierContact> SupplierContact { get; set; }
        [InverseProperty("Contact")]
        public virtual ICollection<SupplierUserInviteLog> SupplierUserInviteLog { get; set; }
    }
}
