using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dta.Marketplace.Api.Services.Entities
{
    [Table("user_framework")]
    public partial class UserFramework
    {
        [Key]
        [Column("user_id")]
        public int UserId { get; set; }
        [Key]
        [Column("framework_id")]
        public int FrameworkId { get; set; }

        [ForeignKey(nameof(FrameworkId))]
        [InverseProperty("UserFramework")]
        public virtual Framework Framework { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("UserFramework")]
        public virtual User User { get; set; }
    }
}
