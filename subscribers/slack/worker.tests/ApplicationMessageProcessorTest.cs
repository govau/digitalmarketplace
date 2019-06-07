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
    
    public class ApplicationMessageProcessorTest {
        [Fact]
        public async void CanProcessApplicationMessage() {
            //Given
            var supplier_slack_url = "http://application.slack.url";            
            var message = 
$@"*A new seller has started an application*
Application Id: 321
test supplier (test@supplier.com)";

            var logger = new Mock<ILogger<AppService>>();

            var config = new Mock<IOptions<AppConfig>>();
            config.Setup(ac => ac.Value).Returns(new AppConfig {
                SupplierSlackUrl = supplier_slack_url
            });
            var slackService = new Mock<ISlackService>();
            slackService.Setup(ss => ss.SendSlackMessage(supplier_slack_url, message)).ReturnsAsync(true);
            var applicationMessageProcessor = new ApplicationMessageProcessor(logger.Object, config.Object, slackService.Object);

            //When
            var awsSnsMessage = new AwsSnsMessage {
                Message = JsonConvert.SerializeObject(new {
                    name = "test supplier",
                    email_address = "test@supplier.com",
                    application = new {
                        id = 321,
                        type = "new",
                        supplier_code = 123
                    }
                }),
                MessageAttributes = new MessageAttributes {
                    EventType = new TType {
                        Value = "created"
                    }
                }
            };
            var result = await applicationMessageProcessor.Process(awsSnsMessage);

            //Then
            slackService.Verify((ss) => ss.SendSlackMessage(supplier_slack_url, message), Times.Exactly(1));
            Assert.True(result, "created should return true");
        }

        [Fact]
        public async void CanProcessApplicationMessageWithNullSupplierCode() {
            //Given
            var supplier_slack_url = "http://application.slack.url";            
            var message = 
$@"*A new seller has started an application*
Application Id: 321
test supplier (test@supplier.com)";

            var logger = new Mock<ILogger<AppService>>();

            var config = new Mock<IOptions<AppConfig>>();
            config.Setup(ac => ac.Value).Returns(new AppConfig {
                SupplierSlackUrl = supplier_slack_url
            });
            var slackService = new Mock<ISlackService>();
            slackService.Setup(ss => ss.SendSlackMessage(supplier_slack_url, message)).ReturnsAsync(true);
            var applicationMessageProcessor = new ApplicationMessageProcessor(logger.Object, config.Object, slackService.Object);

            //When
            var awsSnsMessage = new AwsSnsMessage {
                Message = JsonConvert.SerializeObject(new {
                    name = "test supplier",
                    email_address = "test@supplier.com",
                    application = new {
                        id = 321,
                        type = "new",
                        supplier_code = (int?)null
                    }
                }),
                MessageAttributes = new MessageAttributes {
                    EventType = new TType {
                        Value = "created"
                    }
                }
            };
            var result = await applicationMessageProcessor.Process(awsSnsMessage);

            //Then
            slackService.Verify((ss) => ss.SendSlackMessage(supplier_slack_url, message), Times.Exactly(1));
            Assert.True(result, "created should return true");
            
        }
    }
}
