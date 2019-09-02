using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using Dta.Marketplace.Subscribers.Email.Logger.Worker.Model;
using Dta.Marketplace.Subscribers.Email.Logger.Worker.Processors;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Dta.Marketplace.Subscribers.Email.Logger.Worker {
    public class AppService : IHostedService, IDisposable {
        private readonly ILogger _logger;
        private readonly IOptions<AppConfig> _config;
        private AmazonSQSClient _sqsClient;
        private readonly Func<string, IEmailLogProcessor> _emailLogProcessor;
        private Timer _timer;

        public AppService(ILogger<AppService> logger, IOptions<AppConfig> config, Func<string, IEmailLogProcessor> emailLogProcessor) {
            _logger = logger;
            _config = config;
            _emailLogProcessor = emailLogProcessor;
        }

        public Task StartAsync(CancellationToken cancellationToken) {
            _logger.LogInformation("Starting daemon: Email Logging. {Timer}", new {
                _config.Value.AwsSqsLongPollTimeInSeconds,
                _config.Value.WorkIntervalInSeconds,
                sentryEnabled = string.IsNullOrWhiteSpace(_config.Value.SentryDsn) ? false : true
            });

            var sqsConfig = new AmazonSQSConfig {
                RegionEndpoint = RegionEndpoint.GetBySystemName(_config.Value.AwsSqsRegion)
            };
            sqsConfig.RegionEndpoint = RegionEndpoint.USWest2;
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
                AttributeNames = new List<string>() { "All" },
                QueueUrl = _config.Value.AwsSqsQueueUrl,
                WaitTimeSeconds = _config.Value.AwsSqsLongPollTimeInSeconds,
                MaxNumberOfMessages = 10
            };
            _logger.LogDebug("Heartbeat: {Now}", DateTime.Now);
            var receiveMessageResponse = await _sqsClient.ReceiveMessageAsync(receiveMessageRequest);

            if (receiveMessageResponse.Messages.Count > 0) {
                foreach (var message in receiveMessageResponse.Messages) {
                    _logger.LogDebug("Message Id: {MessageId}", message.MessageId);
                    var notificationLogBodyAnon2 = JsonConvert.DeserializeAnonymousType(message.Body, new {
                        Message = "",
                    });
                    var notificationLogBodyMessageAnon2 = JsonConvert.DeserializeAnonymousType(notificationLogBodyAnon2.Message, new {
                        notificationType = "",
                    });
                    var awsSqsMessage = AwsSqsMessage.FromJson(message.Body);
                    awsSqsMessage.Body = message.Body;
                    var emailProcessor = _emailLogProcessor(notificationLogBodyMessageAnon2.notificationType);

                    if (emailProcessor == null) {
                        _logger.LogDebug("Email processor not found for {@AwsSqsMessage}. Deleting message", awsSqsMessage);
                        await DeleteMessage(message);
                        continue;
                    }
                    var result = emailProcessor.ProcessMessage(awsSqsMessage);
                    if (result == true) {
                        await DeleteMessage(message);
                    }
                }
            }
        }

        private async Task DeleteMessage(Amazon.SQS.Model.Message message) {
            var deleteMessageRequest = new DeleteMessageRequest {
                QueueUrl = _config.Value.AwsSqsQueueUrl,
                ReceiptHandle = message.ReceiptHandle
            };
            await _sqsClient.DeleteMessageAsync(deleteMessageRequest);
        }
    }
}

