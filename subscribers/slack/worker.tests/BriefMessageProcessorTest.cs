using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Xunit;
using Dta.Marketplace.Subscribers.Slack.Worker;
using Dta.Marketplace.Subscribers.Slack.Worker.Model;
using Dta.Marketplace.Subscribers.Slack.Worker.Processors;
using Dta.Marketplace.Subscribers.Slack.Worker.Services;

namespace Dta.Marketplace.Subscribers.Slack.Worker.Tests {
    public class BriefMessageProcessorTest {
        [Fact]
        public async void CanProcessBriefMessage() {
            //Given
            var buyer_slack_url = "http://brief.slack.url";
            var message = 
$@"*A buyer has published a new opportunity*
test brief (atm)
dta
By: me (me@dta)
http://brief.url";

            var logger = new Mock<ILogger<AppService>>();

            var config = new Mock<IOptions<AppConfig>>();
            config.Setup(ac => ac.Value).Returns(new AppConfig {
                BuyerSlackUrl = buyer_slack_url
            });
            var slackService = new Mock<ISlackService>();
            slackService.Setup(ss => ss.SendSlackMessage(buyer_slack_url, message)).ReturnsAsync(true);
            var briefMessageProcessor = new BriefMessageProcessor(logger.Object, config.Object, slackService.Object);

            //When
            var awsSnsMessage = new AwsSnsMessage {
                Message = JsonConvert.SerializeObject(new {
                    brief = new {
                        title = "test brief",
                        organisation = "dta",
                        lotName = "atm"
                    },
                    name = "me",
                    email_address = "me@dta",
                    url = "http://brief.url"
                }),
                MessageAttributes = new MessageAttributes {
                    EventType = new TType {
                        Value = "published"
                    }
                }
            };
            var result = await briefMessageProcessor.Process(awsSnsMessage);

            //Then
            slackService.Verify((ss) => ss.SendSlackMessage(buyer_slack_url, message), Times.Exactly(1));
            Assert.True(result);
        }
    }
}
