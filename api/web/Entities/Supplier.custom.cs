using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dta.Marketplace.Api.Web.Entities {
    public enum SupplierStatus {
        limited,
        complete,
        deleted
    }
    public partial class Supplier {
        [Required]
        [Column("status", TypeName = "supplier_status_enum")]
        public SupplierStatus Status { get; set; }
    }
}
