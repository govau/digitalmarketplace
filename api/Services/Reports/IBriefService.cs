using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dta.Marketplace.Api.Services.Reports {
    public interface IBriefService {
        Task<IEnumerable<dynamic>> GetPublishedBriefsAsync();
    }
}
