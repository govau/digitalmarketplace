using System.Collections.Generic;
using Dta.Marketplace.Api.Services.Entities;

namespace Dta.Marketplace.Api.Services {
    public interface IUserService {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
        User GetById(int id);
    }
}
