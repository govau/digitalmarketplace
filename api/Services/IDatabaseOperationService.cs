using System.Threading.Tasks;
using Dta.Marketplace.Api.Services.Entities;

namespace Dta.Marketplace.Api.Services {
    public interface IDatabaseOperationService {
        Task<T> CreateAsync<T>(T entity) where T: class;
        Task<int> CommitAsync();
    }
}
