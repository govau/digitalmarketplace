using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Amazon;
using Dta.Marketplace.Subscribers.Email.Logger.Worker.Model;
using Dta.Marketplace.Subscribers.Email.Logger.Worker.Processors;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Dta.Marketplace.Subscribers.Email.Logger.Worker.Services {
    internal class SaveEmailBodyService : IEmailBodyService {
        private readonly ILogger _bodylogger;

        public SaveEmailBodyService (ILogger<AppService> logger) {
            _bodylogger = logger;
        }

        public bool SaveEmailBodyMessage (Dictionary<string, string> dataDictToBeStored) {

            DateTime timeStampDT = DateTime.Parse (dataDictToBeStored["EmailTimestamp"]).ToUniversalTime ();
            var emailBodyLogToBeStored = new EmailLogging ();
            using (var db = new EmailLoggerContext ()) {
                var matchingDbLogList = db.EmailLogging
                    .Where (l => l.MessageId == dataDictToBeStored["EmailMessageId"])
                    .ToList ();
                if (matchingDbLogList.Count == 0) {
                    var jsonDataToBeStored = JsonConvert.SerializeObject (dataDictToBeStored);
                    emailBodyLogToBeStored.MessageId = dataDictToBeStored["EmailMessageId"];
                    emailBodyLogToBeStored.Data = jsonDataToBeStored;
                    emailBodyLogToBeStored.DateTimeSent = timeStampDT;
                    emailBodyLogToBeStored.Subject = dataDictToBeStored["EmailSubject"];
                    db.EmailLogging.Add (emailBodyLogToBeStored);
                    db.SaveChanges ();
                } else {
                    foreach (var existingLogInDb in matchingDbLogList) {
                        db.EmailLogging.Update (existingLogInDb);
                        var dataEntryToBeUpdated = JsonConvert.DeserializeObject<Dictionary<string, string>> (existingLogInDb.Data);
                        Dictionary<string, string> unifiedDictToBeStored = new Dictionary<string, string> (dataDictToBeStored);
                        foreach (KeyValuePair<String, String> kvp in dataEntryToBeUpdated) {
                            unifiedDictToBeStored[kvp.Key] = kvp.Value;
                        }
                        var jsonDataToBeStored = JsonConvert.SerializeObject (unifiedDictToBeStored);
                        existingLogInDb.Data = jsonDataToBeStored;

                        db.SaveChanges ();
                    }
                }
            }
            return true;
        }
    }
}

