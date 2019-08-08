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
    public class UserMessageProcessorTest {
        [Fact]
        public async void CanProcessUserMessage() {
            //Given
            var user_slack_url = "http://user.slack.url";            
            var message = 
$@"*A new buyer has signed up*
Domain: b.com";

            var logger = new Mock<ILogger<AppService>>();

            var config = new Mock<IOptions<AppConfig>>();
            config.Setup(ac => ac.Value).Returns(new AppConfig {
                UserSlackUrl = user_slack_url
            });
            var slackService = new Mock<ISlackService>();
            slackService.Setup(ss => ss.SendSlackMessage(user_slack_url, message)).ReturnsAsync(true);
            var userMessageProcessor = new UserMessageProcessor(logger.Object, config.Object, slackService.Object);

            //When
            var awsSnsMessage = new AwsSnsMessage {
                Message = JsonConvert.SerializeObject(new {
                    user = new {
                        email_address = "a@b.com",
                        role = "buyer"
                    }
                }),
                MessageAttributes = new MessageAttributes {
                    EventType = new TType {
                        Value = "created"
                    }
                }
            };
            var result = await userMessageProcessor.Process(awsSnsMessage);

            //Then

            slackService.Verify((ss) => ss.SendSlackMessage(user_slack_url, message), Times.Exactly(1));
            Assert.True(result, "created should return true");
            
        }
    }
}