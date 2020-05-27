using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dta.Marketplace.Api.Services.Reports {
    public interface IFeedbackService {
        Task<IEnumerable<dynamic>> GetFeedbacksAsync();
    }
}
