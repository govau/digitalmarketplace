using System.Threading.Tasks;

namespace Dta.Marketplace.Subscriber.Slack.Services {
    public interface ISlackService : IService {
        Task<bool> SendSlackMessage(string slackUrl, string message);
    }
}
