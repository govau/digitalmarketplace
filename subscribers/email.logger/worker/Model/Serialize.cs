using Newtonsoft.Json;

namespace Dta.Marketplace.Subscribers.Email.Logger.Worker.Model {
    public static class Serialize {
        public static string ToJson(this VcapServices self) => JsonConvert.SerializeObject(self, Dta.Marketplace.Subscribers.Email.Logger.Worker.Model.Converter.Settings);
    }
}
