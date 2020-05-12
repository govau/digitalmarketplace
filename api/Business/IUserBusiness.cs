using System.Collections.Generic;
using System.Threading.Tasks;
using Dta.Marketplace.Api.Business.Models;

namespace Dta.Marketplace.Api.Business {
    public interface IUserBusiness {
        Task<UserModel> AuthenticateAsync(AuthenticateModel model);
        IEnumerable<UserModel> GetAll();
        UserModel GetById(int id);
    }
}
