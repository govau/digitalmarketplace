using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dta.Marketplace.Api.Entities
{
    [Table("team_member_permission")]
    public partial class TeamMemberPermission
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("team_member_id")]
        public int TeamMemberId { get; set; }

        [ForeignKey(nameof(TeamMemberId))]
        [InverseProperty("TeamMemberPermission")]
        public virtual TeamMember TeamMember { get; set; }
    }
}
