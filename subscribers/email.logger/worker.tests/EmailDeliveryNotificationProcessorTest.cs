using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xunit;
using Amazon.SQS;
using Amazon.SQS.Model;
using Dta.Marketplace.Subscribers.Email.Logger.Worker;
using Dta.Marketplace.Subscribers.Email.Logger.Worker.Model;
using Dta.Marketplace.Subscribers.Email.Logger.Worker.Processors;
using Dta.Marketplace.Subscribers.Email.Logger.Worker.Services;

namespace Dta.Marketplace.Subscribers.Email.Logger.Worker.Tests {
    public class EmailDeliveryNotificationProcessorTest {
        [Fact]
        public void CanProcessEmailDeliveryNotificationMessage() {
            //Given
            
            Dictionary <string, string> dataDictToBeStored = new Dictionary<string, string>() {
                { "NotificationBodyMessageId", "0000000000000000-00000000-0000-0000-0000-000000000000-000000" }, 
                { "NotificationBodyTopicARN", "NotificationTestBodyTopicARN" }, 
                { "NotificationBodyType", "Delivery" }, 
                { "NotificationBodyTimestamp", "1561095094474" }, 
                { "NotificationBodyMailSource", "Example Agency <No-reply@example.com>" }, 
                { "NotificationBodyCommonHeadersSubject", "ExampleSubject" },
                { "NotificationBodyDeliveryTimestamp", "Permanent" },
                { "NotificationBodyDeliveryProcessingTimeMs", "1234" },
                { "NotificationBodyDeliverySMTPResponse", "250 2.0.0 Ok: queued as 123456789123" },
                { "NotificationBodyDeliveryRemoteMtaIp", "111.22.333.444" },
                { "NotificationBodyDeliveryReportingMTA", "ExampleMTA" },
                { "Recipients", "example@example.com"},
                { "NotificationBodyCommonHeadersFrom", "Example Company Admin <no-reply@example.com>"},
                { "NotificationBodyCommonHeadersTo", "example@example.com"},
                { "NotificationBodyCommonHeadersReplyTo", "no-reply@example.com"},
                { "NotificationBodyDestination", "example@example.com"},
            };
            var logger = new Mock<ILogger<AppService>>();
            var config = new Mock<IOptions<AppConfig>>();
            var saveEmailNotificationService = new Mock<IEmailService>();
            saveEmailNotificationService.Setup(sens => sens.SaveEmailMessage(It.IsAny<Dictionary<string, string>>())).Returns(true);
            var emailDeliveryNotificationLogProcessor = new EmailDeliveryNotificationProcessor(logger.Object, config.Object, saveEmailNotificationService.Object);

            //When
            var awsSqsMessage = new AwsSqsMessage() {
                Body = JsonConvert.SerializeObject(new {
                    Type = "Notification",
                    Timestamp = "1561095094474",
                    MessageId = "0000000000000000-00000000-0000-0000-0000-000000000000-000000",
                    TopicArn = "NotificationTestBodyTopicARN",
                    Message =  JsonConvert.SerializeObject(new {
                        NotificationType = "Delivery",
                        Mail = new {
                            Timestamp= "1561095094474",
                            Source = "Example Agency <No-reply@example.com>",
                            MessageId = "0000000000000000-00000000-0000-0000-0000-000000000000-000000",
                            Destination = new List <string> () {
                                "Email_Body_Log"
                            },
                            CommonHeaders = new {
                                ReturnPath = "",
                                From = new List<string>() {
                                    "Example Company Admin <no-reply@example.com>",
                                },
                                ReplyTo = new List<string>(){
                                    "no-reply@example.com"
                                },
                                To = new List<string>(){
                                    "example@example.com"
                                },
                                Subject = "ExampleSubject",
                            },
                        },
                        Delivery = new {
                            TimeStamp = "1561095094474",
                            ProcessingTimeMillis = "1234",
                            SmtpResponse = "250 2.0.0 Ok: queued as 123456789123",
                            RemoteMtaIp = "111.22.333.444",
                            ReportingMTA = "ExampleMTA",
                            Recipients = new List<dynamic>(){
                                "example@example.com",
                            }
                        }
                    }),
                }),
                MessageId = "0000000000000000-00000000-0000-0000-0000-000000000000-000000"
            };
            var result = emailDeliveryNotificationLogProcessor.Process(awsSqsMessage);

            //Then
            saveEmailNotificationService.Verify((sens) => sens.SaveEmailMessage(It.IsAny<Dictionary<string, string>>()), Times.Exactly(1));
            Assert.True(result, "created should return true");
        }
    }
}
