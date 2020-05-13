using AutoMapper;
using Dta.Marketplace.Api.Business.Exceptions;
using Dta.Marketplace.Api.Business.Models;
using Dta.Marketplace.Api.Business.Utils;
using Dta.Marketplace.Api.Services;
using System;
using System.Threading.Tasks;

namespace Dta.Marketplace.Api.Business {
    public class UserSessionBusiness : IUserSessionBusiness {
        private readonly IEncryptionUtil _encryptionUtil;
        private readonly IUserService _userService;
        private readonly IUserSessionService _userSessionService;
        private readonly IMapper _mapper;

        public UserSessionBusiness(IEncryptionUtil encryptionUtil, IUserService userService, IUserSessionService userSessionService, IMapper mapper) {
            _userSessionService = userSessionService;
            _userService = userService;
            _mapper = mapper;
            _encryptionUtil = encryptionUtil;
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
            string token = _encryptionUtil.Encrypt($"{user.Id}+{user.Name}+{user.EmailAddress}+{Guid.NewGuid()}");
            await _userSessionService.CreateAsync(user.Id, token);
            return token;
        }
    }
}
