using System;
using System.Collections.Generic;

namespace Dta.Marketplace.Subscribers.Email.Logger.Worker
{
    public partial class EmailLogging
    {
        public int Id { get; set; }

        public string Notification_Type { get; set; }

        public string Message_Id { get; set; }
        public DateTime DateTimeSent { get; set; }
        public string Data { get; set; }
        public string Subject {get; set;}
    }
}
