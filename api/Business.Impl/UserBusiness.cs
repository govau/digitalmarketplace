using AutoMapper;
using Dta.Marketplace.Api.Business.Exceptions;
using Dta.Marketplace.Api.Services.Entities;
using Dta.Marketplace.Api.Shared;
using Dta.Marketplace.Api.Business.Models;
using Dta.Marketplace.Api.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Dta.Marketplace.Api.Business {
    public class UserBusiness : IUserBusiness {
        private readonly AppSettings _appSettings;
        private IUserService _userService;
        private IMapper _mapper;

        public UserBusiness(IOptions<AppSettings> appSettings, IUserService userService, IMapper mapper) {
            _appSettings = appSettings.Value;
            _userService = userService;
            _mapper = mapper;
        }

        public UserModel Authenticate(AuthenticateModel model) {
            var user = _userService.Authenticate(model.Username, model.Password);

            if (user == null) {
                throw new CannotAuthenticateException();
            }

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, Enum.GetName(typeof(UserRole), user.Role))
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            var result = _mapper.Map<UserModel>(user);
            result.Token = tokenHandler.WriteToken(token);
            return result;
        }

        public IEnumerable<UserModel> GetAll() => _mapper.Map<IEnumerable<UserModel>>(_userService.GetAll());
        public UserModel GetById(int id) => _mapper.Map<UserModel>(_userService.GetById(id));
    }
}
