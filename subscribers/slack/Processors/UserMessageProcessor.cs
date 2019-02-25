using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Dta.Marketplace.Subscribers.Slack.Model;
using Dta.Marketplace.Subscribers.Slack.Services;
using System;
using System.Linq;

namespace Dta.Marketplace.Subscribers.Slack.Processors {
    internal class UserMessageProcessor : AbstractMessageProcessor {
        private readonly ISlackService _slackService;
        public UserMessageProcessor(ILogger<AppService> logger, IOptions<AppConfig> config, ISlackService slackService) : base(logger, config) {
            _slackService = slackService;
        }

        public async override Task<bool> Process(AwsSnsMessage awsSnsMessage) {
            switch (awsSnsMessage.MessageAttributes.EventType.Value) {
                case "created":
                    var definition = new {
                        user = new {
                            email_address = "",
                            role = ""
                        }
                    };
                    var message = JsonConvert.DeserializeAnonymousType(awsSnsMessage.Message, definition);
                    if (message.user.role == "buyer") {
                        var domain = message.user.email_address.Split("@").Last();
                        var slackMessage =
$@"*A new buyer has signed up*
Domain: {domain}";

                        return await _slackService.SendSlackMessage(_config.Value.USER_SLACK_URL, slackMessage);
                    } else {
                        _logger.LogWarning($"Not enough to create slack message: {awsSnsMessage.MessageAttributes.ObjectType.Value} {awsSnsMessage.MessageAttributes.EventType.Value}");
                    }
                    break;
                default:
                    _logger.LogInformation($"Unknown message. {awsSnsMessage.MessageAttributes.ObjectType.Value} {awsSnsMessage.MessageAttributes.EventType.Value}");
                    break;
            }
            return true;
        }
    }
}
