using AutoMapper;
using Dta.Marketplace.Api.Business.Exceptions;
using Dta.Marketplace.Api.Business.Models;
using Dta.Marketplace.Api.Business.Utils;
using Dta.Marketplace.Api.Services.Entities;
using Dta.Marketplace.Api.Services;
using System;
using System.Threading.Tasks;

namespace Dta.Marketplace.Api.Business {
    public class ApiKeyBusiness : IApiKeyBusiness {
        private readonly IApiKeyService _apiKeyService;
        private readonly IDatabaseOperationService _databaseOperationService;

        public ApiKeyBusiness(IApiKeyService apiKeyService, IDatabaseOperationService databaseOperationService) {
            _apiKeyService = apiKeyService;
            _databaseOperationService = databaseOperationService;
        }

        public async Task<string> GenerateTokenAsync(int userId) {
            var apiKey = new ApiKey() {
                CreatedAt = DateTime.Now,
                Key = "",
                UserId = userId
            };
            apiKey = await _databaseOperationService.CreateAsync(apiKey);
            await _databaseOperationService.CommitAsync();
            return apiKey.Key;
        }
        public async Task<bool> RevokeAsync(string key) {
            var apiKey = _apiKeyService.GetForUpdateAsync(key);
            if (apiKey == null) {
                throw new NotFoundException();
            }
            if (await _databaseOperationService.CommitAsync() > 0) {
                return true;
            } else {
                return false;
            }
        }
    }
}
