using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dta.Marketplace.Api.Services.Reports;

namespace Dta.Marketplace.Api.Business.Reports {
    public class AgencyBusiness : IAgencyBusiness {
        private readonly IAgencyService _agencyService;

        public AgencyBusiness(IAgencyService agencyService) {
            _agencyService = agencyService;
        }

        public async Task<IEnumerable<dynamic>> GetAgenciesAsync() {
            var results = await _agencyService.GetAgenciesAsync();
            System.Console.WriteLine(results.First().domains);
            return results;
        }
    }
}
