using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Globalization;

namespace Dta.Marketplace.Subscribers.Logger.Worker {
    internal static class Converter {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            }
        };
    }
}
