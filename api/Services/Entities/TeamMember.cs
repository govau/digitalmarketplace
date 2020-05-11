using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dta.Marketplace.Api.Services.Entities
{
    [Table("team_member")]
    public partial class TeamMember
    {
        public TeamMember()
        {
            TeamMemberPermission = new HashSet<TeamMemberPermission>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("is_team_lead")]
        public bool IsTeamLead { get; set; }
        [Column("team_id")]
        public int TeamId { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [ForeignKey(nameof(TeamId))]
        [InverseProperty("TeamMember")]
        public virtual Team Team { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("TeamMember")]
        public virtual User User { get; set; }
        [InverseProperty("TeamMember")]
        public virtual ICollection<TeamMemberPermission> TeamMemberPermission { get; set; }
    }
}
