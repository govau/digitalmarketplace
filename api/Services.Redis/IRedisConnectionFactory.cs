using StackExchange.Redis;

namespace Dta.Marketplace.Api.Services.Redis {
    public interface IRedisConnectionFactory {
        ConnectionMultiplexer Connection();
    }
}
