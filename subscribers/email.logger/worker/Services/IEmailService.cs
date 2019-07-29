using System.Threading.Tasks;
using System.Collections.Generic;

namespace Dta.Marketplace.Subscribers.Email.Logger.Worker.Services {
    public interface IEmailService : IService {
        bool SaveEmailMessage(Dictionary <string, string> dataDictToBeStored);
    }
}
