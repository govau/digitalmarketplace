using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.SQS;
using Amazon.SQS.Model;
using Dta.Marketplace.Subscribers.Email.Logger.Worker;
using Dta.Marketplace.Subscribers.Email.Logger.Worker.Model;
using Dta.Marketplace.Subscribers.Email.Logger.Worker.Processors;
using Dta.Marketplace.Subscribers.Email.Logger.Worker.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace Dta.Marketplace.Subscribers.Email.Logger.Worker.Tests {
    public class EmailBounceNotificationProcessorTest {
        [Fact]
        public void CanProcessEmailBounceNotificationMessage() {
            //Given
            var bouncedRecipients = new List<dynamic>() {
                new {
                emailAddress = "bounce@gmail.com",
                action = "failed",
                status = "5.1.1",
                diagnosticCode = "smtp: 0-1.1.1"
                }
            };
            var bounceRecipientsToString = "";
            for (var index = 0; index < bouncedRecipients.Count; index++) {
                bounceRecipientsToString += $"{bouncedRecipients[index]}";
            }
            Dictionary<string, string> dataDictToBeStored = new Dictionary<string, string>() {

                { "NotificationBodyMessageId", "0000000000000000-00000000-0000-0000-0000-000000000000-000000" }, { "NotificationBodyTopicARN", "NotificationTestBodyTopicARN" }, { "NotificationBodyType", "Bounce" }, { "NotificationBodyTimestamp", "1561095094474" }, { "NotificationBodyMailSource", "Example Agency <No-reply@example.com>" }, { "NotificationBodyCommonHeadersSubject", "ExampleSubject" }, { "NotificationBodyBounceType", "Permanent" }, { "NotificationBodyBounceSubType", "Suppressed" }, { "NotificationBodyBounceTimestamp", "1561095094474" }, { "NotificationBodyBounceSMTPResponse", "250 2.0.0 Ok:123456789123" }, { "NotificationBodyBounceRemoteMTAIp", "111.22.333.444" }, { "NotificationBodyBounceReportingMTA", "ExampleMTA" }, { "NotificationBounceBouncedRecipients", bounceRecipientsToString }, { "NotificationBodyCommonHeadersFrom", "Example Company Admin <no-reply@example.com>" }, { "NotificationBodyCommonHeadersTo", "bounce@gmail.com" }, { "NotificationBodyCommonHeadersReplyTo", "no-reply@example.com" }, { "NotificationBodyDestination", "bounce@gmail.com" },

            };
            var logger = new Mock<ILogger<AppService>>();
            var config = new Mock<IOptions<AppConfig>>();
            var saveEmailNotificationService = new Mock<IEmailService>();
            saveEmailNotificationService.Setup(sens => sens.SaveEmailMessage(It.IsAny<Dictionary<string, string>>())).Returns(true);
            var emailBounceNotificationLogProcessor = new EmailBounceNotificationProcessor(logger.Object, config.Object, saveEmailNotificationService.Object);

            //When
            var awsSqsMessage = new AwsSqsMessage() {
                Body = JsonConvert.SerializeObject(new {
                    Type = "Notification",
                    Timestamp = "1561095094474",
                    MessageId = "0000000000000000-00000000-0000-0000-0000-000000000000-000000",
                    TopicArn = "NotificationTestBodyTopicARN",
                    Message = JsonConvert.SerializeObject(new {
                        NotificationType = "Bounce",
                        Mail = new {
                            TimeStamp = "1561095094474",
                            Source = "Example Agency <No-reply@example.com",
                            MessageId = "0000000000000000-00000000-0000-0000-0000-000000000000-000000",
                            Destination = new List<string>() {
                                "bounce@gmail.com"
                            },
                            CommonHeaders = new {
                                ReturnPath = "",
                                From = new List<string>() {
                                    "Example Company Admin <no-reply@example.com>",
                                },
                                ReplyTo = new List<string>() {
                                    "no-reply@example.com"
                                },
                                To = new List<string>() {
                                    "bounce@gmail.com"
                                },
                                Subject = "ExampleSubject",
                            }
                        },
                        Bounce = new {
                            BounceType = "Permanent",
                            BounceSubType = "Suppressed",
                            BouncedRecipients = new List<dynamic>() {
                                new {
                                    EmailAddress = "bounce@gmail.com",
                                    Action = "failed",
                                    Status = "5.1.1",
                                    DiagnosticCode = "smtp: 0-1.1.1"
                                }
                            },
                            Timestamp = "1561095094474",
                            smtpResponse = "250 2.0.0 Ok:123456789123",
                            remoteMtaIp = "111.22.333.444",
                            reportingMTA = "ExampleMTA"
                        },
                    }),
                }),
                MessageId = "0000000000000000-00000000-0000-0000-0000-000000000000-000000",
            };
            var result = emailBounceNotificationLogProcessor.Process(awsSqsMessage);

            //Then
            saveEmailNotificationService.Verify((sens) => sens.SaveEmailMessage(It.IsAny<Dictionary<string, string>>()), Times.Exactly(1));
            Assert.True(result, "created should return true");
        }
    }
}

