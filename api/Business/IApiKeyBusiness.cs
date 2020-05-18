using System.Threading.Tasks;
using Dta.Marketplace.Api.Business.Models;

namespace Dta.Marketplace.Api.Business {
    public interface IApiKeyBusiness {
        Task<string> GenerateTokenAsync(int userId);
        Task<bool> RevokeAsync(string apiKey);
    }
}
