using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Dta.Marketplace.Subscribers.Email.Logger.Worker.Model;
using Dta.Marketplace.Subscribers.Email.Logger.Worker.Services;


namespace Dta.Marketplace.Subscribers.Email.Logger.Worker.Processors {
    internal class EmailDeliveryNotificationProcessor : AbstractEmailLogProcessor {
        private readonly IEmailService _saveEmailNotificaitonService;

        public EmailDeliveryNotificationProcessor(ILogger<AppService> logger, IOptions<AppConfig> config, IEmailService saveEmailNotificationService) : base(logger, config) {
            _saveEmailNotificaitonService = saveEmailNotificationService;
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
                    TimeStamp = "",
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
                Delivery = new {
                    TimeStamp = "",
                    ProcessingTimeMillis = default(int),
                    SmtpResponse = "",
                    RemoteMtaIp = "",
                    ReportingMTA = "",
                    Recipients = new List<dynamic>(),
                },
            });

            Dictionary<string, string> dataDictToBeStored = new Dictionary<string, string>() {
                { "NotificationBodyMessageId", notificationLogBodyMessageAnon.Mail.MessageID },
                { "NotificationBodyTopicARN", notificationLogBodyAnon.TopicArn },
                { "NotificationBodyType", notificationLogBodyMessageAnon.NotificationType },
                { "NotificationBodyTimestamp", notificationLogBodyAnon.Timestamp.ToString () },
                { "NotificationBodyMailSource", notificationLogBodyMessageAnon.Mail.Source },
                { "NotificationBodyCommonHeadersSubject", notificationLogBodyMessageAnon.Mail.CommonHeaders.Subject },
                { "NotificationBodyDeliveryTimestamp", notificationLogBodyMessageAnon.Delivery.TimeStamp },
                { "NotificationBodyDeliveryProcessingTimeMs", notificationLogBodyMessageAnon.Delivery.ProcessingTimeMillis.ToString () },
                { "NotificationBodyDeliverySMTPResponse", notificationLogBodyMessageAnon.Delivery.SmtpResponse },
                { "NotificationBodyDeliveryRemoteMtaIp", notificationLogBodyMessageAnon.Delivery.RemoteMtaIp },
                { "NotificationBodyDeliveryReportingMTA", notificationLogBodyMessageAnon.Delivery.ReportingMTA },
            };
            var recipients = "";
            for (var index = 0; index < notificationLogBodyMessageAnon.Delivery.Recipients.Count; index++) {
                recipients += $"{notificationLogBodyMessageAnon.Delivery.Recipients[index]},";
            }
            dataDictToBeStored.Add("Recipients", recipients);
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

            _saveEmailNotificaitonService.SaveEmailMessage(dataDictToBeStored);

            return true;
        }
    }
}
