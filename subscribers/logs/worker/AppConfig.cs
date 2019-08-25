using System;

namespace Dta.Marketplace.Subscribers.Logger.Worker {
    public class AppConfig {
        public string AwsSqsRegion { get; set; } = "ap-southeast-2";
        public string AwsSqsQueueUrl { get; set; }
        public string AwsSqsServiceUrl { get; set; } = "http://localhost:4576";
        public string AwsSqsAccessKeyId { get; set; }
        public string AwsSqsSecretAccessKey { get; set; }
        public int AwsSqsLongPollTimeInSeconds { get; set; } = 20;
        public int WorkIntervalInSeconds { get; set; } = 60;
        public string SentryDsn { get; set; }
        public string ConnectionString { get; set; } = "Host=localhost;Port=15432;Database=logger;Username=postgres;Password=password";
    }
}
