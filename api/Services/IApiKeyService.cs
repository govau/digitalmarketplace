using System.Threading.Tasks;
using Dta.Marketplace.Api.Services.Entities;

namespace Dta.Marketplace.Api.Services {
    public interface IApiKeyService {
        Task<ApiKey> GetAsync(string apiKey);
    }
}
