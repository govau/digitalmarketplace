using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System;
using Dta.Marketplace.Api.Shared;

namespace Dta.Marketplace.Api.Services.Redis {
    public class RedisConnectionFactory : IRedisConnectionFactory {
        private readonly Lazy<ConnectionMultiplexer> _connection;

        public RedisConnectionFactory(IOptions<AppSettings> appSettings) {
            this._connection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(appSettings.Value.RedisConnectionString));
        }

        public ConnectionMultiplexer Connection() {
            return this._connection.Value;
        }
    }
}
