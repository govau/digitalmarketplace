using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Dta.Marketplace.Api.Business.Exceptions;
using Dta.Marketplace.Api.Business;
using Dta.Marketplace.Api.Shared;
using Dta.Marketplace.Api.Business.Models;
using Dta.Marketplace.Api.Web.Utils;
using System.Threading.Tasks;
using Dta.Marketplace.Api.Services.Entities;

namespace Dta.Marketplace.Api.Web.Controllers {
    // [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AgencyController : ControllerBase {
        private readonly IAgencyBusiness _agencyBusiness;

        public AgencyController(IAgencyBusiness agencyBusiness, IAuthorizationUtil authorizationUtil) {
            _agencyBusiness = agencyBusiness;
        }

        // [Authorize(Roles = Roles.Admin)]
        [HttpGet]
        public async Task<IActionResult> GetAgenciesAsync() {
            var users = await _agencyBusiness.GetAgenciesAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAgencyAsync(int id) {
            // if (!_authorizationUtil.IsUserInRole(User, Roles.Admin) && !_authorizationUtil.IsUserTheSame(User, id)) {
            //     return Forbid();
            // }

            var agency = await _agencyBusiness.GetAgencyAsync(id);
            if (agency == null) {
                return NotFound();
            }

            return Ok(agency);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAgencyAsync([FromBody]Agency model) {
            var result = await _agencyBusiness.UpdateAsync(model.Id, model, "user");
            if (!result){
                return BadRequest();
            }
            return Ok();
            
            // try {
            //     var user = await _userBusiness.AuthenticateAsync(model);
            //     return Ok(user);
            // } catch (CannotAuthenticateException) {
            //     return BadRequest(new { message = "Username or password is incorrect" });
            // }
        }
    }
}
