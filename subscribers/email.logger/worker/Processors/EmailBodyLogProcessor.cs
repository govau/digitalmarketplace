using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Dta.Marketplace.Subscribers.Email.Logger.Worker.Model;
using Dta.Marketplace.Subscribers.Email.Logger.Worker.Services;


namespace Dta.Marketplace.Subscribers.Email.Logger.Worker.Processors {
    public class EmailBodyLogProcessor : AbstractEmailLogProcessor {
        private readonly IEmailBodyService _saveEmailBodyService;

        public EmailBodyLogProcessor(ILogger<AppService> logger, IOptions<AppConfig> config, IEmailBodyService saveEmailBodyService) : base(logger, config) {
            _saveEmailBodyService = saveEmailBodyService;
        }

        public override bool Process(AwsSqsMessage awsSqsMessage) {

            var emailLogBodyAnon = JsonConvert.DeserializeAnonymousType(awsSqsMessage.Body, new {
                Type = "",
                Timestamp = "",
                MessageId = "",
                TopicArn = "",
                Message = "",
            });
            var emailLogBodySubAnon = JsonConvert.DeserializeAnonymousType(emailLogBodyAnon.Message, new {
                Email = "",
            });
            var emailLogBodySubTwoAnon = JsonConvert.DeserializeAnonymousType(emailLogBodySubAnon.Email, new {
                Body = "",
                Subject = "",
                MessageId = "",
                notificationType = "",
                EmailResponseMetaData = new {
                    MessageId = "",
                    ResponseMetadata = new {
                        RetryAttempts = default(int),
                        HTTPStatusCode = "",
                        RequestId = "",

                    }
                },
            });
            Dictionary<string, string> dataDictToBeStored = new Dictionary<string, string>() {
                {"EmailMessageId", emailLogBodySubTwoAnon.EmailResponseMetaData.MessageId},
                {"EmailBody", emailLogBodySubTwoAnon.Body},
                {"EmailSubject", emailLogBodySubTwoAnon.Subject},
                {"EmailTopicArn", emailLogBodyAnon.TopicArn},
                {"EmailTimestamp", emailLogBodyAnon.Timestamp.ToString()},
                {"EmailResponseMetaDataHTTPCode", emailLogBodySubTwoAnon.EmailResponseMetaData.ResponseMetadata.HTTPStatusCode},
                {"EmailResponseMetaDataRequestId", emailLogBodySubTwoAnon.EmailResponseMetaData.ResponseMetadata.RequestId},
                {"EmailResponseMetaDataRetryAttempts", emailLogBodySubTwoAnon.EmailResponseMetaData.ResponseMetadata.RetryAttempts.ToString()},
            };

            _saveEmailBodyService.SaveEmailBodyMessage(dataDictToBeStored);
            return true;
        }
    }
}
