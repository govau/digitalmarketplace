using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dta.Marketplace.Api.Entities {
    public enum ApplicationStatus {
        saved,
        submitted,
        approved,
        complete,
        approval_rejected,
        assessment_rejected,
        deleted
    }
    
    public enum ApplicationType {
        @new,
        edit,
        upgrade
    }

    public partial class Application {
        [Required]
        [Column("status", TypeName = "application_status_enum")]
        public ApplicationStatus Status { get; set; }

        [Required]
        [Column("type", TypeName = "application_type_enum")]
        public ApplicationType Type { get; set; }
    }
}
