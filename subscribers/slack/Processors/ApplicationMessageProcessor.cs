using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Dta.Marketplace.Subscribers.Slack.Model;
using Dta.Marketplace.Subscribers.Slack.Services;


namespace Dta.Marketplace.Subscribers.Slack.Processors {
    internal class ApplicationMessageProcessor : AbstractMessageProcessor {
        private readonly ISlackService _slackService;

        public ApplicationMessageProcessor(ILogger<AppService> logger, IOptions<AppConfig> config, ISlackService slackService) : base(logger, config) {
            _slackService = slackService;
        }

        public async override Task<bool> Process(AwsSnsMessage awsSnsMessage) {
            switch (awsSnsMessage.MessageAttributes.EventType.Value) {
                case "created":
                    var definition = new {
                        name = "",
                        email_address = "",
                        application = new {
                            id = -1,
                            name = "",
                            status = "",
                            type = "",
                            supplier_code = -1
                        }
                    };
                    var message = JsonConvert.DeserializeAnonymousType(awsSnsMessage.Message, definition);
                    string subject = null;
                    if (message.application.type == "edit") {
                        subject = "An existing seller has started a new application";
                    } else if (message.application.type == "new") {
                        subject = "A new seller has started an application";
                    }
                    if (string.IsNullOrEmpty(subject) == false) {
                        var slackMessage =
$@"*{subject}*
Application Id: {message.application.id}
{message.name} ({message.email_address})";

                        return await _slackService.SendSlackMessage(_config.Value.SupplierSlackUrl, slackMessage);

                    } else {
                        _logger.LogError("Unknown application type for {@AwsSnsMessage}.", awsSnsMessage);
                    }
                    break;
                default:
                        _logger.LogDebug("Unknown processor for {@AwsSnsMessage}.", awsSnsMessage);
                    break;
            }
            return true;
        }
    }
}
