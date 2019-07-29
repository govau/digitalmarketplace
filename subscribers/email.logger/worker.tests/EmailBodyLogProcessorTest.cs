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
    public class EmailBodyLogProcessorTest {
        [Fact]
        public void CanProcessEmailBodyLogMessage() {
            //Given
            var logger = new Mock<ILogger<AppService>>();
            var config = new Mock<IOptions<AppConfig>>();
            var saveEmailBodyService = new Mock<IEmailBodyService>();

            Dictionary <string, string> dataDictToBeStored = new Dictionary<string, string>() {
                {"EmailMessageId", "0000000000000000-00000000-0000-0000-0000-000000000000-000000"},
                {"EmailBody", "EmailTestBody"},
                {"EmailSubject", "EmailTestBodySubject"},
                {"EmailTopicArn", "EmailTestBodyTopicARN"},
                {"EmailTimestamp", "1561095094474"},
                {"EmailResponseMetaDataHTTPCode", "200"},
                {"EmailResponseMetaDataRequestId", "AAAABBBBCCCCDDDDEEEE1"},
                {"EmailResponseMetaDataRetryAttempts", "1"},
            };

            saveEmailBodyService.Setup(sebs => sebs.SaveEmailBodyMessage(dataDictToBeStored)).Returns(true);
            
            //When
            var awsSqsMessage = new AwsSqsMessage() {
                Body = JsonConvert.SerializeObject(new {
                    Type = "Notification",
                    Timestamp = "1561095094474",
                    MessageId = "0000000000000000-00000000-0000-0000-0000-000000000000-000000",
                    TopicArn = "EmailTestBodyTopicARN",
                    Message =  JsonConvert.SerializeObject(new {
                         Email = JsonConvert.SerializeObject(new {
                             Body= "EmailTestBody",
                            Subject = "EmailTestBodySubject",
                            MessageId = "0000000000000000-00000000-0000-0000-0000-000000000000-000000",
                            notificationType ="Email_Body_Log",
                            EmailResponseMetaData = new {
                                MessageId = "0000000000000000-00000000-0000-0000-0000-000000000000-000000",
                                ResponseMetadata = new {
                                    RetryAttempts = 1,
                                    HTTPStatusCode = 200,
                                    RequestId = "AAAABBBBCCCCDDDDEEEE1",
                                }
                            }
                        })
                    }),
                }),
                MessageId = "0000000000000000-00000000-0000-0000-0000-000000000000-000000"
            };
            
            
            var emailBodyLogProcessor = new EmailBodyLogProcessor(logger.Object, config.Object, saveEmailBodyService.Object);
            
            
            saveEmailBodyService.Setup(sebs => sebs.SaveEmailBodyMessage(dataDictToBeStored)).Returns(true);
            var result = emailBodyLogProcessor.Process(awsSqsMessage);
                
            // //Then
            saveEmailBodyService.Verify((sebs) => sebs.SaveEmailBodyMessage(dataDictToBeStored), Times.Exactly(1));
            Assert.True(result, "created should return true");
        }
    }
}
