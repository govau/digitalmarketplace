using System.Collections.Generic;
using System.Threading.Tasks;
using Dta.Marketplace.Api.Business.Models;

namespace Dta.Marketplace.Api.Business {
    public interface IUserBusiness {
        Task<UserModel> AuthenticateAsync(AuthenticateModel model);
        Task<IEnumerable<UserModel>> GetAllAsync();
        Task<UserModel> GetByIdAsync(int id);
        Task<UserModel> AuthenticateByApiKeyAsync(string apiKey);
    }
}
