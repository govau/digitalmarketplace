using Lamar;

namespace Dta.Marketplace.Api.Web {
    public class SelfServiceRegistry: ServiceRegistry {
        public SelfServiceRegistry() {
            Scan(x => {
                x.Assembly(typeof(Program).Assembly);
                x.WithDefaultConventions();
            });
        }
    }
}
