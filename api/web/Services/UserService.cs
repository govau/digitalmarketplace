using System.Collections.Generic;
using System.Linq;
using Dta.Marketplace.Api.Web.Services.Interfaces;
using Dta.Marketplace.Api.Web.Entities;

namespace Dta.Marketplace.Api.Web.Services {
    public class UserService : IUserService {
        private readonly DigitalMarketplaceContext _context;

        public UserService(DigitalMarketplaceContext context) {
            _context = context;
        }

        public User Authenticate(string username, string password) => (
            _context.User.SingleOrDefault(u =>
                u.EmailAddress == username &&
                // u.Password == password &&
                u.FailedLoginCount <= 5 &&
                u.Active == true
            )
        );
        public IEnumerable<User> GetAll() => _context.User.ToList();
        public User GetById(int id) => _context.User.SingleOrDefault(x => x.Id == id);
    }
}
