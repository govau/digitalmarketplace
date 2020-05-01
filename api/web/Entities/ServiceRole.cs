using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dta.Marketplace.Api.Web.Entities
{
    [Table("service_role")]
    public partial class ServiceRole
    {
        public ServiceRole()
        {
            PriceSchedule = new HashSet<PriceSchedule>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("category_id")]
        public int CategoryId { get; set; }
        [Required]
        [Column("name", TypeName = "character varying")]
        public string Name { get; set; }
        [Required]
        [Column("abbreviation", TypeName = "character varying")]
        public string Abbreviation { get; set; }

        [ForeignKey(nameof(CategoryId))]
        [InverseProperty(nameof(ServiceCategory.ServiceRole))]
        public virtual ServiceCategory Category { get; set; }
        [InverseProperty("ServiceRole")]
        public virtual ICollection<PriceSchedule> PriceSchedule { get; set; }
    }
}
