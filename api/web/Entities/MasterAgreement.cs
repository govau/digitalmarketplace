using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dta.Marketplace.Api.Web.Entities
{
    [Table("master_agreement")]
    public partial class MasterAgreement
    {
        public MasterAgreement()
        {
            SignedAgreement = new HashSet<SignedAgreement>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("start_date")]
        public DateTime StartDate { get; set; }
        [Column("end_date")]
        public DateTime EndDate { get; set; }
        [Column("data", TypeName = "json")]
        public string Data { get; set; }

        [InverseProperty("Agreement")]
        public virtual ICollection<SignedAgreement> SignedAgreement { get; set; }
    }
}
