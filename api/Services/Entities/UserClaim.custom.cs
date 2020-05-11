using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dta.Marketplace.Api.Services.Entities {
    public enum UserClaimType {
        signup,
        password_reset
    }

    public partial class UserClaim {
        [Required]
        [Column("type", TypeName = "user_claim_type_enum")]
        public UserClaimType Type { get; set; }
    }
}
