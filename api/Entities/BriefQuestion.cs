using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dta.Marketplace.Api.Entities
{
    [Table("brief_question")]
    public partial class BriefQuestion
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("brief_id")]
        public int BriefId { get; set; }
        [Column("supplier_code")]
        public long SupplierCode { get; set; }
        [Required]
        [Column("data", TypeName = "json")]
        public string Data { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        [Column("answered")]
        public bool Answered { get; set; }

        [ForeignKey(nameof(BriefId))]
        [InverseProperty("BriefQuestion")]
        public virtual Brief Brief { get; set; }
        public virtual Supplier SupplierCodeNavigation { get; set; }
    }
}
