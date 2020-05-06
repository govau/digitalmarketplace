using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dta.Marketplace.Api.Entities
{
    [Table("agency_domain")]
    public partial class AgencyDomain
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("agency_id")]
        public int AgencyId { get; set; }
        [Required]
        [Column("domain", TypeName = "character varying")]
        public string Domain { get; set; }
        [Column("active")]
        public bool Active { get; set; }

        [ForeignKey(nameof(AgencyId))]
        [InverseProperty("AgencyDomain")]
        public virtual Agency Agency { get; set; }
    }
}
