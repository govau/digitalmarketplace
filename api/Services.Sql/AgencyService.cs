using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json;
using Dapper;
using Dta.Marketplace.Api.Services;

namespace Dta.Marketplace.Api.Services.Sql {
    public class AgencyService : IAgencyService {
        private readonly DigitalMarketplaceContext _context;

        public AgencyService(DigitalMarketplaceContext context) {
            _context = context;
        }
        public async Task<IEnumerable<dynamic>> GetAgenciesAsync() {
            var connection = _context.Database.GetDbConnection();
            return await connection.QueryAsync<dynamic, dynamic, dynamic>(
                sql: @"
                    SELECT
                        a.id,
                        a.name,
                        a.domain,
                        a.category,
                        a.state,
                        a.body_type ""bodyType"",
                        a.whitelisted,
                        a.reports,
                        r.domains
                    FROM agency a
                    INNER JOIN (
                        SELECT
                            agency_id,
                            json_agg(
                                json_build_object(
                                    'id', id,
                                    'domain', domain,
                                    'active', active
                                )
                            ) ""domains""
                        FROM agency_domain ad
                        GROUP BY ad.agency_id
                    ) r on r.agency_id = a.id
                    ORDER BY a.name
                ",
                map: (a, ad) => {
                    a.domains = JsonSerializer.Deserialize<dynamic>(ad.domains);
                    return a;
                }, 
                splitOn: "domains");
        }

        public async Task<IEnumerable<dynamic>> GetAgencyAsync(int agencyId) {
            var connection = _context.Database.GetDbConnection();
            return await connection.QueryAsync<dynamic, dynamic, dynamic>(
                sql: string.Format(@"
                    SELECT
                        a.id,
                        a.name,
                        a.domain,
                        a.category,
                        a.state,
                        a.body_type ""bodyType"",
                        a.whitelisted,
                        a.reports,
                        a.must_join_team,
                        r.domains
                    FROM agency a
                    INNER JOIN (
                        SELECT
                            agency_id,
                            json_agg(
                                json_build_object(
                                    'id', id,
                                    'domain', domain,
                                    'active', active
                                )
                            ) ""domains""
                        FROM agency_domain ad
                        GROUP BY ad.agency_id
                    ) r on r.agency_id = {0}
                    WHERE a.id = 1
                    ORDER BY a.name
                ", agencyId.ToString()),
                map: (a, ad) => {
                    a.domains = JsonSerializer.Deserialize<dynamic>(ad.domains);
                    return a;
                }, 
                splitOn: "domains");
        }

        public async Task<IEnumerable<dynamic>> UpdateAsync(int agency_id, dynamic agency, string updated_by) {
            var connection = _context.Database.GetDbConnection();
            return await connection.QueryAsync<dynamic, dynamic, dynamic>(
                sql: @"
                    SELECT
                        a.id,
                        a.name,
                        a.domain,
                        a.category,
                        a.state,
                        a.body_type ""bodyType"",
                        a.whitelisted,
                        a.reports,
                        r.domains
                    FROM agency a
                    INNER JOIN (
                        SELECT
                            agency_id,
                            json_agg(
                                json_build_object(
                                    'id', id,
                                    'domain', domain,
                                    'active', active
                                )
                            ) ""domains""
                        FROM agency_domain ad
                        GROUP BY ad.agency_id
                    ) r on r.agency_id = a.id
                    ORDER BY a.name
                ",
                map: (a, ad) => {
                    a.domains = JsonSerializer.Deserialize<dynamic>(ad.domains);
                    return a;
                }, 
                splitOn: "domains");
        }
    }
}
