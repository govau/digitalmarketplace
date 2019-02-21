using System;

namespace Dta.Marketplace.Subscriber.Slack {
    public class AppConfig {
        public string AWS_SQS_QUEUE_URL { get; set; }
        public string AWS_SQS_SERVICE_URL { get; set; }
        public string AWS_SQS_ACCESS_KEY_ID { get; set; }
        public string AWS_SQS_SECRET_ACCESS_KEY { get; set; }
        public int AWS_SQS_LONG_POLL_TIME_IN_SECONDS { get; set; } = 20;
        public string SUPPLIER_SLACK_URL { get; set; }
        public string BUYER_SLACK_URL { get; set; }
        public string USER_SLACK_URL { get; set; }
        public int WORK_INTERVAL_IN_SECONDS { get; set; } = 60;
    }
}
