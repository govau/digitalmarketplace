using AutoMapper;
using Dta.Marketplace.Api.Entities;
using Dta.Marketplace.Api.Business.Models;

namespace Dta.Marketplace.Api.Mapping {
    public class AutoMapping : Profile {
        public AutoMapping() {
            CreateMap<User, UserModel>();
        }
    }
}
