using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dta.Marketplace.Api.Entities
{
    [Table("audit_event")]
    public partial class AuditEvent
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("type", TypeName = "character varying")]
        public string Type { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        [Column("user", TypeName = "character varying")]
        public string User { get; set; }
        [Column("data", TypeName = "json")]
        public string Data { get; set; }
        [Column("object_type", TypeName = "character varying")]
        public string ObjectType { get; set; }
        [Column("object_id")]
        public long? ObjectId { get; set; }
        [Column("acknowledged")]
        public bool Acknowledged { get; set; }
        [Column("acknowledged_by", TypeName = "character varying")]
        public string AcknowledgedBy { get; set; }
        [Column("acknowledged_at")]
        public DateTime? AcknowledgedAt { get; set; }
    }
}
