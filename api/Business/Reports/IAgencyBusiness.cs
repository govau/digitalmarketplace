using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dta.Marketplace.Api.Business.Reports {
    public interface IAgencyBusiness {
        Task<IEnumerable<dynamic>> GetAgenciesAsync();
    }
}
