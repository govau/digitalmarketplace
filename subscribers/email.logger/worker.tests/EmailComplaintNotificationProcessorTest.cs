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
    public class EmailComplaintNotificationProcessorTest {
        [Fact]
        public void CanProcessEmailComplaintNotificationMessage() {
            //Given
            var complainedRecipients = new List<dynamic>() {
                                new {
                                    emailAddress = "complaint@complaint.com", 
                                }
            };
            
            Dictionary <string, string> dataDictToBeStored = new Dictionary<string, string>() {
                
                { "NotificationBodyMessageId", "0000000000000000-00000000-0000-0000-0000-000000000000-000000" }, 
                { "NotificationBodyTopicARN", "NotificationTestBodyTopicARN" }, 
                { "NotificationBodyType", "Complaint" }, 
                { "NotificationBodyTimestamp", "1561095094474" }, 
                { "NotificationBodyMailSource", "Example Agency <No-reply@example.com>" }, 
                { "NotificationBodyCommonHeadersSubject", "ExampleSubject" },
                { "NotificationBodyComplaintUserAgent", "AWS" },
                { "NotificationBodyComplaintFeedbackType", "Suppressed" },
                { "NotificationBodyComplaintFeedbackId", "1561095094474" },
                { "NotificationBodyComplaintTimestamp", "250 2.0.0 Ok:123456789123" },
                { "NotificationBodyCommonHeadersFrom", "Example Company Admin <no-reply@example.com>"},
                { "NotificationBodyCommonHeadersTo", "complaint@complaint.com"},
                { "NotificationBodyCommonHeadersReplyTo", "no-reply@example.com"},
                { "NotificationBodyDestination", "complaint@complaint.com"},
                    
            };
            var logger = new Mock<ILogger<AppService>>();
            var config = new Mock<IOptions<AppConfig>>();
            var saveEmailNotificationService = new Mock<IEmailService>();
            saveEmailNotificationService.Setup(sens => sens.SaveEmailMessage(It.IsAny<Dictionary<string, string>>())).Returns(true);
            var emailComplaintNotificationLogProcessor = new EmailComplaintNotificationProcessor(logger.Object, config.Object, saveEmailNotificationService.Object);

            //When
            var awsSqsMessage = new AwsSqsMessage() 
            {
                Body = JsonConvert.SerializeObject(new 
                {
                    Type = "Notification",
                    Timestamp = "1561095094474",
                    MessageId = "0000000000000000-00000000-0000-0000-0000-000000000000-000000",
                    TopicArn = "NotificationTestBodyTopicARN",
                    Message =  JsonConvert.SerializeObject(new 
                    {
                        notificationType = "Bounce",
                        mail = new 
                        {
                            timestamp= "1561095094474",
                            source = "Example Agency <No-reply@example.com",
                            messageId = "0000000000000000-00000000-0000-0000-0000-000000000000-000000",
                            destination = new List<string>() {
                                "complaint@complaint.com"
                            },
                            commonHeaders = new {
                                returnPath = "",
                                from = new List<string>() {
                                    "Example Company Admin <no-reply@example.com>",
                                },
                                replyTo = new List<string>(){
                                    "no-reply@example.com"
                                },
                                to = new List<string>(){
                                    "complaint@complaint.com"
                                },
                                subject = "ExampleSubject",
                            }
                        },
                        complaint = new 
                        {
                            userAgent = "AWS",
                            complainedRecipients = new List<dynamic>() {
                                new {
                                    emailAddress = "complaint@complaint.com", 
                                }
                            },
                            complaintFeedbackType = "abuse",
                            arrivalDate = "1561095094474",
                            timeStamp = "1561095094474",
                            feedbackId = "0000000000000000-00000000-0000-0000-0000-000000000000-000000"	
                        },
                    }),
                }),
                MessageId = "0000000000000000-00000000-0000-0000-0000-000000000000-000000",
            };
            var result = emailComplaintNotificationLogProcessor.Process(awsSqsMessage);

            //Then
            saveEmailNotificationService.Verify((sens) => sens.SaveEmailMessage(It.IsAny<Dictionary<string, string>>()), Times.Exactly(1));
            Assert.True(result, "created should return true");
        }

  
    }
}
