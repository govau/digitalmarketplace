using Newtonsoft.Json;

namespace Dta.Marketplace.Subscriber.Slack.Model {
    public static class Serialize {
        public static string ToJson(this VcapServices self) => JsonConvert.SerializeObject(self, Dta.Marketplace.Subscriber.Slack.Model.Converter.Settings);
    }
}
