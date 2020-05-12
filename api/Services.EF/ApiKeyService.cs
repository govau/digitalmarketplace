using System.Collections.Generic;
using System.Linq;
using Dta.Marketplace.Api.Services.Entities;

namespace Dta.Marketplace.Api.Services.EF {
    public class ApiKeyService : IApiKeyService {
        private readonly DigitalMarketplaceContext _context;

        public ApiKeyService(DigitalMarketplaceContext context) {
            _context = context;
        }
        public ApiKey Get(string apiKey) {
            return _context.ApiKey.SingleOrDefault(a => a.Key == apiKey);
        }
    }
}
