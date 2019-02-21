using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Dta.Marketplace.Subscriber.Slack.Processors;
using Dta.Marketplace.Subscriber.Slack.Services;
using Dta.Marketplace.Subscriber.Slack.Model;

namespace Dta.Marketplace.Subscriber.Slack {
    class Program {
        public static async Task Main(string[] args) {
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
                        var vcapServicesString = Environment.GetEnvironmentVariable("VCAP_SERVICES");
                        if (vcapServicesString != null) {
                            var vcapServices = VcapServices.FromJson(vcapServicesString);
                            var credentials = vcapServices.UserProvided.First().Credentials;

                            ac.AWS_SQS_ACCESS_KEY_ID = credentials.AwsSqsAccessKeyId;
                            ac.AWS_SQS_QUEUE_URL = credentials.AwsSqsQueueUrl;
                            ac.AWS_SQS_REGION = credentials.AwsSqsRegion;
                            ac.AWS_SQS_SECRET_ACCESS_KEY = credentials.AwsSqsSecretAccessKey;
                            ac.BUYER_SLACK_URL = credentials.BuyerSlackUrl;
                            ac.SUPPLIER_SLACK_URL = credentials.SupplierSlackUrl;
                            ac.USER_SLACK_URL = credentials.UserSlackUrl;
                            ac.WORK_INTERVAL_IN_SECONDS = credentials.WorkIntervalInSeconds;
                            ac.AWS_SQS_LONG_POLL_TIME_IN_SECONDS = credentials.AwsSqsLongPollTimeInSeconds;
                        }
                    });


                    services.AddSingleton<IHostedService, AppService>();

                    services.AddTransient<ApplicationMessageProcessor>();
                    services.AddTransient<BriefMessageProcessor>();
                    services.AddTransient<UserMessageProcessor>();
                    services.AddTransient<ISlackService, SlackService>();

                    services.AddTransient<Func<string, IMessageProcessor>>(sp => key => {
                        switch (key) {
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
                });

            await builder.RunConsoleAsync();
        }
    }
}
