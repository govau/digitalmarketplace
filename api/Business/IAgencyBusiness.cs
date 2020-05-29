using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dta.Marketplace.Api.Business {
    public interface IAgencyBusiness {
        Task<IEnumerable<dynamic>> GetAgenciesAsync();
        Task<IEnumerable<dynamic>> GetAgencyAsync(int agency_id);
        Task<IEnumerable<dynamic>> UpdateAsync(int agency_id, dynamic agency, string updated_by);
    }
}
