using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Dta.Marketplace.Subscribers.Email.Logger.Worker;
using Dta.Marketplace.Subscribers.Email.Logger.Worker.Model;
using Dta.Marketplace.Subscribers.Email.Logger.Worker.Processors;
using Dta.Marketplace.Subscribers.Email.Logger.Worker.Services;


namespace Dta.Marketplace.Subscribers.Email.Logger.Worker.Processors {
    internal class EmailBounceNotificationProcessor : AbstractEmailLogProcessor {
        private readonly IEmailService _saveEmailNotificationService;

        public EmailBounceNotificationProcessor(ILogger<AppService> logger, IOptions<AppConfig> config, IEmailService saveEmailNotificationService) : base(logger, config) {
            _saveEmailNotificationService = saveEmailNotificationService;
        }
        public override bool Process(AwsSqsMessage awsSqsMessage) {

            var notificationLogBodyAnon = JsonConvert.DeserializeAnonymousType(awsSqsMessage.Body, new {
                Type = "",
                Timestamp = "",
                MessageId = "",
                TopicArn = "",
                Message = "",
            });
            var notificationLogBodyMessageAnon = JsonConvert.DeserializeAnonymousType(notificationLogBodyAnon.Message, new {
                notificationType = "",
                mail = new {
                    timestamp = "",
                    Source = "",
                    messageID = "",
                    destination = new List<string>(),
                    commonHeaders = new {
                        returnPath = "",
                        from = new List<string>(),
                        replyTo = new List<string>(),
                        to = new List<string>(),
                        subject = ""
                    }
                },
                bounce = new {
                    bounceType = "",
                    bounceSubType = "",
                    bouncedRecipients = new List<dynamic>(),
                    timestamp = "",
                    smtpResponse = "",
                    remoteMtaIp = "",
                    reportingMTA = ""
                },
            });

            Dictionary<string, string> dataDictToBeStored = new Dictionary<string, string>() {
                    { "NotificationBodyMessageId", notificationLogBodyMessageAnon.mail.messageID },
                    { "NotificationBodyTopicARN", notificationLogBodyAnon.TopicArn },
                    { "NotificationBodyType", notificationLogBodyMessageAnon.notificationType },
                    { "NotificationBodyTimestamp", notificationLogBodyAnon.Timestamp.ToString () },
                    { "NotificationBodyMailSource", notificationLogBodyMessageAnon.mail.Source },
                    { "NotificationBodyCommonHeadersSubject", notificationLogBodyMessageAnon.mail.commonHeaders.subject },
                    { "NotificationBodyBounceType", notificationLogBodyMessageAnon.bounce.bounceType },
                    { "NotificationBodyBounceSubType", notificationLogBodyMessageAnon.bounce.bounceSubType },
                    { "NotificationBodyBounceTimestamp", notificationLogBodyMessageAnon.bounce.timestamp },
                    { "NotificationBodyBounceSMTPResponse", notificationLogBodyMessageAnon.bounce.smtpResponse },
                    { "NotificationBodyBounceRemoteMTAIp", notificationLogBodyMessageAnon.bounce.remoteMtaIp },
                    { "NotificationBodyBounceReportingMTA", notificationLogBodyMessageAnon.bounce.reportingMTA },
                };
            var bouncedRecipients = "";
            for (var index = 0; index < notificationLogBodyMessageAnon.bounce.bouncedRecipients.Count; index++) {
                bouncedRecipients += $"{notificationLogBodyMessageAnon.bounce.bouncedRecipients[index]},";
            }
            dataDictToBeStored.Add("NotificationBodyBounceBouncedRecipients", bouncedRecipients);
            for (var index = 0; index < notificationLogBodyMessageAnon.mail.commonHeaders.from.Count - 1; index++) {
                dataDictToBeStored.Add("NotificationBodyCommonHeadersFrom" + (index + 1), notificationLogBodyMessageAnon.mail.commonHeaders.from[index]);
            }
            for (var index = 0; index < notificationLogBodyMessageAnon.mail.commonHeaders.to.Count - 1; index++) {
                dataDictToBeStored.Add("NotificationBodyCommonHeadersTo" + (index + 1), notificationLogBodyMessageAnon.mail.commonHeaders.to[index]);
            }
            for (var index = 0; index < notificationLogBodyMessageAnon.mail.commonHeaders.replyTo.Count - 1; index++) {
                dataDictToBeStored.Add("NotificationBodyCommonHeadersReplyTo" + (index + 1), notificationLogBodyMessageAnon.mail.commonHeaders.replyTo[index]);
            }
            for (var index = 0; index < notificationLogBodyMessageAnon.mail.destination.Count - 1; index++) {
                dataDictToBeStored.Add("NotificationBodyDestination" + (index + 1), notificationLogBodyMessageAnon.mail.destination[index]);
            }
            _saveEmailNotificationService.SaveEmailMessage(dataDictToBeStored);
            return true;
        }
    }
}
