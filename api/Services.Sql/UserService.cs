using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dta.Marketplace.Api.Services.Entities;

namespace Dta.Marketplace.Api.Services.Sql {
    public class UserService : IUserService {
        private readonly DigitalMarketplaceContext _context;

        public UserService(DigitalMarketplaceContext context) {
            _context = context;
        }

        public async Task<User> AuthenticateAsync(string username, string password) => (
            await _context
                .User
                .AsNoTracking()
                .SingleOrDefaultAsync(u =>
                    u.EmailAddress == username &&
                    u.Password == password &&
                    u.FailedLoginCount <= 5 &&
                    u.Active == true
                )
        );

        public async Task<IEnumerable<User>> GetAllAsync() => await _context.User.ToListAsync();
        public async Task<User> GetByIdAsync(int id) => await _context.User.SingleOrDefaultAsync(x => x.Id == id);
    }
}
