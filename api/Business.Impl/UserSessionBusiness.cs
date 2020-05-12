using AutoMapper;
using Dta.Marketplace.Api.Business.Exceptions;
using Dta.Marketplace.Api.Shared;
using Dta.Marketplace.Api.Business.Models;
using Dta.Marketplace.Api.Services;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Dta.Marketplace.Api.Business {
    public class UserSessionBusiness : IUserSessionBusiness {
        private readonly AppSettings _appSettings;
        private IUserService _userService;
        private IUserSessionService _userSessionService;
        private IMapper _mapper;

        public UserSessionBusiness(IOptions<AppSettings> appSettings, IUserService userService, IUserSessionService userSessionService, IMapper mapper) {
            _appSettings = appSettings.Value;
            _userSessionService = userSessionService;
            _userService = userService;
            _mapper = mapper;
        }
        public async Task<UserModel> GetSessionAsync(string token) {
            var userId = await _userSessionService.GetAsync(token);
            if (!userId.HasValue) {
                throw new SessionExpiredException();
            }
            return _mapper.Map<UserModel>(await _userService.GetByIdAsync(userId.Value));
        }
        public async Task<string> CreateSessionAsync(UserModel user) {
            if (user == null) {
                throw new ArgumentNullException("User cannot be null");
            }

            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create()) {
                rng.GetBytes(salt);
            }

            string token = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: $"{user.Id}+{user.Name}+{user.EmailAddress}+{Guid.NewGuid()}",
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            await _userSessionService.CreateAsync(user.Id, token);
            return token;
        }
    }
}
