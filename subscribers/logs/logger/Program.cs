using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Serilog;
using Serilog.Sinks.PostgreSQL;
using Serilog.Configuration;
using Serilog.Core; 
using Serilog.Events;
using System.Collections.Generic;
using Npgsql;

namespace Dta.Marketplace.Subscribers.Logger.Worker {
    class Program {
        static async Task Main(string[] args) {
            Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Debug()
                        .WriteTo.Console()
                        .WriteTo.Sentry(o => {
                            o.MinimumEventLevel = Serilog.Events.LogEventLevel.Information;
                        })
                        .CreateLogger();

            var hostBuilder = new HostBuilder()
                .ConfigureAppConfiguration((hostContext, app) => {
                    app.AddEnvironmentVariables();
                    if (args != null) {
                        app.AddCommandLine(args);
                    }
                })
                .ConfigureLogging((hostContext, logging) => { 
                    logging.AddConfiguration(hostContext.Configuration.GetSection("Logging"));
                    logging.AddConsole();
                })
                .UseSerilog(Log.Logger)
                .ConfigureServices((hostContext, services) => {
                    services.AddOptions();
                    services.AddSingleton<IHostedService, AppService>();
                    services.AddSingleton(typeof(ILoggerAdapter<>),typeof(loggerAdapter<>));
                    services.AddTransient<IMessageProcessor, MessageProcessor>();
                    services.AddTransient<ILoggerContext, LoggerContext>();
                    services.Configure<AppConfig>(ac => {
                        ac.AwsSqsAccessKeyId = Environment.GetEnvironmentVariable("AWS_SQS_ACCESS_KEY_ID");
                        ac.AwsSqsQueueUrl = Environment.GetEnvironmentVariable("AWS_SQS_QUEUE_URL");
                        ac.AwsSqsServiceUrl = Environment.GetEnvironmentVariable("AWS_SQS_SERVICE_URL");
                        var awsSqsRegion = Environment.GetEnvironmentVariable("AWS_SQS_REGION");
                        if (string.IsNullOrWhiteSpace(awsSqsRegion) == false) {
                            ac.AwsSqsRegion = awsSqsRegion;
                        }
                        ac.AwsSqsSecretAccessKey = Environment.GetEnvironmentVariable("AWS_SQS_SECRET_ACCESS_KEY");
                        ac.BuyerSlackUrl = Environment.GetEnvironmentVariable("BUYER_SLACK_URL");
                        ac.SupplierSlackUrl = Environment.GetEnvironmentVariable("SUPPLIER_SLACK_URL");// dont need this
                        ac.UserSlackUrl = Environment.GetEnvironmentVariable("USER_SLACK_URL");// dont need this 
                        var workIntervalInSeconds = Environment.GetEnvironmentVariable("WORK_INTERVAL_IN_SECONDS");
                        if (string.IsNullOrWhiteSpace(workIntervalInSeconds) == false) {
                            ac.WorkIntervalInSeconds = int.Parse(workIntervalInSeconds);
                        }
                        var awsSqsLongPollTimeInSeconds = Environment.GetEnvironmentVariable("AWS_SQS_LONG_POLL_TIME_IN_SECONDS");
                        if (string.IsNullOrWhiteSpace(awsSqsLongPollTimeInSeconds) == false) {
                            ac.AwsSqsLongPollTimeInSeconds = int.Parse(awsSqsLongPollTimeInSeconds);
                        }
                        ac.SentryDsn = Environment.GetEnvironmentVariable("SENTRY_DSN");
                    });
                });
            await hostBuilder.RunConsoleAsync();
            Log.CloseAndFlush();
        }
    }
}

