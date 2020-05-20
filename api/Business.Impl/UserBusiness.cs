using AutoMapper;
using Dta.Marketplace.Api.Business.Exceptions;
using Dta.Marketplace.Api.Business.Models;
using Dta.Marketplace.Api.Business.Utils;
using Dta.Marketplace.Api.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dta.Marketplace.Api.Business {
    public class UserBusiness : IUserBusiness {
        private readonly IApiKeyService _apiKeyService;
        private readonly IEncryptionUtil _encryptionUtil;
        private readonly IUserService _userService;
        private readonly IUserSessionBusiness _userSessionBusiness;
        private readonly IMapper _mapper;

        public UserBusiness(IApiKeyService apiKeyService, IEncryptionUtil encryptionUtil, IUserService userService, IUserSessionBusiness userSessionBusiness, IMapper mapper) {
            _apiKeyService = apiKeyService;
            _userService = userService;
            _userSessionBusiness = userSessionBusiness;
            _mapper = mapper;
            _encryptionUtil = encryptionUtil;
        }

        public async Task<UserModel> AuthenticateAsync(AuthenticateModel model) {
            string encryptedPassword = _encryptionUtil.Encrypt(model.Password);

            var user = await _userService.AuthenticateAsync(model.Username, encryptedPassword);
            if (user == null) {
                throw new CannotAuthenticateException();
            }
            var result = _mapper.Map<UserModel>(user);
            result.Token = await _userSessionBusiness.CreateSessionAsync(result);
            return result;
        }
        public async Task<IEnumerable<UserModel>> GetAllAsync() => _mapper.Map<IEnumerable<UserModel>>(await _userService.GetAllAsync());
        public async Task<UserModel> GetByIdAsync(int id) => _mapper.Map<UserModel>(await _userService.GetByIdAsync(id));
        public async Task<UserModel> AuthenticateByApiKeyAsync(string apiKey) {
            var api = await _apiKeyService.GetAsync(apiKey);
            if (api == null) {
                throw new CannotAuthenticateException();
            }
            return _mapper.Map<UserModel>(api.User);
        }
    }
}
