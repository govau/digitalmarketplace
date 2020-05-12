using System.Threading.Tasks;
using Dta.Marketplace.Api.Business.Models;

namespace Dta.Marketplace.Api.Business {
    public interface IUserSessionBusiness {
        Task<UserModel> GetSessionAsync(string token);
        Task<string> CreateSessionAsync(UserModel user);
    }
}
