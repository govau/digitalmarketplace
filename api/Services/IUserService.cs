using System.Collections.Generic;
using Dta.Marketplace.Api.Entities;

namespace Dta.Marketplace.Api.Services {
    public interface IUserService : IService {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
        User GetById(int id);
    }
}
