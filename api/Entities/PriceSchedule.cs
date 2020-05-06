using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dta.Marketplace.Api.Entities
{
    [Table("price_schedule")]
    public partial class PriceSchedule
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("supplier_id")]
        public int SupplierId { get; set; }
        [Column("service_role_id")]
        public int ServiceRoleId { get; set; }
        [Column("hourly_rate", TypeName = "numeric")]
        public decimal? HourlyRate { get; set; }
        [Column("daily_rate", TypeName = "numeric")]
        public decimal? DailyRate { get; set; }
        [Column("gst_included")]
        public bool GstIncluded { get; set; }

        [ForeignKey(nameof(ServiceRoleId))]
        [InverseProperty("PriceSchedule")]
        public virtual ServiceRole ServiceRole { get; set; }
        [ForeignKey(nameof(SupplierId))]
        [InverseProperty("PriceSchedule")]
        public virtual Supplier Supplier { get; set; }
    }
}
