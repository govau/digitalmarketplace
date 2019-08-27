using System;
using Amazon.SQS.Model;

namespace Dta.Marketplace.Subscribers.Logger.Worker {
    public class MessageProcessor : Exception, IMessageProcessor {
        private readonly LoggerContext _loggerContext;

        public MessageProcessor(LoggerContext loggerContext) {
            _loggerContext = loggerContext;
        }

        public void Process(Message message) {
            _loggerContext.LogEntry.Add(
                new LogEntry {
                    Data = message.Body,
                    CreatedAt = DateTime.Now
                }
            );
            _loggerContext.SaveChanges();
        }
    }
}
