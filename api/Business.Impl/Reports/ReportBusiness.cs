using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dta.Marketplace.Api.Services.Reports;

namespace Dta.Marketplace.Api.Business.Reports {
    public class ReportBusiness : IReportBusiness {
        private readonly IAgencyService _agencyService;
        private readonly IBriefService _briefService;
        private readonly IBriefResponseService _briefResponseService;
        private readonly IFeedbackService _feedbackService;
        private readonly ISupplierService _supplierService;
        public ReportBusiness(
            IAgencyService agencyService,
            IBriefService briefService,
            IBriefResponseService briefResponseService,
            IFeedbackService feedbackService,
            ISupplierService supplierService
        ) {
            _agencyService = agencyService;
            _briefService = briefService;
            _briefResponseService = briefResponseService;
            _feedbackService = feedbackService;
            _supplierService = supplierService;
        }
        public async Task<IEnumerable<dynamic>> GetAgenciesAsync() =>  await _agencyService.GetAgenciesAsync();
        public async Task<IEnumerable<dynamic>> GetPublishedBriefsAsync() => await _briefService.GetPublishedBriefsAsync();
        public async Task<IEnumerable<dynamic>> GetSubmittedBriefResponsesAsync() => await _briefResponseService.GetSubmittedBriefResponsesAsync();
        public async Task<IEnumerable<dynamic>> GetFeedbacksAsync() => await _feedbackService.GetFeedbacksAsync();
        public async Task<IEnumerable<dynamic>> GetSuppliersAsync() => await _supplierService.GetSuppliersAsync();
    }
}
