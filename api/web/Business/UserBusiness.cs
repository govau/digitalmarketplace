using AutoMapper;
using System.Collections.Generic;
using Dta.Marketplace.Api.Web.Business.Exceptions;
using Dta.Marketplace.Api.Web.Business.Interfaces;
using Dta.Marketplace.Api.Web.Services.Interfaces;
using Dta.Marketplace.Api.Web.Models;

namespace Dta.Marketplace.Api.Web.Business {
    public class UserBusiness : IUserBusiness {
        private IUserService _userService;
        private IMapper _mapper;

        public UserBusiness(IUserService userService, IMapper mapper) {
            _userService = userService;
            _mapper = mapper;
        }

        public UserModel Authenticate(AuthenticateModel model) {
            var user = _userService.Authenticate(model.Username, model.Password);

            if (user == null) {
                throw new CannotAuthenticateException();
            }
            return _mapper.Map<UserModel>(user);
        }

        public IEnumerable<UserModel> GetAll() => _mapper.Map<IEnumerable<UserModel>>(_userService.GetAll());
        public UserModel GetById(int id) => _mapper.Map<UserModel>(_userService.GetById(id));
    }
}
