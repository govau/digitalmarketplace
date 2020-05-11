using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dta.Marketplace.Api.Services.Entities
{
    [Table("api_key")]
    public partial class ApiKey
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [Required]
        [Column("key")]
        [StringLength(64)]
        public string Key { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        [Column("revoked_at")]
        public DateTime? RevokedAt { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty("ApiKey")]
        public virtual User User { get; set; }
    }
}
