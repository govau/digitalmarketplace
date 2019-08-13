
// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using Dta.Marketplace.Subscribers.Slack.Worker.Model;
//
//    var vcapServices = VcapServices.FromJson(jsonString);

using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Dta.Marketplace.Subscribers.Email.Logger.Worker.Model {

    public partial class VcapServices {
        [JsonProperty("user-provided")]
        public List<UserProvided> UserProvided { get; set; }
        
        [JsonProperty("postgres")]
        public List<Postgres> Postgres { get; set; }
    }
  

    public partial class UserProvided {
        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("tags")]
        public List<object> Tags { get; set; }

        [JsonProperty("instance_name")]
        public string InstanceName { get; set; }

        [JsonProperty("binding_name")]
        public object BindingName { get; set; }

        [JsonProperty("credentials")]
        public Credentials Credentials { get; set; }

        [JsonProperty("syslog_drain_url")]
        public string SyslogDrainUrl { get; set; }

        [JsonProperty("volume_mounts")]
        public List<object> VolumeMounts { get; set; }
    }

    public partial class Postgres {
       [JsonProperty("credentials")]
       public PostgresCredentials Credentials { get; set; }
    }
    public partial class Credentials {
        [JsonProperty("AWS_SQS_ACCESS_KEY_ID")]
        public string AwsSqsAccessKeyId { get; set; }

        [JsonProperty("AWS_SQS_QUEUE_URL")]
        public string AwsSqsQueueUrl { get; set; }

        [JsonProperty("AWS_SQS_REGION")]
        public string AwsSqsRegion { get; set; }
        
        [JsonProperty("AWS_SQS_BODY_QUEUE_URL")]
        public string AwsSqsBodyQueueUrl { get; set; }
        
        [JsonProperty("AWS_SQS_SERVICE_URL")]
        public string AwsSqsServiceUrl { get; set; }
        
        [JsonProperty("AWS_SQS_BODY_SERVICE_URL")]
        public string AwsSqsBodyServiceUrl { get; set; }

        [JsonProperty("AWS_SQS_BODY_REGION")]
        public string AwsSqsBodyRegion { get; set; }

        [JsonProperty("AWS_SQS_SECRET_ACCESS_KEY")]
        public string AwsSqsSecretAccessKey { get; set; }

        [JsonProperty("WORK_INTERVAL_IN_SECONDS")]
        public int WorkIntervalInSeconds { get; set; }

        [JsonProperty("AWS_SQS_LONG_POLL_TIME_IN_SECONDS")]
        public int AwsSqsLongPollTimeInSeconds { get; set; }

        [JsonProperty("SENTRY_DSN")]
        public string SentryDsn { get; set; }
    }
    
    public partial class PostgresCredentials {
       [JsonProperty("dbname")]
       public string DbName { get; set; }
       [JsonProperty("host")]
       public string Host { get; set; }
       [JsonProperty("password")]
       public string Password { get; set; }
       [JsonProperty("port")]
       public string Port { get; set; }
       [JsonProperty("username")]
       public string Username { get; set; }
   }
    public partial class VcapServices {
        public static VcapServices FromJson(string json) => JsonConvert.DeserializeObject<VcapServices>(json, Converter.Settings);
    }
}