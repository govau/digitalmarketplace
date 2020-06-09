using AutoMapper;
using Dta.Marketplace.Api.Services.Entities;
using Dta.Marketplace.Api.Business.Models;

namespace Dta.Marketplace.Api.Business.Mapping {
    public class AutoMapping : Profile {
        public AutoMapping() {
            CreateMap<User, UserModel>();
            CreateMap<Agency, AgencyModel>();
            CreateMap<AgencyDomain, AgencyDomainModel>();
            CreateMap<AgencyDomainModel, AgencyDomain>();
        }
    }
}
