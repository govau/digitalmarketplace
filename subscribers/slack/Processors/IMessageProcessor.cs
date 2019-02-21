using System.Threading.Tasks;
using Dta.Marketplace.Subscriber.Slack.Model;

namespace Dta.Marketplace.Subscriber.Slack.Processors {
    public interface IMessageProcessor {
        Task<bool> ProcessMessage(AwsSnsMessage message);
    }
}
