
using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
//generated
namespace Dta.Marketplace.Subscriber.Slack.Model {

    public partial class AwsSnsMessage {
        [JsonProperty("Message")]
        public string Message { get; set; }

        [JsonProperty("MessageAttributes")]
        public MessageAttributes MessageAttributes { get; set; }
    }

    public partial class MessageAttributes {
        [JsonProperty("object_type")]
        public TType ObjectType { get; set; }

        [JsonProperty("event_type")]
        public TType EventType { get; set; }
    }

    public partial class TType {
        [JsonProperty("Type")]
        public string Type { get; set; }

        [JsonProperty("Value")]
        public string Value { get; set; }
    }

    public partial class AwsSnsMessage {
        public static AwsSnsMessage FromJson(string json) => JsonConvert.DeserializeObject<AwsSnsMessage>(json, Dta.Marketplace.Subscriber.Slack.Model.Converter.Settings);
    }

    public static class Serialize {
        public static string ToJson(this AwsSnsMessage self) => JsonConvert.SerializeObject(self, Dta.Marketplace.Subscriber.Slack.Model.Converter.Settings);
    }

    internal static class Converter {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            }
        };
    }
}
