using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Amazon.SQS.Model;

namespace Dta.Marketplace.Subscribers.Logger.Worker
{
    public class MessageProcessor: Exception, IMessageProcessor
    {
        private readonly ILoggerAdapter<AppService> _logger; 
        private readonly LoggerContext _loggerContext;

        public MessageProcessor(ILoggerAdapter<AppService> logger, LoggerContext loggerContext){ 
            _logger = logger;
            _loggerContext = loggerContext;
        }
        public void Process(Message message) {
            try {
                _logger.LogInformation(message.Body);
                _loggerContext.LogEntry.Add(new LogEntry { Data = message.Body });
                _loggerContext.SaveChanges();
            }
            catch(Exception ex) {
                _logger.LogError("Unable to process", ex);
            }
        }
    }
}