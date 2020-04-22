using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Dta.Marketplace.Api.Web.Business.Exceptions;
using Dta.Marketplace.Api.Web.Business.Interfaces;
using Dta.Marketplace.Api.Web.Helpers;
using Dta.Marketplace.Api.Web.Models;

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
        public IActionResult Authenticate([FromBody]AuthenticateModel model) {
            try {
                var user = _userBusiness.Authenticate(model);
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
            // only allow admins to access other user records
            var currentUserId = int.Parse(User.Identity.Name);
            if (id != currentUserId && !User.IsInRole(Roles.Admin)) {
                return Forbid();
            }

            var user = _userBusiness.GetById(id);
            if (user == null) {
                return NotFound();
            }

            return Ok(user);
        }
    }
}
