using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;

namespace Dta.Marketplace.Subscribers.Logger.Worker {
    public class Program {
        public static async Task Main(string[] args) {
            Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Information()
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
                    services.AddTransient<IMessageProcessor, MessageProcessor>();
                    services.Configure<AppConfig>(ac => {
                        ac.AwsSqsAccessKeyId = Environment.GetEnvironmentVariable("AWS_SQS_ACCESS_KEY_ID");
                        ac.AwsSqsQueueUrl = Environment.GetEnvironmentVariable("AWS_SQS_QUEUE_URL");
                        var serviceUrl = Environment.GetEnvironmentVariable("AWS_SQS_SERVICE_URL");
                        if (string.IsNullOrWhiteSpace(serviceUrl) == false) {
                            ac.AwsSqsServiceUrl = serviceUrl;
                        }
                        var awsSqsRegion = Environment.GetEnvironmentVariable("AWS_SQS_REGION");
                        if (string.IsNullOrWhiteSpace(awsSqsRegion) == false) {
                            ac.AwsSqsRegion = awsSqsRegion;
                        }
                        ac.AwsSqsSecretAccessKey = Environment.GetEnvironmentVariable("AWS_SQS_SECRET_ACCESS_KEY");
                        var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
                        if (string.IsNullOrWhiteSpace(connectionString) == false) {
                            ac.ConnectionString = connectionString;
                        }

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
                            if (credentials.WorkIntervalInSeconds != 0) {
                                ac.WorkIntervalInSeconds = credentials.WorkIntervalInSeconds;
                            }
                            if (credentials.AwsSqsLongPollTimeInSeconds != 0) {
                                ac.AwsSqsLongPollTimeInSeconds = credentials.AwsSqsLongPollTimeInSeconds;
                            }
                            ac.SentryDsn = credentials.SentryDsn;
                            Sentry.SentrySdk.Init(ac.SentryDsn);

                            var postgresCredentials = vcapServices.Postgres.First().Credentials;
                            ac.ConnectionString = $"Host={postgresCredentials.Host};Port={postgresCredentials.Port};Database={postgresCredentials.DbName};Username={postgresCredentials.Username};Password={postgresCredentials.Password}";
                        }

                        if (string.IsNullOrWhiteSpace(ac.AwsSqsAccessKeyId) == false) {
                            ac.AwsSqsServiceUrl = null;
                        }
                    });
                    var serviceProvider = services.BuildServiceProvider();
                    var appConfigOptions = serviceProvider.GetService<IOptions<AppConfig>>();
                    var appConfig = appConfigOptions.Value;

                    services.AddEntityFrameworkNpgsql()
                            .AddDbContext<LoggerContext>(options => {
                                options.UseNpgsql(appConfig.ConnectionString);
                            })
                            .BuildServiceProvider();
                });
            await hostBuilder.RunConsoleAsync();
            Log.CloseAndFlush();
        }
    }
}
