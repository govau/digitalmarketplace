using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dta.Marketplace.Api.Services.Entities
{
    [Table("council")]
    public partial class Council
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("name", TypeName = "character varying")]
        public string Name { get; set; }
        [Required]
        [Column("domain", TypeName = "character varying")]
        public string Domain { get; set; }
        [Column("home_page", TypeName = "character varying")]
        public string HomePage { get; set; }
    }
}
