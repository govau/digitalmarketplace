using System.Threading.Tasks;

namespace Dta.Marketplace.Api.Services.Redis {
    public class UserSessionService : IUserSessionService {
        private readonly IRedisConnectionFactory _redis;

        public UserSessionService(IRedisConnectionFactory redis) {
            _redis = redis;
        }

        public async Task<bool> CreateAsync(int userId, string token) {
            var db = _redis.Connection().GetDatabase();
            return await db.StringSetAsync(token, userId);
        }

        public async Task<int?> GetAsync(string token) {
            var db = _redis.Connection().GetDatabase();
            return (int)await db.StringGetAsync(token);
        }
    }
}
