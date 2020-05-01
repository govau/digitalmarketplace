using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dta.Marketplace.Api.Web.Entities
{
    [Table("work_order")]
    public partial class WorkOrder
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("brief_id")]
        public int BriefId { get; set; }
        [Column("supplier_code")]
        public long SupplierCode { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        [Required]
        [Column("data", TypeName = "json")]
        public string Data { get; set; }

        [ForeignKey(nameof(BriefId))]
        [InverseProperty("WorkOrder")]
        public virtual Brief Brief { get; set; }
        public virtual Supplier SupplierCodeNavigation { get; set; }
    }
}
