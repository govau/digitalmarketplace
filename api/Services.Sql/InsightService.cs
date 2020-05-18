using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dta.Marketplace.Api.Services.Entities;
using Dta.Marketplace.Api.Shared.Extensions;

namespace Dta.Marketplace.Api.Services.Sql {
    public class InsightService : IInsightService {
        private readonly DigitalMarketplaceContext _context;

        public InsightService(DigitalMarketplaceContext context) {
            _context = context;
        }
        public async Task<Insight> GetInsight(DateTime? now, bool? activeOnly) {
            var query = _context
                .Insight
                .AsNoTracking()
                .AsQueryable();

            if (activeOnly.HasValue) {
                query = query.Where(i => i.Active == activeOnly.Value);
            }
            if (now.HasValue) {
                return await query
                    .Where(i =>
                        i.PublishedAt >= now.Value.FirstDayOfMonth() &&
                        i.PublishedAt <= now.Value.LastDayOfMonth()
                    )
                    .SingleOrDefaultAsync();
            } else {
                return await query
                    .OrderByDescending(i => i.PublishedAt)
                    .FirstOrDefaultAsync();
            }
        }
        public async Task<Insight> GetInsightForUpdate(DateTime now) {
            return await _context
                .Insight
                .Where(i =>
                    i.PublishedAt >= now.FirstDayOfMonth() &&
                    i.PublishedAt <= now.LastDayOfMonth()
                )
                .SingleOrDefaultAsync();
        }
    }
}
