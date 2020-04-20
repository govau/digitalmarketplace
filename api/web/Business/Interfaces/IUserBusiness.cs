using System.Collections.Generic;
using Dta.Marketplace.Api.Web.Entities;
using Dta.Marketplace.Api.Web.Models;

namespace Dta.Marketplace.Api.Web.Business.Interfaces {
    public interface IUserBusiness {
        UserModel Authenticate(AuthenticateModel model);
        IEnumerable<UserModel> GetAll();
        UserModel GetById(int id);
    }
}
