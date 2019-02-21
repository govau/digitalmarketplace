using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Dta.Marketplace.Subscriber.Slack.Model;
using Dta.Marketplace.Subscriber.Slack.Services;

namespace Dta.Marketplace.Subscriber.Slack.Processors {
    internal class BriefMessageProcessor : AbstractMessageProcessor {
        private readonly ISlackService _slackService;
        public BriefMessageProcessor(ILogger<AppService> logger, IOptions<AppConfig> config, ISlackService slackService) : base(logger, config) {
            _slackService = slackService;
        }
        public async override Task<bool> Process(AwsSnsMessage awsSnsMessage) {
            switch (awsSnsMessage.MessageAttributes.EventType.Value) {
                case "published":
                    var definition = new {
                        brief = new {
                            title = "",
                            organisation = "",
                            lotName = ""
                        },
                        name = "",
                        email_address = "",
                        url = ""
                    };
                    var message = JsonConvert.DeserializeAnonymousType(awsSnsMessage.Message, definition);
                    var slackMessage =
$@"*A buyer has published a new opportunity*
{message.brief.title} ({message.brief.lotName})
{message.brief.organisation}
By: {message.name} ({message.email_address})
{message.url})";

                    return await _slackService.SendSlackMessage(_config.Value.BUYER_SLACK_URL, slackMessage);
            }
            return true;
        }
    }
}
