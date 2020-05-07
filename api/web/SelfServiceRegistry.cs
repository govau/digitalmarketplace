using Lamar;

namespace Dta.Marketplace.Api.Web {
    public class SelfServiceRegistry: ServiceRegistry {
        public SelfServiceRegistry() {
            Scan(x => {
                x.AssembliesFromApplicationBaseDirectory(af => af.GetName().Name.StartsWith("Dta.Marketplace.Api"));
                x.WithDefaultConventions();
            });
        }
    }
}
