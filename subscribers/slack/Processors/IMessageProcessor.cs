using System.Threading.Tasks;
using Dta.Marketplace.Subscribers.Slack.Model;

namespace Dta.Marketplace.Subscribers.Slack.Processors {
    public interface IMessageProcessor {
        Task<bool> ProcessMessage(AwsSnsMessage message);
    }
}
