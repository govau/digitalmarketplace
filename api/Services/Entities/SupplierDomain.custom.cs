using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dta.Marketplace.Api.Services.Entities {
    public enum SupplierDomainPriceStatus {
        approved,
        rejected,
        unassessed
    }
    public enum SupplierDomainStatus {
        unassessed,
        assessed,
        rejected
    }
    public partial class SupplierDomain {
        [Required]
        [Column("price_status", TypeName = "supplier_domain_price_status_enum")]
        public SupplierDomainPriceStatus PriceStatus { get; set; }

        [Required]
        [Column("status", TypeName = "supplier_domain_status_enum")]
        public SupplierDomainStatus Status { get; set; }
    }
}
