using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Dta.Marketplace.Api.Services.Entities;

namespace Dta.Marketplace.Api.Services.Sql {
    public class AgencyService : IAgencyService {
        private readonly DigitalMarketplaceContext _context;
        public AgencyService(DigitalMarketplaceContext context) {
            _context = context;
        }
        public async Task<Agency> GetAgencyForUpdateAsync(int id) => await _context.Agency.Include(x => x.AgencyDomain).Where(x => x.Id == id).SingleOrDefaultAsync();
        public async Task<Agency> GetOrAddAgencyAsync(string domain) {
            domain = domain.ToLower();
            var agency = await this.GetAgencyByDomainAsync(domain);
            if (agency == null){
                agency = new Agency();
                agency.Name = domain;
                agency.Domain = domain;
                agency.Category = "Commonwealth";
                agency.State = "ACT";
                agency.Whitelisted = true;
                agency.BodyType = BodyType.other;
                agency.Reports = true;
                agency.AgencyDomain = new AgencyDomain[]{
                    new AgencyDomain() {
                        Domain = domain,
                        Active = true
                    }
                };
                // TODO:
                // publish_tasks.agency.delay(
                //     publish_tasks.compress_agency(agency),
                //     'created'
                // )
                _context.Agency.Add(agency);
                await _context.SaveChangesAsync();
            }
            return agency;
        }
        public async Task<string> GetAgencyNameAsync(int id) {
            var agency = await _context.Agency.Where(x => x.Id == id).SingleOrDefaultAsync();
            if (agency == null){
                return agency.Name;
            }
            return "Unknown";
        }
        public async Task<Agency> GetAgencyByDomainAsync(string domain) {
            var agency = await _context.Agency.Where(x => x.Domain == domain || x.AgencyDomain.Any(ad => ad.Domain == domain)).SingleOrDefaultAsync();
            return agency;
        }
        public async Task<IEnumerable<AgencyDomain>> GetAgencyDomainsAsync(int id) => await _context.AgencyDomain.Where(x => x.AgencyId == id).ToListAsync();
        public async Task<IEnumerable<Agency>> GetAgenciesAsync() => await _context.Agency.ToListAsync();
        public async Task<Agency> GetAgencyAsync(int id) => await _context.Agency.Include(x => x.AgencyDomain).Where(x => x.Id == id).SingleOrDefaultAsync();
        public async Task<bool> AgencyRequiresTeamMembershipAsync(int id){
            var agency = await _context.Agency.Where(x => x.Id == id).SingleOrDefaultAsync();
            // TODO:
            // if (agency != null && agency.RequiresTeamMembership){
            //     return true;
            // }
            return false;
        }
        public async Task<bool> UpdateAsync() {
            var result = await _context.SaveChangesAsync();
            return true;
        }
    }
}
