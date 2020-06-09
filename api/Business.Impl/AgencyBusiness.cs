using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Dta.Marketplace.Api.Business.Models;
using Dta.Marketplace.Api.Services;
using Dta.Marketplace.Api.Services.Entities;

namespace Dta.Marketplace.Api.Business {
    public class AgencyBusiness : IAgencyBusiness {
        private readonly IAgencyService _agencyService;
        private readonly IAuditService _auditService;
        private readonly IMapper _mapper;
        public AgencyBusiness(
            IAgencyService agencyService,
            IAuditService auditService,
            IMapper mapper
        ) {
            _agencyService = agencyService;
            _auditService = auditService;
            _mapper = mapper;
        }
        public async Task<IEnumerable<AgencyModel>> GetAgenciesAsync() =>  _mapper.Map<IEnumerable<AgencyModel>>(await _agencyService.GetAgenciesAsync());
        public async Task<AgencyModel> GetAgencyAsync(int id) => _mapper.Map<AgencyModel>(await _agencyService.GetAgencyAsync(id));
        public async Task<bool> UpdateAsync(int id, dynamic agency, string updatedBy) {
            var existing = await _agencyService.GetAgencyForUpdateAsync(id);
            if (existing == null){
                return false;
            }
            var changed = false;
            AgencyModel updated = _mapper.Map<AgencyModel>(agency);
            if (agency.Name != null && existing.Name != updated.Name){
                existing.Name = updated.Name;
                changed = true;
            }
            if (agency.Category != null && existing.Category != updated.Category){
                existing.Category = updated.Category;
                changed = true;
            }
            // TODO: if (agency.BodyType != null && existing.BodyType != updated.BodyType){
            //     existing.BodyType = updated.BodyType;
            //     changed = true;
            // }
            if (agency.Whitelisted != null && existing.Whitelisted != updated.Whitelisted){
                existing.Whitelisted = updated.Whitelisted;
                changed = true;
            }
            if (agency.Reports != null && existing.Reports != updated.Reports){
                existing.Reports = updated.Reports;
                changed = true;
            }
            // TODO: if agency.get('must_join_team', None) is not None:
            if (agency.State != null && existing.State != updated.State){
                existing.State = updated.State;
                changed = true;
            }
            if (agency.Domain != null && existing.Domain != updated.Domain){
                existing.Domain = updated.Domain;
                changed = true;
            }
            if (agency.AgencyDomain != null){
                var adds = new List<AgencyDomainModel>();
                foreach (var ud in updated.AgencyDomain){
                    var add = true;
                    foreach (var ed in existing.AgencyDomain){
                        if (ed.Domain == ud.Domain){
                            add = false;
                            break;
                        }
                    }
                    if (add){
                        adds.Add(ud);
                    }
                }
                var removes = new List<AgencyDomain>();
                foreach (var ed in existing.AgencyDomain){
                    var remove = true;
                    foreach (var ud in updated.AgencyDomain){
                        if (ed.Domain == ud.Domain){
                            remove = false;
                            break;
                        }
                    }
                    if (remove){
                        removes.Add(ed);
                    }
                }
                if (adds.Count  > 0|| removes.Count > 0){
                    foreach (var add in adds){
                        existing.AgencyDomain.Add(_mapper.Map<AgencyDomain>(add));
                    }
                    foreach (var remove in removes){
                        existing.AgencyDomain.Remove(remove);
                    }
                    changed = true;
                }
            }
            if (!changed){
                return true;
            }
            var result = await _agencyService.UpdateAsync();            
            var updatedEntity = await _agencyService.GetAgencyAsync(updated.Id);
            var updatedModel = _mapper.Map<AgencyModel>(updatedEntity);
            var incoming = JsonSerializer.Serialize(agency);
            var saved = JsonSerializer.Serialize(updatedModel);
            var data = "{ \"incoming\":" + incoming + ", \"saved\": " + saved + "}";
            await _auditService.LogAuditEventAsync(AuditType.agency_updated, updatedBy, data, "Agency", updated.Id);
            return true;
        } 
    }
}
