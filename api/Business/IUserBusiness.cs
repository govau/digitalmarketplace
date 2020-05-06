using System.Collections.Generic;
using Dta.Marketplace.Api.Business.Models;

namespace Dta.Marketplace.Api.Business {
    public interface IUserBusiness {
        UserModel Authenticate(AuthenticateModel model);
        IEnumerable<UserModel> GetAll();
        UserModel GetById(int id);
    }
}
