using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Dta.Marketplace.Api.Business.Exceptions;
using Dta.Marketplace.Api.Business;
using Dta.Marketplace.Api.Shared;
using Dta.Marketplace.Api.Business.Models;
using System.Threading.Tasks;

namespace Dta.Marketplace.Api.Web.Controllers {
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase {
        private IUserBusiness _userBusiness;

        public UsersController(IUserBusiness userBusiness) {
            _userBusiness = userBusiness;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync([FromBody]AuthenticateModel model) {
            try {
                var user = await _userBusiness.AuthenticateAsync(model);
                return Ok(user);
            } catch (CannotAuthenticateException) {
                return BadRequest(new { message = "Username or password is incorrect" });
            }
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpGet]
        public IActionResult GetAll() {
            var users = _userBusiness.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id) {
            var currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (id != currentUserId && !User.IsInRole(Roles.Admin)) {
                return Forbid();
            }

            var user = _userBusiness.GetById(id);
            if (user == null) {
                return NotFound();
            }

            return Ok(user);
        }

        [Authorize(AuthenticationSchemes = Schemes.ApiKeyAuthenticationHandler, Roles = Roles.Admin)]
        [HttpGet("api/{id}")]
        public IActionResult GetByIdApi(int id) {
            // var currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            // if (id != currentUserId && !User.IsInRole(Roles.Admin)) {
            //     return Forbid();
            // }

            var user = _userBusiness.GetById(id);
            if (user == null) {
                return NotFound();
            }

            return Ok(user);
        }
    }
}
