using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dta.Marketplace.Api.Web.Entities
{
    [Table("service_category")]
    public partial class ServiceCategory
    {
        public ServiceCategory()
        {
            ServiceRole = new HashSet<ServiceRole>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("name", TypeName = "character varying")]
        public string Name { get; set; }
        [Required]
        [Column("abbreviation", TypeName = "character varying")]
        public string Abbreviation { get; set; }

        [InverseProperty("Category")]
        public virtual ICollection<ServiceRole> ServiceRole { get; set; }
    }
}
