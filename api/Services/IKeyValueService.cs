using System.Collections.Generic;
using System.Threading.Tasks;
using Dta.Marketplace.Api.Services.Entities;

namespace Dta.Marketplace.Api.Services {
    public interface IKeyValueService {
        Task<KeyValue> Upsert(string key, dynamic data);
        Task<KeyValue> GetByKey(string key);
        Task<KeyValue> GetByKeys(params string[] keys);
    }
}
