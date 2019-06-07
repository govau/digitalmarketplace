using System;

namespace Dta.Marketplace.Subscribers.Logger.Worker {
    public class AppConfig {
        public string AwsSqsRegion { get; set; } = "ap-southeast-2";
        public string AwsSqsQueueUrl { get; set; }
        public string AwsSqsServiceUrl { get; set; }
        public string AwsSqsAccessKeyId { get; set; }
        public string AwsSqsSecretAccessKey { get; set; }
        public int AwsSqsLongPollTimeInSeconds { get; set; } = 20;
        public string SupplierSlackUrl { get; set; }
        public string BuyerSlackUrl { get; set; }
        public string UserSlackUrl { get; set; }
        public int WorkIntervalInSeconds { get; set; } = 60;
        public string SentryDsn { get; set; }
    }
}
