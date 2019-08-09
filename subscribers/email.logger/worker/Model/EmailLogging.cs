using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace Dta.Marketplace.Subscribers.Email.Logger.Worker
{
    public partial class EmailLogging
    {
        [Column("id")]
        public int Id { get; set; }
        
        [Column("notification_type")]
        public string Notification_Type { get; set; }
        
        [Column("message_id")]
        public string Message_Id { get; set; }
        
        [Column("date_time_sent")]
        public DateTime DateTimeSent { get; set; }

        [Column("data", TypeName="jsonb")]
        public string Data { get; set; }

        [Column("subject")]
        public string Subject {get; set;}
    }
}
