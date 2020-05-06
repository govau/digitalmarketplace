using System;

namespace Dta.Marketplace.Api.Shared {
    public class AppSettings {
        public string Secret { get; set; }
        private string _connectionString;
        public string ConnectionString {
            get {
                if (!string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("ConnectionString"))) {
                    _connectionString = Environment.GetEnvironmentVariable("ConnectionString");
                }
                return _connectionString;
            }
            set {
                _connectionString = value;
            }
        }
    }
}
