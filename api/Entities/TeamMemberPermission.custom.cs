using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dta.Marketplace.Api.Entities {
    public enum PermissionType {
        create_drafts,
        publish_opportunities,
        answer_seller_questions,
        download_responses,
        download_reports,
        create_work_orders
    }
    public partial class TeamMemberPermission {
        [Required]
        [Column("permission", TypeName = "permission_type_enum")]
        public PermissionType Permission { get; set; }
    }
}
