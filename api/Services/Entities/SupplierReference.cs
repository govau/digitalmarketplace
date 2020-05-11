using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dta.Marketplace.Api.Services.Entities
{
    [Table("supplier_reference")]
    public partial class SupplierReference
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("supplier_id")]
        public int? SupplierId { get; set; }
        [Required]
        [Column("name", TypeName = "character varying")]
        public string Name { get; set; }
        [Required]
        [Column("organisation", TypeName = "character varying")]
        public string Organisation { get; set; }
        [Column("role", TypeName = "character varying")]
        public string Role { get; set; }
        [Required]
        [Column("email", TypeName = "character varying")]
        public string Email { get; set; }

        [ForeignKey(nameof(SupplierId))]
        [InverseProperty("SupplierReference")]
        public virtual Supplier Supplier { get; set; }
    }
}
