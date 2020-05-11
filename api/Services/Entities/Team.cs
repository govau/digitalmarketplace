using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dta.Marketplace.Api.Services.Entities
{
    [Table("team")]
    public partial class Team
    {
        public Team()
        {
            TeamBrief = new HashSet<TeamBrief>();
            TeamMember = new HashSet<TeamMember>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("name", TypeName = "character varying")]
        public string Name { get; set; }
        [Column("email_address", TypeName = "character varying")]
        public string EmailAddress { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [InverseProperty("Team")]
        public virtual ICollection<TeamBrief> TeamBrief { get; set; }
        [InverseProperty("Team")]
        public virtual ICollection<TeamMember> TeamMember { get; set; }
    }
}
