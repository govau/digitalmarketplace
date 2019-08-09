using System.Threading.Tasks;
using Dta.Marketplace.Subscribers.Email.Logger.Worker.Model;

namespace Dta.Marketplace.Subscribers.Email.Logger.Worker.Processors {
    public interface IEmailLogProcessor {
        bool ProcessMessage(AwsSqsMessage message);
    }
}
