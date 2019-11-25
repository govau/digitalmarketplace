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
    internal class EmailComplaintNotificationProcessor : AbstractEmailLogProcessor {
        private readonly IEmailService _saveEmailNotificationService;

        public EmailComplaintNotificationProcessor(ILogger<AppService> logger, IOptions<AppConfig> config, IEmailService saveEmailNotificationService) : base(logger, config) {
            _saveEmailNotificationService = saveEmailNotificationService;

        }

        public override bool Process(AwsSqsMessage awsSqsMessage) {

            var notificationLogBodyAnon = JsonConvert.DeserializeAnonymousType(awsSqsMessage.Body, new {
                Type = "",
                TimeStamp = "",
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
                Complaint = new {
                    UserAgent = "",
                    ComplainedRecipients = new List<dynamic>(),
                    ComplaintFeedbackType = "",
                    ArrivalDate = "",
                    TimeStamp = "",
                    FeedbackId = ""
                }
            });

            Dictionary<string, string> dataDictToBeStored = new Dictionary<string, string>() {
                { "NotificationBodyMessageId", notificationLogBodyMessageAnon.Mail.MessageID },
                { "NotificationBodyTopicARN", notificationLogBodyAnon.TopicArn },
                { "NotificationBodyType", notificationLogBodyMessageAnon.NotificationType },
                { "NotificationBodyTimestamp", notificationLogBodyAnon.TimeStamp.ToString () },
                { "NotificationBodyMailSource", notificationLogBodyMessageAnon.Mail.Source },
                { "NotificationBodyCommonHeadersSubject", notificationLogBodyMessageAnon.Mail.CommonHeaders.Subject },
                { "NotificationBodyComplaintUserAgent", notificationLogBodyMessageAnon.Complaint.UserAgent },
                { "NotificationBodyComplaintFeedbackType", notificationLogBodyMessageAnon.Complaint.ComplaintFeedbackType },
                { "NotificationBodyComplaintFeedbackId", notificationLogBodyMessageAnon.Complaint.FeedbackId },
                { "NotificationBodyComplaintTimestamp", notificationLogBodyMessageAnon.Complaint.TimeStamp },
            };
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
            var ComplainedRecipient = "";
            for (var index = 0; index < notificationLogBodyMessageAnon.Complaint.ComplainedRecipients.Count; index++) {
                ComplainedRecipient += $"{notificationLogBodyMessageAnon.Complaint.ComplainedRecipients[index]},";
            }
            dataDictToBeStored.Add("NotificationBodyComplaintRecipient", ComplainedRecipient);

            var result = _saveEmailNotificationService.SaveEmailMessage(dataDictToBeStored);
            return result;
        }
    }
}
