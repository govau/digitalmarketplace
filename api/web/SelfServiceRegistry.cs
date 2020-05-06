using Lamar;
using Dta.Marketplace.Api.Business;
using Dta.Marketplace.Api.Business.Impl;
using Dta.Marketplace.Api.Services;
using Dta.Marketplace.Api.Services.Impl;

namespace Dta.Marketplace.Api.Web {
    public class SelfServiceRegistry: ServiceRegistry {
        public SelfServiceRegistry() {
            Scan(x => {
                x.Assembly(typeof(BaseBusiness).Assembly);
                x.WithDefaultConventions();
            });
            Scan(x => {
                x.Assembly(typeof(IBusiness).Assembly);
                x.WithDefaultConventions();
            });
            Scan(x => {
                x.Assembly(typeof(BaseService).Assembly);
                x.WithDefaultConventions();
            });
            Scan(x => {
                x.Assembly(typeof(IService).Assembly);
                x.WithDefaultConventions();
            });
        }
    }
}
