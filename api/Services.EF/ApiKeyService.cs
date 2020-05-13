using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Dta.Marketplace.Api.Services.Entities;

namespace Dta.Marketplace.Api.Services.EF {
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
                .SingleOrDefaultAsync(a => a.Key == apiKey)
        );
    }
}
