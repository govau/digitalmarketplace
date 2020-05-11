using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dta.Marketplace.Api.Services.Entities
{
    [Table("address")]
    public partial class Address
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("address_line", TypeName = "character varying")]
        public string AddressLine { get; set; }
        [Column("suburb", TypeName = "character varying")]
        public string Suburb { get; set; }
        [Required]
        [Column("state", TypeName = "character varying")]
        public string State { get; set; }
        [Required]
        [Column("postal_code", TypeName = "character varying")]
        public string PostalCode { get; set; }
        [Required]
        [Column("country", TypeName = "character varying")]
        public string Country { get; set; }
        [Column("supplier_code")]
        public long? SupplierCode { get; set; }

        public virtual Supplier SupplierCodeNavigation { get; set; }
    }
}
