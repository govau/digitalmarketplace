using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json;
using Dapper;
using Dta.Marketplace.Api.Services.Reports;

namespace Dta.Marketplace.Api.Services.Sql.Reports {
    public class BriefResponseService : IBriefResponseService {
        private readonly DigitalMarketplaceContext _context;

        public BriefResponseService(DigitalMarketplaceContext context) {
            _context = context;
        }
        public async Task<IEnumerable<dynamic>> GetSubmittedBriefResponsesAsync() {
            var connection = _context.Database.GetDbConnection();
            return await connection.QueryAsync<dynamic>(
                sql: @"
                    SELECT
                        br.brief_id,
                        br.supplier_code,
                        br.created_at,
                        br.data ->> 'dayRate' AS dayRate,
                        l.name as brief_type,
                        br.data ->> 'areaOfExpertise' AS areaOfExpertise
                    FROM brief_response br	
                    INNER JOIN brief b ON b.id = br.brief_id
                    INNER JOIN lot l  ON l.id = br.brief_id
                    ORDER BY b.id
                ");
        }
    }
}
