using Dta.Marketplace.Api.Services.Entities;

namespace Dta.Marketplace.Api.Services {
    public interface IApiKeyService {
        ApiKey Get(string apiKey);
    }
}
