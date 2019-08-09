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

namespace Dta.Marketplace.Subscribers.Email.Logger.Worker {
    public class AppBodyService : IHostedService, IDisposable {
        private readonly ILogger _bodyLogger;
        private readonly IOptions<AppConfig> _config;
        private readonly IEmailLogProcessor _emailLogProcessor;
        private AmazonSQSClient _sqsBodyClient;
        private Timer _bodyTimer;

        public AppBodyService(ILogger<AppService> logger, IOptions<AppConfig> config, IEmailLogProcessor emailLogProcessor) {
            _bodyLogger = logger;
            _config = config;
            _emailLogProcessor = emailLogProcessor;
        }

        public Task StartAsync(CancellationToken cancellationToken) {
            _bodyLogger.LogInformation("Starting daemon: Email Logging. {Timer}", new {
                _config.Value.AwsSqsLongPollTimeInSeconds,
                _config.Value.WorkIntervalInSeconds,
                sentryEnabled = string.IsNullOrWhiteSpace(_config.Value.SentryDsn) ? false : true
            });

            var sqsBodyConfig = new AmazonSQSConfig {
                RegionEndpoint = RegionEndpoint.GetBySystemName(_config.Value.AwsSqsBodyRegion)
            };
            if (string.IsNullOrWhiteSpace(_config.Value.AwsSqsBodyServiceUrl) == false) {
                sqsBodyConfig.ServiceURL = _config.Value.AwsSqsBodyServiceUrl;
            }
            if (string.IsNullOrWhiteSpace(_config.Value.AwsSqsSecretAccessKey) == false &&
                string.IsNullOrWhiteSpace(_config.Value.AwsSqsAccessKeyId) == false) {
                _sqsBodyClient = new AmazonSQSClient(_config.Value.AwsSqsAccessKeyId, _config.Value.AwsSqsSecretAccessKey, sqsBodyConfig);
            } else {
                _sqsBodyClient = new AmazonSQSClient(sqsBodyConfig);
            }

            _bodyTimer = new Timer(DoBodyWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(_config.Value.WorkIntervalInSeconds));
            return Task.CompletedTask;
        }
        public Task StopAsync(CancellationToken cancellationToken) {
            _bodyLogger.LogInformation("Stopping daemon.");
            _bodyTimer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
        public void Dispose() {
            _bodyLogger.LogInformation("Disposing....");
            _bodyTimer?.Dispose();
            _sqsBodyClient?.Dispose();
        }

        private async void DoBodyWork(object state) {
            var receiveBodyMessageRequest = new ReceiveMessageRequest() {
                AttributeNames = new List<string>() { "All" },
                QueueUrl = _config.Value.AwsSqsBodyQueueUrl,
                WaitTimeSeconds = _config.Value.AwsSqsLongPollTimeInSeconds,
                MaxNumberOfMessages = 10
            };
            _bodyLogger.LogDebug("Heartbeat: {Now}", DateTime.Now);
            var receiveBodyMessageResponse = await _sqsBodyClient.ReceiveMessageAsync(receiveBodyMessageRequest);

            if (receiveBodyMessageResponse.Messages.Count > 0) {

                foreach (var message in receiveBodyMessageResponse.Messages) {

                    _bodyLogger.LogDebug("Message Id: {MessageId}", message.MessageId);
                    var awsSqsMessage = AwsSqsMessage.FromJson(message.Body);
                    awsSqsMessage.Body = message.Body;

                    if (message.Body.Contains("Email_Body_Log")) {
                        var emailLogProcessor = _emailLogProcessor;

                        if (emailLogProcessor == null) {
                            _bodyLogger.LogDebug("Email processor not found for {@AwsSqsMessage}. Deleting message", awsSqsMessage);
                            await DeleteBodyMessage(message);
                            continue;
                        }
                        var result = emailLogProcessor.ProcessMessage(awsSqsMessage);
                        if (result == true) {
                            await DeleteBodyMessage(message);
                        }
                    } else {
                        _bodyLogger.LogDebug("Not an email body message for {@AwsSqsMessage}. Deleting message", awsSqsMessage);
                        await DeleteBodyMessage(message);
                        continue;
                    }
                }
            }
        }

        private async Task DeleteBodyMessage(Amazon.SQS.Model.Message message) {
            var deleteMessageRequest = new DeleteMessageRequest {
                QueueUrl = _config.Value.AwsSqsBodyQueueUrl,
                ReceiptHandle = message.ReceiptHandle
            };
            await _sqsBodyClient.DeleteMessageAsync(deleteMessageRequest);
        }
    }
}

