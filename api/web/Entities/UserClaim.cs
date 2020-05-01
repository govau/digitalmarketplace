using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dta.Marketplace.Api.Web.Entities
{
    [Table("user_claim")]
    public partial class UserClaim
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("email_address", TypeName = "character varying")]
        public string EmailAddress { get; set; }
        [Required]
        [Column("token", TypeName = "character varying")]
        public string Token { get; set; }
        [Column("data", TypeName = "json")]
        public string Data { get; set; }
        [Column("claimed")]
        public bool Claimed { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}
