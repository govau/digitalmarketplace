using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using Dta.Marketplace.Api.Business;

namespace Dta.Marketplace.Api.Web.Handlers {
    public class ApiKeyAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions> {
        private readonly IUserBusiness _userBusiness;

        public ApiKeyAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IUserBusiness userBusiness)
            : base(options, logger, encoder, clock) {
            _userBusiness = userBusiness;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync() {
            await Task.Run(() => {});
            if (!Request.Headers.ContainsKey("X-Api-Key")) {
                return AuthenticateResult.NoResult();
            }

            var apiKey = Request.Headers["X-Api-Key"];
            var user = _userBusiness.GetByApiKey(apiKey);
            if (user == null) {
                return AuthenticateResult.NoResult();
            }

            var identity = new ClaimsIdentity(new Claim[] {
                new Claim(ClaimTypes.NameIdentifier, $"{user.Id}"),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.EmailAddress),
                new Claim(ClaimTypes.Role, user.Role)
            }, Scheme.Name);

            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}
