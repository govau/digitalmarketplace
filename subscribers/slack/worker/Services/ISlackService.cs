using System.Threading.Tasks;

namespace Dta.Marketplace.Subscribers.Slack.Worker.Services {
    public interface ISlackService : IService {
        Task<bool> SendSlackMessage(string slackUrl, string message);
    }
}
