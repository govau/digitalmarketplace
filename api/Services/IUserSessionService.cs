using System.Threading.Tasks;

namespace Dta.Marketplace.Api.Services {
    public interface IUserSessionService {
        Task<bool> CreateAsync(int userId, string token);
        Task<int?> GetAsync(string token);
    }
}
