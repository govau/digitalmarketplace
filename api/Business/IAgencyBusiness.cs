using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dta.Marketplace.Api.Business {
    public interface IAgencyBusiness {
        Task<IEnumerable<dynamic>> GetAgenciesAsync();
        Task<dynamic> GetAgencyAsync(int id);
        Task<bool> UpdateAsync(int id, dynamic agency, string updatedBy);
    }
}
