using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using Dta.Marketplace.Subscribers.Email.Logger.Worker.Model;
using Dta.Marketplace.Subscribers.Email.Logger.Worker.Processors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Dta.Marketplace.Subscribers.Email.Logger.Worker.Services {
    internal class SaveEmailNotificationService : IEmailService {
        private readonly ILogger _logger;

        public SaveEmailNotificationService (ILogger<AppService> logger) {
            _logger = logger;
        }

        public bool SaveEmailMessage (Dictionary<string, string> dataDictToBeStored) {

            DateTime timeStampDT = DateTime.Parse (dataDictToBeStored["NotificationBodyTimestamp"]).ToUniversalTime ();
            var emailNotificationLogToBeStored = new EmailLogging ();

            using (var db = new EmailLoggerContext ()) {

                var matchingDbLogList = db.EmailLogging
                    .Where (l => l.MessageId == dataDictToBeStored["NotificationBodyMessageId"])
                    .ToList ();

                if (matchingDbLogList.Count == 0) {
                    var jsonDataToBeStored = JsonConvert.SerializeObject (dataDictToBeStored);
                    emailNotificationLogToBeStored.MessageId = dataDictToBeStored["NotificationBodyMessageId"];
                    emailNotificationLogToBeStored.Data = jsonDataToBeStored;
                    emailNotificationLogToBeStored.NotificationType = dataDictToBeStored["NotificationBodyType"];
                    emailNotificationLogToBeStored.DateTimeSent = timeStampDT;
                    emailNotificationLogToBeStored.Subject = dataDictToBeStored["NotificationBodyCommonHeadersSubject"];
                    db.EmailLogging.Add (emailNotificationLogToBeStored);
                    db.SaveChanges ();
                } 
                else {
                    foreach (var existingLogInDb in matchingDbLogList) {
                        db.EmailLogging.Update (existingLogInDb);
                        var dataEntryToBeUpdated = JsonConvert.DeserializeObject<Dictionary<string, string>> (existingLogInDb.Data);
                        Dictionary<string, string> unifiedDictToBeStored = new Dictionary<string, string> (dataDictToBeStored);
                        foreach (KeyValuePair<String, String> kvp in dataEntryToBeUpdated) {
                            unifiedDictToBeStored[kvp.Key] = kvp.Value;
                        }
                        var jsonDataToBeStored = JsonConvert.SerializeObject (unifiedDictToBeStored);
                        existingLogInDb.Data = jsonDataToBeStored;
                        existingLogInDb.NotificationType = dataDictToBeStored["NotificationBodyType"];
                        db.SaveChanges ();
                    }
                }
            }
            return true;
        }
    }
}