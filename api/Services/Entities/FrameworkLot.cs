using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dta.Marketplace.Api.Services.Entities
{
    [Table("framework_lot")]
    public partial class FrameworkLot
    {
        public FrameworkLot()
        {
            ArchivedService = new HashSet<ArchivedService>();
            Brief = new HashSet<Brief>();
            DraftService = new HashSet<DraftService>();
            Service = new HashSet<Service>();
        }

        [Key]
        [Column("framework_id")]
        public int FrameworkId { get; set; }
        [Key]
        [Column("lot_id")]
        public int LotId { get; set; }

        [ForeignKey(nameof(FrameworkId))]
        [InverseProperty("FrameworkLot")]
        public virtual Framework Framework { get; set; }
        [ForeignKey(nameof(LotId))]
        [InverseProperty("FrameworkLot")]
        public virtual Lot Lot { get; set; }
        [InverseProperty("FrameworkLot")]
        public virtual ICollection<ArchivedService> ArchivedService { get; set; }
        [InverseProperty("FrameworkLot")]
        public virtual ICollection<Brief> Brief { get; set; }
        [InverseProperty("FrameworkLot")]
        public virtual ICollection<DraftService> DraftService { get; set; }
        [InverseProperty("FrameworkLot")]
        public virtual ICollection<Service> Service { get; set; }
    }
}
