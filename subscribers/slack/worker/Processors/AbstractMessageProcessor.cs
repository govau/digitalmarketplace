using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Amazon.SQS.Model;
using Dta.Marketplace.Subscribers.Slack.Worker.Model;


namespace Dta.Marketplace.Subscribers.Slack.Worker.Processors {
    internal abstract class AbstractMessageProcessor : IMessageProcessor {
        protected readonly ILogger _logger;
        protected readonly IOptions<AppConfig> _config;
        
        public AbstractMessageProcessor(ILogger<AppService> logger, IOptions<AppConfig> config) {
            _logger = logger;
            _config = config;
        }

        public async Task<bool> ProcessMessage(AwsSnsMessage awsSnsMessage) {
            return await Process(awsSnsMessage);
        }

        public abstract Task<bool> Process(AwsSnsMessage awsSnsMessage);
    }
}
