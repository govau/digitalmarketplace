using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog;
using Dta.Marketplace.Subscribers.Slack.Worker.Processors;
using Dta.Marketplace.Subscribers.Slack.Worker.Services;
using Dta.Marketplace.Subscribers.Slack.Worker.Model;

[assembly: InternalsVisibleToAttribute("slack.worker.test")]
namespace Dta.Marketplace.Subscribers.Slack.Worker {
    class Program {
        public static async Task Main(string[] args) {
            Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Information()
                        .WriteTo.Console()
                        .WriteTo.Sentry(o => {
                            o.MinimumEventLevel = Serilog.Events.LogEventLevel.Information;
                        })
                        .CreateLogger();

            var builder = new HostBuilder()
                .ConfigureAppConfiguration((hostingContext, config) => {
                    config.AddEnvironmentVariables();
                    if (args != null) {
                        config.AddCommandLine(args);
                    }
                })
                .ConfigureServices((hostContext, services) => {
                    services.AddOptions();
                    services.Configure<AppConfig>(hostContext.Configuration.GetSection("Daemon"));
                    services.Configure<AppConfig>(hostContext.Configuration);
                    services.Configure<AppConfig>(ac => {
                        ac.AwsSqsAccessKeyId = Environment.GetEnvironmentVariable("AWS_SQS_ACCESS_KEY_ID");
                        var awsSqsQueueUrl = Environment.GetEnvironmentVariable("AWS_SQS_QUEUE_URL");
                        if (string.IsNullOrWhiteSpace(awsSqsQueueUrl) == false) {
                            ac.AwsSqsQueueUrl = awsSqsQueueUrl;
                        }
                        var awsSqsServiceUrl = Environment.GetEnvironmentVariable("AWS_SQS_SERVICE_URL");
                        if (string.IsNullOrWhiteSpace(awsSqsServiceUrl) == false) {
                            ac.AwsSqsServiceUrl = awsSqsServiceUrl;
                        }
                        var awsSqsRegion = Environment.GetEnvironmentVariable("AWS_SQS_REGION");
                        if (string.IsNullOrWhiteSpace(awsSqsRegion) == false) {
                            ac.AwsSqsRegion = awsSqsRegion;
                        }
                        ac.AwsSqsSecretAccessKey = Environment.GetEnvironmentVariable("AWS_SQS_SECRET_ACCESS_KEY");
                        ac.AgencySlackUrl = Environment.GetEnvironmentVariable("AGENCY_SLACK_URL");
                        ac.BuyerSlackUrl = Environment.GetEnvironmentVariable("BUYER_SLACK_URL");
                        ac.SupplierSlackUrl = Environment.GetEnvironmentVariable("SUPPLIER_SLACK_URL");
                        ac.UserSlackUrl = Environment.GetEnvironmentVariable("USER_SLACK_URL");
                        var workIntervalInSeconds = Environment.GetEnvironmentVariable("WORK_INTERVAL_IN_SECONDS");
                        if (string.IsNullOrWhiteSpace(workIntervalInSeconds) == false) {
                            ac.WorkIntervalInSeconds = int.Parse(workIntervalInSeconds);
                        }
                        var awsSqsLongPollTimeInSeconds = Environment.GetEnvironmentVariable("AWS_SQS_LONG_POLL_TIME_IN_SECONDS");
                        if (string.IsNullOrWhiteSpace(awsSqsLongPollTimeInSeconds) == false) {
                            ac.AwsSqsLongPollTimeInSeconds = int.Parse(awsSqsLongPollTimeInSeconds);
                        }
                        ac.SentryDsn = Environment.GetEnvironmentVariable("SENTRY_DSN");

                        var vcapServicesString = Environment.GetEnvironmentVariable("VCAP_SERVICES");
                        if (vcapServicesString != null) {
                            var vcapServices = VcapServices.FromJson(vcapServicesString);
                            var credentials = vcapServices.UserProvided.First().Credentials;

                            ac.AwsSqsAccessKeyId = credentials.AwsSqsAccessKeyId;
                            ac.AwsSqsQueueUrl = credentials.AwsSqsQueueUrl;
                            if (string.IsNullOrWhiteSpace(credentials.AwsSqsRegion) == false) {
                                ac.AwsSqsRegion = credentials.AwsSqsRegion;
                            }
                            ac.AwsSqsSecretAccessKey = credentials.AwsSqsSecretAccessKey;
                            ac.AgencySlackUrl = credentials.AgencySlackUrl;
                            ac.BuyerSlackUrl = credentials.BuyerSlackUrl;
                            ac.SupplierSlackUrl = credentials.SupplierSlackUrl;
                            ac.UserSlackUrl = credentials.UserSlackUrl;
                            if (credentials.WorkIntervalInSeconds != 0) {
                                ac.WorkIntervalInSeconds = credentials.WorkIntervalInSeconds;
                            }
                            if (credentials.AwsSqsLongPollTimeInSeconds != 0) {
                                ac.AwsSqsLongPollTimeInSeconds = credentials.AwsSqsLongPollTimeInSeconds;
                            }
                            ac.SentryDsn = credentials.SentryDsn;
                            Sentry.SentrySdk.Init(ac.SentryDsn);
                        }

                        if (string.IsNullOrWhiteSpace(ac.AwsSqsAccessKeyId) == false) {
                            ac.AwsSqsServiceUrl = null;
                        }
                    });

                    services.AddSingleton<IHostedService, AppService>();

                    services.AddTransient<AgencyMessageProcessor>();
                    services.AddTransient<ApplicationMessageProcessor>();
                    services.AddTransient<BriefMessageProcessor>();
                    services.AddTransient<UserMessageProcessor>();
                    services.AddTransient<ISlackService, SlackService>();

                    services.AddTransient<Func<string, IMessageProcessor>>(sp => key => {
                        switch (key) {
                            case "agency":
                                return sp.GetService<AgencyMessageProcessor>();
                            case "application":
                                return sp.GetService<ApplicationMessageProcessor>();
                            case "brief":
                                return sp.GetService<BriefMessageProcessor>();
                            case "user":
                                return sp.GetService<UserMessageProcessor>();
                            default:
                                return null;
                        }
                    });
                })
                .ConfigureLogging((hostingContext, logging) => {
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddConsole();
                })
                .UseSerilog(Log.Logger);

            await builder.RunConsoleAsync();
        }
    }
}
