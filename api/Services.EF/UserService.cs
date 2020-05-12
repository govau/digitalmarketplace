using System.Collections.Generic;
using System.Linq;
using Dta.Marketplace.Api.Services.Entities;

namespace Dta.Marketplace.Api.Services.EF {
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
