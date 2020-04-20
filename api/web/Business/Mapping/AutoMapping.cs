using AutoMapper;
using Dta.Marketplace.Api.Web.Entities;
using Dta.Marketplace.Api.Web.Models;

namespace Dta.Marketplace.Api.Web.Mapping {
    public class AutoMapping : Profile {
        public AutoMapping() {
            CreateMap<User, UserModel>();
        }
    }
}
