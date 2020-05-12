using System.Collections.Generic;
using System.Threading.Tasks;
using Dta.Marketplace.Api.Services.Entities;

namespace Dta.Marketplace.Api.Services {
    public interface IUserService {
        Task<User> AuthenticateAsync(string username, string password);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
    }
}
