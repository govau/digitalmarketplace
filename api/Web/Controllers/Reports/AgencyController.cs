using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Dta.Marketplace.Api.Shared;
using Dta.Marketplace.Api.Business.Reports;
using System.Threading.Tasks;

namespace Dta.Marketplace.Api.Web.Controllers.Reports {
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AgencyController : ControllerBase {
        private readonly IAgencyBusiness _agencyBusiness;

        public AgencyController(IAgencyBusiness agencyBusiness) {
            _agencyBusiness = agencyBusiness;
        }

        [Authorize(AuthenticationSchemes = Schemes.ApiKeyAuthenticationHandler)]
        [HttpGet("report")]
        public async Task<IActionResult> GetAgenciesAsync(int id) {
            var agencies = await _agencyBusiness.GetAgenciesAsync();
            return Ok(agencies);
        }
    }
}
