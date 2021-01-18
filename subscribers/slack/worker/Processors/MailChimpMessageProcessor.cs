using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Dta.Marketplace.Subscribers.Slack.Worker.Model;
using Dta.Marketplace.Subscribers.Slack.Worker.Services;

namespace Dta.Marketplace.Subscribers.Slack.Worker.Processors {
    internal class MailchimpMessageProcessor : AbstractMessageProcessor {
        private readonly ISlackService _slackService;

        public MailchimpMessageProcessor(ILogger<AppService> logger, IOptions<AppConfig> config, ISlackService slackService) : base(logger, config) {
            _slackService = slackService;
        }

        public async override Task<bool> Process(AwsSnsMessage awsSnsMessage) {
            switch (awsSnsMessage.MessageAttributes.EventType.Value) {
                case "mailchimp":
                    var definition = new {
                        mailchimp = new {
                            message = "",
                            error = ""
                        },
                    };
                    var message = JsonConvert.DeserializeAnonymousType(awsSnsMessage.Message, definition);
                    var slackMessage =
$@":email:*Mailchimp Error*:email:
Message: {message.mailchimp.message}
Error: {message.mailchimp.error}";

                    return await _slackService.SendSlackMessage(_config.Value.MailchimpSlackUrl, slackMessage);
                
                default:
                        _logger.LogDebug("Unknown processor for {@AwsSnsMessage}.", awsSnsMessage);
                    break;
            }
            return true;
        }
    }
}
