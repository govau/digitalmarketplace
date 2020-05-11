using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dta.Marketplace.Api.Services.Entities {
    public enum UserRole {
        buyer,
        supplier,
        admin,
        // admin-ccs-category,
        // admin-ccs-sourcing,
        applicant,
    }
    public partial class User {

        [Required]
        [Column("role", TypeName = "user_roles_enum")]
        public UserRole Role { get; set; }
    }
}
