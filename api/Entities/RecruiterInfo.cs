using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dta.Marketplace.Api.Entities
{
    [Table("recruiter_info")]
    public partial class RecruiterInfo
    {
        public RecruiterInfo()
        {
            SupplierDomain = new HashSet<SupplierDomain>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("active_candidates", TypeName = "character varying")]
        public string ActiveCandidates { get; set; }
        [Required]
        [Column("database_size", TypeName = "character varying")]
        public string DatabaseSize { get; set; }
        [Required]
        [Column("placed_candidates", TypeName = "character varying")]
        public string PlacedCandidates { get; set; }
        [Required]
        [Column("margin", TypeName = "character varying")]
        public string Margin { get; set; }
        [Required]
        [Column("markup", TypeName = "character varying")]
        public string Markup { get; set; }

        [InverseProperty("RecruiterInfo")]
        public virtual ICollection<SupplierDomain> SupplierDomain { get; set; }
    }
}
