using System.Threading.Tasks;
using Dta.Marketplace.Subscribers.Slack.Worker.Model;

namespace Dta.Marketplace.Subscribers.Slack.Worker.Processors {
    public interface IMessageProcessor {
        Task<bool> ProcessMessage(AwsSnsMessage message);
    }
}
