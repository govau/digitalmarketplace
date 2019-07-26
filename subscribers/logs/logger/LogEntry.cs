using System;
using System.Collections.Generic;

namespace Dta.Marketplace.Subscribers.Logger.Worker
{
    public partial class LogEntry
    {
        public int Id { get; set; }
        public string Data { get; set; }
        public TimeSpan CreatedAt { get; set; }
    }
}
