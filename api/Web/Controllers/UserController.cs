using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Dta.Marketplace.Api.Business.Exceptions;
using Dta.Marketplace.Api.Business;
using Dta.Marketplace.Api.Shared;
using Dta.Marketplace.Api.Business.Models;
using Dta.Marketplace.Api.Web.Utils;
using System.Threading.Tasks;

namespace Dta.Marketplace.Api.Web.Controllers {
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase {
        private readonly IAuthorizationUtil _authorizationUtil; 
        private readonly IUserBusiness _userBusiness;

        public UserController(IUserBusiness userBusiness, IAuthorizationUtil authorizationUtil) {
            _userBusiness = userBusiness;
            _authorizationUtil = authorizationUtil;
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
        public async Task<IActionResult> GetAll() {
            var users = await _userBusiness.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id) {
            if (!_authorizationUtil.IsUserInRole(User, Roles.Admin) && !_authorizationUtil.IsUserTheSame(User, id)) {
                return Forbid();
            }

            var user = await _userBusiness.GetByIdAsync(id);
            if (user == null) {
                return NotFound();
            }

            return Ok(user);
        }
    }
}
