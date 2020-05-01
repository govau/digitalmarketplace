using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dta.Marketplace.Api.Web.Entities
{
    [Table("signed_agreement")]
    public partial class SignedAgreement
    {
        [Key]
        [Column("agreement_id")]
        public int AgreementId { get; set; }
        [Key]
        [Column("user_id")]
        public int UserId { get; set; }
        [Key]
        [Column("signed_at")]
        public DateTime SignedAt { get; set; }
        [Column("application_id")]
        public long? ApplicationId { get; set; }
        [Column("supplier_code")]
        public long? SupplierCode { get; set; }

        [ForeignKey(nameof(AgreementId))]
        [InverseProperty(nameof(MasterAgreement.SignedAgreement))]
        public virtual MasterAgreement Agreement { get; set; }
        [ForeignKey(nameof(ApplicationId))]
        [InverseProperty("SignedAgreement")]
        public virtual Application Application { get; set; }
        public virtual Supplier SupplierCodeNavigation { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("SignedAgreement")]
        public virtual User User { get; set; }
    }
}
