using System;

namespace Dta.Marketplace.Subscribers.Slack.Worker {
    public class AppConfig {
        public string AwsSqsRegion { get; set; } = "ap-southeast-2";
        public string AwsSqsQueueUrl { get; set; } = "http://localhost:4576/queue/dta-marketplace-local-slack";
        public string AwsSqsServiceUrl { get; set; } = "http://localhost:4576";
        public string AwsSqsAccessKeyId { get; set; }
        public string AwsSqsSecretAccessKey { get; set; }
        public int AwsSqsLongPollTimeInSeconds { get; set; } = 20;
        public string AgencySlackUrl { get; set; }
        public string SupplierSlackUrl { get; set; }
        public string BuyerSlackUrl { get; set; }
        public string UserSlackUrl { get; set; }
        public int WorkIntervalInSeconds { get; set; } = 60;
        public string SentryDsn { get; set; }
    }
}
