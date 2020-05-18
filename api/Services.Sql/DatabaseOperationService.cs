using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dta.Marketplace.Api.Services;

namespace Dta.Marketplace.Api.Services.Sql {
    public class DatabaseOperationService : IDatabaseOperationService {
        private readonly DigitalMarketplaceContext _context;

        public DatabaseOperationService(DigitalMarketplaceContext context) {
            _context = context;
        }

        public async Task<T> CreateAsync<T>(T entity) where T : class {
            var result = await _context.AddAsync<T>(entity);
            return result.Entity;
        }
        public async Task<int> CommitAsync() {
            return await _context.SaveChangesAsync();
        }
    }
}
