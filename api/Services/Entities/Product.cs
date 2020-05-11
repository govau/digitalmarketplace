using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dta.Marketplace.Api.Services.Entities
{
    [Table("product")]
    public partial class Product
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("name", TypeName = "character varying")]
        public string Name { get; set; }
        [Column("pricing", TypeName = "character varying")]
        public string Pricing { get; set; }
        [Column("summary", TypeName = "character varying")]
        public string Summary { get; set; }
        [Column("support", TypeName = "character varying")]
        public string Support { get; set; }
        [Column("website", TypeName = "character varying")]
        public string Website { get; set; }
        [Column("supplier_code")]
        public long SupplierCode { get; set; }

        public virtual Supplier SupplierCodeNavigation { get; set; }
    }
}
