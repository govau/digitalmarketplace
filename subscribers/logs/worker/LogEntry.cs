using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dta.Marketplace.Subscribers.Logger.Worker {
    [Table("log_entry")]
    public partial class LogEntry {
        [Column("id")]
        public long Id { get; set; }

        [Column("data", TypeName = "jsonb")]
        public string Data { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
