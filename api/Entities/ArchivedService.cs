using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dta.Marketplace.Api.Entities
{
    [Table("archived_service")]
    public partial class ArchivedService
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
        [Required]
        [Column("service_id", TypeName = "character varying")]
        public string ServiceId { get; set; }
        [Column("framework_id")]
        public int FrameworkId { get; set; }
        [Column("lot_id")]
        public int LotId { get; set; }
        [Column("supplier_code")]
        public long SupplierCode { get; set; }

        [ForeignKey(nameof(FrameworkId))]
        [InverseProperty("ArchivedService")]
        public virtual Framework Framework { get; set; }
        [ForeignKey("FrameworkId,LotId")]
        [InverseProperty("ArchivedService")]
        public virtual FrameworkLot FrameworkLot { get; set; }
        [ForeignKey(nameof(LotId))]
        [InverseProperty("ArchivedService")]
        public virtual Lot Lot { get; set; }
        public virtual Supplier SupplierCodeNavigation { get; set; }
    }
}
