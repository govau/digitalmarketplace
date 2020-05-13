using AutoMapper;
using Dta.Marketplace.Api.Business.Exceptions;
using Dta.Marketplace.Api.Shared;
using Dta.Marketplace.Api.Business.Models;
using Dta.Marketplace.Api.Services;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

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
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create()) {
                rng.GetBytes(salt);
            }

            string encryptedPassword = Convert.ToBase64String(
                KeyDerivation.Pbkdf2(
                    password: model.Password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8
                )
            );

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
            return _mapper.Map<UserModel>(api.User);
        }
    }
}
