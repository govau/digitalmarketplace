using System.Threading.Tasks;
using System.Collections.Generic;

namespace Dta.Marketplace.Subscribers.Email.Logger.Worker.Services {
    public interface IEmailBodyService : IBodyService {
        bool SaveEmailBodyMessage(Dictionary <string, string> dataDictToBeStored);
    }
}
