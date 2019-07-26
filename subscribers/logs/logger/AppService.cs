/* */
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

namespace Dta.Marketplace.Subscribers.Logger.Worker{
    public class AppService : IHostedService, IDisposable {
        private readonly ILogger _logger;
        private readonly IOptions<AppConfig> _config;
        private readonly ILoggerContext _loggerContext;
        private readonly IMessageProcessor _messageProcessor;
        private AmazonSQSClient _sqsClient;
        private Timer _timer;

        public AppService(ILogger<AppService> logger, IOptions<AppConfig> config, ILoggerContext loggerContext, IMessageProcessor messageProcessor) {
            _logger = logger;
            _config = config;
            _messageProcessor = messageProcessor;
            _loggerContext = loggerContext;
        }

        public Task StartAsync(CancellationToken cancellationToken) {
            _logger.LogInformation("Starting daemon: Logger. {Timer}", new {
                _config.Value.AwsSqsLongPollTimeInSeconds,
                _config.Value.WorkIntervalInSeconds,
                _config.Value.AwsSqsRegion,
                _config.Value.AwsSqsServiceUrl,
                _config.Value.AwsSqsQueueUrl,
                sentryEnabled = string.IsNullOrWhiteSpace(_config.Value.SentryDsn) ? false : true
            });

            var sqsConfig = new AmazonSQSConfig {
                RegionEndpoint = RegionEndpoint.GetBySystemName(_config.Value.AwsSqsRegion)
            };
            if (string.IsNullOrWhiteSpace(_config.Value.AwsSqsServiceUrl) == false) {
                sqsConfig.ServiceURL = _config.Value.AwsSqsServiceUrl;
            }
            if (string.IsNullOrWhiteSpace(_config.Value.AwsSqsSecretAccessKey) == false &&
                string.IsNullOrWhiteSpace(_config.Value.AwsSqsAccessKeyId) == false) {
                _sqsClient = new AmazonSQSClient(_config.Value.AwsSqsAccessKeyId, _config.Value.AwsSqsSecretAccessKey, sqsConfig);
            } else {
                _sqsClient = new AmazonSQSClient(sqsConfig);
            }

            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(_config.Value.WorkIntervalInSeconds));
            return Task.CompletedTask;
        }
        public Task StopAsync(CancellationToken cancellationToken) {
            _logger.LogInformation("Stopping daemon.");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
        public void Dispose() {
            _logger.LogInformation("Disposing....");
            _timer?.Dispose();
            _sqsClient?.Dispose();
        }
        private async void DoWork(object state) {
            var receiveMessageRequest = new ReceiveMessageRequest() {
                QueueUrl = _config.Value.AwsSqsQueueUrl,
                WaitTimeSeconds = _config.Value.AwsSqsLongPollTimeInSeconds
            };
            _logger.LogDebug("Heartbeat: {Now}", DateTime.Now);
            var receiveMessageResponse = await _sqsClient.ReceiveMessageAsync(receiveMessageRequest);
                foreach (var message in receiveMessageResponse.Messages) {
                    // _logger.LogDebug("Message Id: {MessageId}", message.MessageId);
                    // _logger.LogInformation(message.Body);
                    // _loggerContext.LogEntry.Add(new LogEntry { Data = message.Body });
                    // _loggerContext.SaveChanges();
                    _messageProcessor.Process(message);
                    await DeleteMessage(message);
                }
        }

        private async Task DeleteMessage(Message message) {
            var deleteMessageRequest = new DeleteMessageRequest {
                QueueUrl = _config.Value.AwsSqsQueueUrl,
                ReceiptHandle = message.ReceiptHandle
            };
            await _sqsClient.DeleteMessageAsync(deleteMessageRequest);
        }
    }
}
