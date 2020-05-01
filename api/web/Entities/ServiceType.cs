using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dta.Marketplace.Api.Web.Entities
{
    [Table("service_type")]
    public partial class ServiceType
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("category_id")]
        public int CategoryId { get; set; }
        [Required]
        [Column("name", TypeName = "character varying")]
        public string Name { get; set; }
        [Column("framework_id")]
        public long FrameworkId { get; set; }
        [Column("lot_id")]
        public long LotId { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
        [Column("fee_type", TypeName = "character varying")]
        public string FeeType { get; set; }
    }
}
