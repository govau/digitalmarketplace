using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dta.Marketplace.Api.Services {
    public interface IAgencyService {
        Task<IEnumerable<dynamic>> GetAgenciesAsync();
        Task<dynamic> GetAgencyAsync(int id);
        Task<bool> UpdateAsync(int id, dynamic agency, string updatedBy);
    }
}
