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
                NotificationType = "",
                Mail = new {
                    Timestamp = "",
                    Source = "",
                    MessageID = "",
                    Destination = new List<string>(),
                    CommonHeaders = new {
                        ReturnPath = "",
                        From = new List<string>(),
                        ReplyTo = new List<string>(),
                        To = new List<string>(),
                        Subject = ""
                    }
                },
                Bounce = new {
                    BounceType = "",
                    BounceSubType = "",
                    BouncedRecipients = new List<dynamic>(),
                    Timestamp = "",
                    SmtpResponse = "",
                    RemoteMtaIp = "",
                    ReportingMTA = ""
                },
            });

            Dictionary<string, string> dataDictToBeStored = new Dictionary<string, string>() {
                    { "NotificationBodyMessageId", notificationLogBodyMessageAnon.Mail.MessageID },
                    { "NotificationBodyTopicARN", notificationLogBodyAnon.TopicArn },
                    { "NotificationBodyType", notificationLogBodyMessageAnon.NotificationType },
                    { "NotificationBodyTimestamp", notificationLogBodyAnon.Timestamp.ToString () },
                    { "NotificationBodyMailSource", notificationLogBodyMessageAnon.Mail.Source },
                    { "NotificationBodyCommonHeadersSubject", notificationLogBodyMessageAnon.Mail.CommonHeaders.Subject },
                    { "NotificationBodyBounceType", notificationLogBodyMessageAnon.Bounce.BounceType },
                    { "NotificationBodyBounceSubType", notificationLogBodyMessageAnon.Bounce.BounceSubType },
                    { "NotificationBodyBounceTimestamp", notificationLogBodyMessageAnon.Bounce.Timestamp },
                    { "NotificationBodyBounceSMTPResponse", notificationLogBodyMessageAnon.Bounce.SmtpResponse },
                    { "NotificationBodyBounceRemoteMTAIp", notificationLogBodyMessageAnon.Bounce.RemoteMtaIp },
                    { "NotificationBodyBounceReportingMTA", notificationLogBodyMessageAnon.Bounce.ReportingMTA },
                };
            var bouncedRecipients = "";
            for (var index = 0; index < notificationLogBodyMessageAnon.Bounce.BouncedRecipients.Count; index++) {
                bouncedRecipients += $"{notificationLogBodyMessageAnon.Bounce.BouncedRecipients[index]},";
            }
            dataDictToBeStored.Add("NotificationBodyBounceBouncedRecipients", bouncedRecipients);
            for (var index = 0; index < notificationLogBodyMessageAnon.Mail.CommonHeaders.From.Count - 1; index++) {
                dataDictToBeStored.Add("NotificationBodyCommonHeadersFrom" + (index + 1), notificationLogBodyMessageAnon.Mail.CommonHeaders.From[index]);
            }
            for (var index = 0; index < notificationLogBodyMessageAnon.Mail.CommonHeaders.To.Count - 1; index++) {
                dataDictToBeStored.Add("NotificationBodyCommonHeadersTo" + (index + 1), notificationLogBodyMessageAnon.Mail.CommonHeaders.To[index]);
            }
            for (var index = 0; index < notificationLogBodyMessageAnon.Mail.CommonHeaders.ReplyTo.Count - 1; index++) {
                dataDictToBeStored.Add("NotificationBodyCommonHeadersReplyTo" + (index + 1), notificationLogBodyMessageAnon.Mail.CommonHeaders.ReplyTo[index]);
            }
            for (var index = 0; index < notificationLogBodyMessageAnon.Mail.Destination.Count - 1; index++) {
                dataDictToBeStored.Add("NotificationBodyDestination" + (index + 1), notificationLogBodyMessageAnon.Mail.Destination[index]);
            }
            _saveEmailNotificationService.SaveEmailMessage(dataDictToBeStored);
            return true;
        }
    }
}
