using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dta.Marketplace.Api.Entities
{
    [Table("team_brief")]
    public partial class TeamBrief
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("team_id")]
        public int TeamId { get; set; }
        [Column("brief_id")]
        public int BriefId { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }

        [ForeignKey(nameof(BriefId))]
        [InverseProperty("TeamBrief")]
        public virtual Brief Brief { get; set; }
        [ForeignKey(nameof(TeamId))]
        [InverseProperty("TeamBrief")]
        public virtual Team Team { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("TeamBrief")]
        public virtual User User { get; set; }
    }
}
