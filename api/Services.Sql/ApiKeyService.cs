using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Dta.Marketplace.Api.Services.Entities;

namespace Dta.Marketplace.Api.Services.Sql {
    public class ApiKeyService : IApiKeyService {
        private readonly DigitalMarketplaceContext _context;

        public ApiKeyService(DigitalMarketplaceContext context) {
            _context = context;
        }
        
        public async Task<ApiKey> GetAsync(string apiKey) => (
            await _context
                .ApiKey
                .AsNoTracking()
                .Include(a => a.User)
                .Where(a => a.Key == apiKey && a.RevokedAt != null)
                .SingleOrDefaultAsync()
        );
        public async Task<ApiKey> GetForUpdateAsync(string apiKey) => (
            await _context
                .ApiKey
                .Include(a => a.User)
                .Where(a => a.Key == apiKey)
                .SingleOrDefaultAsync()
        );
    }
}
