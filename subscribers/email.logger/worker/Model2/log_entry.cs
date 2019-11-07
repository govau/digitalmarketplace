using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dta.Marketplace.Subscribers.Email.Logger.Worker{
    [Table("log_entry")]

    public partial class LogEntry{
        [Column("id")]
        public int Id { get; set; }

        [Column("notification_type")]
        public string NotificationType{get; set; }

        [Column("message_id")]
        public string MessageId{ get; set; }

        [Column("date_time_sent")]
        public DateTime DateTimeSent {get; set;}

        [Column("date", TypeName = "jsonb")]
        public string Data {get; set; }

        [Column("Subject")]
        public string Subject {get; set; }

    //     [Column("Body")]
    //     public AppBodyService Body {get; set; }
    }
}
