using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dta.Marketplace.Api.Services.Entities;

namespace Dta.Marketplace.Api.Services.EF {
    public class ApiKeyService : IApiKeyService {
        private readonly DigitalMarketplaceContext _context;

        public ApiKeyService(DigitalMarketplaceContext context) {
            _context = context;
        }
        public async Task<ApiKey> GetAsync(string apiKey) {
            return await _context.ApiKey.Include(a => a.User).SingleOrDefaultAsync(a => a.Key == apiKey);
        }
    }
}
