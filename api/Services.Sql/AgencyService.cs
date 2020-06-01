using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text.Json;
using Dapper;
using Dta.Marketplace.Api.Services;
using Dta.Marketplace.Api.Services.Entities;

namespace Dta.Marketplace.Api.Services.Sql {
    public class AgencyService : IAgencyService {
        private readonly DigitalMarketplaceContext _context;
        public AgencyService(DigitalMarketplaceContext context) {
            _context = context;
        }        
        public async Task<IEnumerable<dynamic>> GetAgenciesAsync() => await _context.Agency.ToListAsync();
        public async Task<dynamic> GetAgencyAsync(int id) => await _context.Agency.Where(x => x.Id == id).SingleOrDefaultAsync();
        
        public async Task<bool> UpdateAsync(int id, dynamic agency, string updatedBy) {
            var existing = await _context.Agency.Where(x => x.Id == id).SingleOrDefaultAsync();
            if (existing == null){
                return false;
            }
            var changed = false;
            Agency updated = agency;
            if (agency.Name != null && existing.Name != updated.Name){
                existing.Name = updated.Name;
                changed = true;
            }
            if (agency.Category != null && existing.Category != updated.Category){
                existing.Category = updated.Category;
                changed = true;
            }
            if (agency.BodyType != null && existing.BodyType != updated.BodyType){
                existing.BodyType = updated.BodyType;
                changed = true;
            }
            if (agency.Whitelisted != null && existing.Whitelisted != updated.Whitelisted){
                existing.Whitelisted = updated.Whitelisted;
                changed = true;
            }
            if (agency.Reports != null && existing.Reports != updated.Reports){
                existing.Reports = updated.Reports;
                changed = true;
            }
            // if agency.get('must_join_team', None) is not None:
            if (agency.State != null && existing.State != updated.State){
                existing.State = updated.State;
                changed = true;
            }
            if (agency.Domains != null){
                existing.Domain = updated.Domain;
                return true;
                       // domains = agency.get('domains', [])
        // to_remove = []
        // to_add = []
        // for e in existing.domains:
        //     if e.domain not in domains:
        //         to_remove.append(e)

        // for d in domains:
        //     if d not in [e.domain for e in existing.domains]:
        //         to_add.append(AgencyDomain(active=True, domain=d))

        // for e in to_remove:
        //     existing.domains.remove(e)
        // for e in to_add:
        //     existing.domains.append(e)
            }            
            if (!changed){
                return true;
            }
            var result = await _context.SaveChangesAsync();

//    updated = agency_service.save(existing)
//     result = get_agency(updated.id)
//     audit_service.log_audit_event(
//         audit_type=audit_types.agency_updated,
//         user=updated_by,
//         data={
//             'incoming': agency,
//             'saved': result
//         },
//         db_object=updated)
//     return result

            return true;
        }
    }
}
