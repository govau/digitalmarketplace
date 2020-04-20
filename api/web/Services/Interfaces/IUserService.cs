using System.Collections.Generic;
using Dta.Marketplace.Api.Web.Entities;

namespace Dta.Marketplace.Api.Web.Services.Interfaces {
    public interface IUserService : IService {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
        User GetById(int id);
    }
}
