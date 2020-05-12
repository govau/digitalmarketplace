using AutoMapper;
using Dta.Marketplace.Api.Business.Exceptions;
using Dta.Marketplace.Api.Shared;
using Dta.Marketplace.Api.Business.Models;
using Dta.Marketplace.Api.Services;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dta.Marketplace.Api.Business {
    public class UserBusiness : IUserBusiness {
        private readonly AppSettings _appSettings;
        private IApiKeyService _apiKeyService;
        private IUserService _userService;
        private IUserSessionBusiness _userSessionBusiness;
        private IMapper _mapper;

        public UserBusiness(IOptions<AppSettings> appSettings, IApiKeyService apiKeyService, IUserService userService, IUserSessionBusiness userSessionBusiness, IMapper mapper) {
            _appSettings = appSettings.Value;
            _apiKeyService = apiKeyService;
            _userService = userService;
            _userSessionBusiness = userSessionBusiness;
            _mapper = mapper;
        }

        public async Task<UserModel> AuthenticateAsync(AuthenticateModel model) {
            var user = _userService.Authenticate(model.Username, model.Password);

            if (user == null) {
                throw new CannotAuthenticateException();
            }
            var result = _mapper.Map<UserModel>(user);
            var token = await _userSessionBusiness.CreateSessionAsync(result);
            result.Token = token;
            return result;
        }

        public IEnumerable<UserModel> GetAll() => _mapper.Map<IEnumerable<UserModel>>(_userService.GetAll());
        public UserModel GetById(int id) => _mapper.Map<UserModel>(_userService.GetById(id));

        public async Task<UserModel> GetByApiKeyAsync(string apiKey) {
            var api = await _apiKeyService.GetAsync(apiKey);
            return _mapper.Map<UserModel>(api.User);
        }
    }
}
