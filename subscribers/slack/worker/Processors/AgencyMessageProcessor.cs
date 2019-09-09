using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Dta.Marketplace.Subscribers.Slack.Worker.Model;
using Dta.Marketplace.Subscribers.Slack.Worker.Services;
using System.Collections.Generic;

namespace Dta.Marketplace.Subscribers.Slack.Worker.Processors {
    internal class AgencyMessageProcessor : AbstractMessageProcessor {
        private readonly ISlackService _slackService;

        public AgencyMessageProcessor(ILogger<AppService> logger, IOptions<AppConfig> config, ISlackService slackService) : base(logger, config) {
            _slackService = slackService;
        }

        public async override Task<bool> Process(AwsSnsMessage awsSnsMessage) {
            switch (awsSnsMessage.MessageAttributes.EventType.Value) {
                case "created":
                    var definition = new {
                        agency = new {
                            id = default(int),
                            name = "",
                            domains = ""
                        }
                    };
                    var message = JsonConvert.DeserializeAnonymousType(awsSnsMessage.Message, definition);
                    
                    var slackMessage =
$@":rotating_light:*A new agency was created*:rotating_light:
id: {message.agency.id}
name: {message.agency.name}
domains: {message.agency.domains}
Please update this record accordingly";

                    return await _slackService.SendSlackMessage(_config.Value.AgencySlackUrl, slackMessage);
                default:
                        _logger.LogDebug("Unknown processor for {@AwsSnsMessage}.", awsSnsMessage);
                    break;
            }
            return true;
        }
    }
}
