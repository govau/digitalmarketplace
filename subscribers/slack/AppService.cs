using Amazon;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Amazon.SQS;
using Amazon.SQS.Model;
using Newtonsoft.Json;
using Dta.Marketplace.Subscribers.Slack.Processors;
using Dta.Marketplace.Subscribers.Slack.Model;

namespace Dta.Marketplace.Subscribers.Slack {
    public class AppService : IHostedService, IDisposable {
        private readonly ILogger _logger;
        private readonly IOptions<AppConfig> _config;
        private readonly Func<string, IMessageProcessor> _messageProcessor;
        private AmazonSQSClient _sqsClient;
        private Timer _timer;
        public AppService(ILogger<AppService> logger, IOptions<AppConfig> config, Func<string, IMessageProcessor> messageProcessor) {
            _logger = logger;
            _config = config;
            _messageProcessor = messageProcessor;
        }

        public Task StartAsync(CancellationToken cancellationToken) {
            _logger.LogInformation("Starting daemon: Slack Subscriber");

            var sqsConfig = new AmazonSQSConfig {
                RegionEndpoint = RegionEndpoint.GetBySystemName(_config.Value.AWS_SQS_REGION)
            };
            if (string.IsNullOrWhiteSpace(_config.Value.AWS_SQS_SERVICE_URL) == false) {
                sqsConfig.ServiceURL = _config.Value.AWS_SQS_SERVICE_URL;
            }
            if (string.IsNullOrWhiteSpace(_config.Value.AWS_SQS_SECRET_ACCESS_KEY) == false &&
                string.IsNullOrWhiteSpace(_config.Value.AWS_SQS_ACCESS_KEY_ID) == false) {
                _sqsClient = new AmazonSQSClient(_config.Value.AWS_SQS_ACCESS_KEY_ID, _config.Value.AWS_SQS_SECRET_ACCESS_KEY, sqsConfig);
            } else {
                _sqsClient = new AmazonSQSClient(sqsConfig);
            }

            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(_config.Value.WORK_INTERVAL_IN_SECONDS));
            return Task.CompletedTask;
        }
        private async void DoWork(object state) {
            var receiveMessageRequest = new ReceiveMessageRequest() {
                QueueUrl = _config.Value.AWS_SQS_QUEUE_URL,
                WaitTimeSeconds = _config.Value.AWS_SQS_LONG_POLL_TIME_IN_SECONDS
            };
            var receiveMessageResponse = await _sqsClient.ReceiveMessageAsync(receiveMessageRequest);
            foreach (var message in receiveMessageResponse.Messages) {
                _logger.LogInformation($"Message Id: {message.MessageId}");
                var awsSnsMessage = AwsSnsMessage.FromJson(message.Body);
                var messageProcessor = _messageProcessor(awsSnsMessage.MessageAttributes.ObjectType.Value);
                if (messageProcessor == null) {
                    _logger.LogWarning($"Message processor not found for '{awsSnsMessage.MessageAttributes.ObjectType.Value}'. Deleting message");
                    await DeleteMessage(message);
                    continue;
                }
                var result = await messageProcessor.ProcessMessage(awsSnsMessage);
                if (result == true) {
                    await DeleteMessage(message);
                }
            }
        }
        public Task StopAsync(CancellationToken cancellationToken) {
            _logger.LogInformation("Stopping daemon.");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose() {
            _logger.LogInformation("Disposing....");
            _timer?.Dispose();
            if (_sqsClient != null) {
                _sqsClient.Dispose();
            }
        }

        private async Task DeleteMessage(Message message) {
            var deleteMessageRequest = new DeleteMessageRequest {
                QueueUrl = _config.Value.AWS_SQS_QUEUE_URL,
                ReceiptHandle = message.ReceiptHandle
            };
            await _sqsClient.DeleteMessageAsync(deleteMessageRequest);
        }
    }
}
