using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dta.Marketplace.Api.Entities
{
    [Table("application")]
    public partial class Application
    {
        public Application()
        {
            SignedAgreement = new HashSet<SignedAgreement>();
            User = new HashSet<User>();
        }

        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Required]
        [Column("data", TypeName = "json")]
        public string Data { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        [Column("supplier_code")]
        public long? SupplierCode { get; set; }
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }

        public virtual Supplier SupplierCodeNavigation { get; set; }
        [InverseProperty("Application")]
        public virtual ICollection<SignedAgreement> SignedAgreement { get; set; }
        [InverseProperty("Application")]
        public virtual ICollection<User> User { get; set; }
    }
}
