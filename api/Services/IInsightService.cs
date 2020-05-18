using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dta.Marketplace.Api.Services.Entities;

namespace Dta.Marketplace.Api.Services {
    public interface IInsightService {
        Task<Insight> GetInsight(DateTime? now, bool? activeOnly);
        Task<Insight> GetInsightForUpdate(DateTime now);
    }
}
