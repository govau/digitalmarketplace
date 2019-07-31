using System;

namespace Dta.Marketplace.Subscribers.Logger.Worker {
    public class AppConfig {
        public string AwsSqsRegion { get; set; } = "ap-southeast-2";
        public string AwsSqsQueueUrl { get; set; }
        public string AwsSqsServiceUrl { get; set; }
        public string AwsSqsAccessKeyId { get; set; }
        public string AwsSqsSecretAccessKey { get; set; }
        public int AwsSqsLongPollTimeInSeconds { get; set; } = 20;
        public int WorkIntervalInSeconds { get; set; } = 60;
        public string SentryDsn { get; set; }

        public string DbName { get; set; }
        public string DbHost { get; set; }
        public string DbPort { get; set; }
        public string DbUsername { get; set; }
        public string DbPassword { get; set; }
    }
}
