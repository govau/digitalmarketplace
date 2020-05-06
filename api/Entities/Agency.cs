using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dta.Marketplace.Api.Entities
{
    [Table("agency")]
    public partial class Agency
    {
        public Agency()
        {
            AgencyDomain = new HashSet<AgencyDomain>();
            User = new HashSet<User>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("name", TypeName = "character varying")]
        public string Name { get; set; }
        [Required]
        [Column("domain", TypeName = "character varying")]
        public string Domain { get; set; }
        [Column("category", TypeName = "character varying")]
        public string Category { get; set; }
        [Column("state", TypeName = "character varying")]
        public string State { get; set; }
        [Required]
        [Column("whitelisted")]
        public bool? Whitelisted { get; set; }
        [Column("reports")]
        public bool Reports { get; set; }

        [InverseProperty("Agency")]
        public virtual ICollection<AgencyDomain> AgencyDomain { get; set; }
        [InverseProperty("Agency")]
        public virtual ICollection<User> User { get; set; }
    }
}
