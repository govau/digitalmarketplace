using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dta.Marketplace.Api.Web.Entities
{
    [Table("supplier_framework")]
    public partial class SupplierFramework
    {
        [Key]
        [Column("supplier_code")]
        public long SupplierCode { get; set; }
        [Key]
        [Column("framework_id")]
        public int FrameworkId { get; set; }
        [Column("declaration", TypeName = "json")]
        public string Declaration { get; set; }
        [Column("on_framework")]
        public bool? OnFramework { get; set; }
        [Column("agreement_returned_at")]
        public DateTime? AgreementReturnedAt { get; set; }
        [Column("countersigned_at")]
        public DateTime? CountersignedAt { get; set; }
        [Column("agreement_details", TypeName = "json")]
        public string AgreementDetails { get; set; }

        [ForeignKey(nameof(FrameworkId))]
        [InverseProperty("SupplierFramework")]
        public virtual Framework Framework { get; set; }
        public virtual Supplier SupplierCodeNavigation { get; set; }
    }
}
