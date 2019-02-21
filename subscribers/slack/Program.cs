using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Dta.Marketplace.Subscriber.Slack.Processors;
using Dta.Marketplace.Subscriber.Slack.Services;

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
