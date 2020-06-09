using System.Collections.Generic;
using System.Threading.Tasks;
using Dta.Marketplace.Api.Services.Entities;

namespace Dta.Marketplace.Api.Services {
    public interface IAgencyService {
        Task<Agency> GetAgencyForUpdateAsync(int id);
        Task<Agency> GetOrAddAgencyAsync(string domain);
        Task<string> GetAgencyNameAsync(int id);
        Task<Agency> GetAgencyByDomainAsync(string domain);
        Task<IEnumerable<AgencyDomain>> GetAgencyDomainsAsync(int id);
        Task<IEnumerable<Agency>> GetAgenciesAsync();
        Task<Agency> GetAgencyAsync(int id);
        Task<bool> AgencyRequiresTeamMembershipAsync(int id);
        Task<bool> UpdateAsync();
    }
}
