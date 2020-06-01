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

        public async Task<dynamic> GetAgencyAsync(int id) => await _agencyService.GetAgencyAsync(id);

        public async Task<bool> UpdateAsync(int id, dynamic agency, string updatedBy) => await _agencyService.UpdateAsync(id, agency, updatedBy);
    }
}
