using Newtonsoft.Json;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Amazon.SQS.Model;
using Dta.Marketplace.Subscribers.Email.Logger.Worker.Model;


namespace Dta.Marketplace.Subscribers.Email.Logger.Worker.Processors {
    public abstract class AbstractEmailLogProcessor : IEmailLogProcessor {
        protected readonly ILogger _logger;
        protected readonly IOptions<AppConfig> _config;

        public AbstractEmailLogProcessor(ILogger<AppService> logger, IOptions<AppConfig> config) {
            _logger = logger;
            _config = config;
        }

        public bool ProcessMessage(AwsSqsMessage awsSqsMessage) {
            return Process(awsSqsMessage);
        }

        public abstract bool Process(AwsSqsMessage awsSqsMessage);
    }
}

