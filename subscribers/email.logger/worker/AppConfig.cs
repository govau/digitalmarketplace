using System;

namespace Dta.Marketplace.Subscribers.Email.Logger.Worker {
    public class AppConfig {
        public string AwsSqsRegion { get; set; }
        public string AwsSqsBodyRegion { get; set;}
        public string AwsSqsQueueUrl { get; set; }
        public string AwsSqsBodyQueueUrl { get; set; }
        public string AwsSqsServiceUrl { get; set; }
        public string AwsSqsAccessKeyId { get; set; }
        public string AwsSqsSecretAccessKey { get; set; }
        public int AwsSqsLongPollTimeInSeconds { get; set; } = 20;
        public int WorkIntervalInSeconds { get; set; } = 60;
        public string SentryDsn { get; set; }
    }
}
