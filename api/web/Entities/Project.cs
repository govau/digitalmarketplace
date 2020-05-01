using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dta.Marketplace.Api.Web.Entities
{
    [Table("project")]
    public partial class Project
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("data", TypeName = "json")]
        public string Data { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}
