using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dta.Marketplace.Api.Web.Entities
{
    [Table("draft_service")]
    public partial class DraftService
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("data", TypeName = "json")]
        public string Data { get; set; }
        [Required]
        [Column("status", TypeName = "character varying")]
        public string Status { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
        [Column("service_id", TypeName = "character varying")]
        public string ServiceId { get; set; }
        [Column("framework_id")]
        public int FrameworkId { get; set; }
        [Column("lot_id")]
        public int LotId { get; set; }
        [Column("supplier_code")]
        public long SupplierCode { get; set; }

        [ForeignKey(nameof(FrameworkId))]
        [InverseProperty("DraftService")]
        public virtual Framework Framework { get; set; }
        [ForeignKey("FrameworkId,LotId")]
        [InverseProperty("DraftService")]
        public virtual FrameworkLot FrameworkLot { get; set; }
        [ForeignKey(nameof(LotId))]
        [InverseProperty("DraftService")]
        public virtual Lot Lot { get; set; }
        public virtual Supplier SupplierCodeNavigation { get; set; }
    }
}
