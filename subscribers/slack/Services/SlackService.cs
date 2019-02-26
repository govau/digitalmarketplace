using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Amazon.SQS.Model;
using Dta.Marketplace.Subscribers.Slack.Model;

namespace Dta.Marketplace.Subscribers.Slack.Services {
    internal class SlackService : ISlackService {
        private readonly ILogger _logger;
        
        public SlackService(ILogger<AppService> logger) {
            _logger = logger;
        }

        public async Task<bool> SendSlackMessage(string slackUrl, string message) {
            if (string.IsNullOrWhiteSpace(slackUrl)) {
                _logger.LogInformation("Slack ({slackUrl}) message: {Message}", slackUrl, message);
                return true;
            }
            using (var client = new HttpClient()) {
                var content = new StringContent(
                    JsonConvert.SerializeObject(
                        new {
                            text = message
                        }
                    ),
                    System.Text.Encoding.Default,
                    "application/json"
                );
                var result = await client.PostAsync(slackUrl, content);
                return result.IsSuccessStatusCode;
            }
        }
    }
}
