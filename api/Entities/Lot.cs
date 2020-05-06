using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dta.Marketplace.Api.Entities
{
    [Table("lot")]
    public partial class Lot
    {
        public Lot()
        {
            ArchivedService = new HashSet<ArchivedService>();
            Brief = new HashSet<Brief>();
            DraftService = new HashSet<DraftService>();
            FrameworkLot = new HashSet<FrameworkLot>();
            Service = new HashSet<Service>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("slug", TypeName = "character varying")]
        public string Slug { get; set; }
        [Required]
        [Column("name", TypeName = "character varying")]
        public string Name { get; set; }
        [Column("one_service_limit")]
        public bool OneServiceLimit { get; set; }
        [Column("data", TypeName = "json")]
        public string Data { get; set; }

        [InverseProperty("Lot")]
        public virtual ICollection<ArchivedService> ArchivedService { get; set; }
        [InverseProperty("Lot")]
        public virtual ICollection<Brief> Brief { get; set; }
        [InverseProperty("Lot")]
        public virtual ICollection<DraftService> DraftService { get; set; }
        [InverseProperty("Lot")]
        public virtual ICollection<FrameworkLot> FrameworkLot { get; set; }
        [InverseProperty("Lot")]
        public virtual ICollection<Service> Service { get; set; }
    }
}
