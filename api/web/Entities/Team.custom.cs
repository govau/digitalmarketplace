using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dta.Marketplace.Api.Web.Entities {
    public enum TeamStatus {
        created,
        completed,
        deleted
    }
    public partial class Team {
        [Required]
        [Column("status", TypeName = "team_status_enum")]
        public TeamStatus Status { get; set; }
    }
}
