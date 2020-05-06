using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dta.Marketplace.Api.Entities
{
    [Table("insight")]
    public partial class Insight
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("data", TypeName = "json")]
        public string Data { get; set; }
        [Column("published_at")]
        public DateTime PublishedAt { get; set; }
        [Column("active")]
        public bool Active { get; set; }
    }
}
