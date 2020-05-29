using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dta.Marketplace.Api.Services;

namespace Dta.Marketplace.Api.Business {
    public class AgencyBusiness : IAgencyBusiness {
        private readonly IAgencyService _agencyService;
        public AgencyBusiness(
            IAgencyService agencyService
        ) {
            _agencyService = agencyService;
        }
        public async Task<IEnumerable<dynamic>> GetAgenciesAsync() =>  await _agencyService.GetAgenciesAsync();

        public async Task<IEnumerable<dynamic>> GetAgencyAsync(int agency_id) => await _agencyService.GetAgencyAsync(agency_id);

        public async Task<IEnumerable<dynamic>> UpdateAsync(int agency_id, dynamic agency, string updated_by) => await _agencyService.UpdateAsync(agency_id, agency, updated_by);
    }
}
