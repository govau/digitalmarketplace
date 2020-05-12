using System;

namespace Dta.Marketplace.Api.Shared {
    public class AppSettings {
        private string _marketplaceConnectionString;
        public string MarketplaceConnectionString {
            get {
                if (!string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("MarketplaceConnectionString"))) {
                    _marketplaceConnectionString = Environment.GetEnvironmentVariable("MarketplaceConnectionString");
                }
                return _marketplaceConnectionString;
            }
            set {
                _marketplaceConnectionString = value;
            }
        }

        private string _redisConnectionString;
        public string RedisConnectionString {
            get {
                if (!string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("RedisConnectionString"))) {
                    _redisConnectionString = Environment.GetEnvironmentVariable("RedisConnectionString");
                }
                return _redisConnectionString;
            }
            set {
                _redisConnectionString = value;
            }
        }
    }
}