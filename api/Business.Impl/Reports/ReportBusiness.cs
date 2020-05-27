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
        public async Task<IEnumerable<dynamic>> GetAgenciesAsync() {
            var results = await _agencyService.GetAgenciesAsync();
            // System.Console.WriteLine(results.First().domains);
            return results;
        }
        public async Task<IEnumerable<dynamic>> GetPublishedBriefsAsync() {
            var results = await _briefService.GetPublishedBriefsAsync();
            return results;
        }
        public async Task<IEnumerable<dynamic>> GetSubmittedBriefResponsesAsync() {
            var results = await _briefResponseService.GetSubmittedBriefResponsesAsync();
            return results;
        }        
        public async Task<IEnumerable<dynamic>> GetFeedbacksAsync() {
            var results = await _feedbackService.GetFeedbacksAsync();
            return results;
        }
        public async Task<IEnumerable<dynamic>> GetSuppliersAsync() {
            var results = await _supplierService.GetSuppliersAsync();
            return results;
        }
    }
}
