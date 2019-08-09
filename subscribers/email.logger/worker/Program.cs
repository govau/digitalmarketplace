using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Npgsql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Dta.Marketplace.Subscribers.Email.Logger.Worker.Processors;
using Dta.Marketplace.Subscribers.Email.Logger.Worker.Services;
using Dta.Marketplace.Subscribers.Email.Logger.Worker.Model;

[assembly: InternalsVisibleToAttribute("email.logger.worker.tests")]
namespace Dta.Marketplace.Subscribers.Email.Logger.Worker {
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
                        ac.AwsSqsSecretAccessKey = Environment.GetEnvironmentVariable("AWS_SQS_SECRET_ACCESS_KEY");
                        ac.AwsSqsQueueUrl = Environment.GetEnvironmentVariable("AWS_SQS_QUEUE_URL");
                        ac.AwsSqsBodyServiceUrl = Environment.GetEnvironmentVariable("AWS_SQS_BODY_SERVICE_URL");
                        ac.AwsSqsServiceUrl = Environment.GetEnvironmentVariable("AWS_SQS_SERVICE_URL");
                        ac.AwsSqsBodyQueueUrl = Environment.GetEnvironmentVariable("AWS_SQS_BODY_QUEUE_URL");
                        var awsSqsRegion = Environment.GetEnvironmentVariable("AWS_SQS_REGION");
                        if (string.IsNullOrWhiteSpace(awsSqsRegion) == false) {
                            ac.AwsSqsRegion = awsSqsRegion;
                        }
                        var awsSqsBodyRegion = Environment.GetEnvironmentVariable("AWS_SQS_BODY_REGION");
                        if (string.IsNullOrWhiteSpace(awsSqsRegion) == false) {
                            ac.AwsSqsBodyRegion = awsSqsBodyRegion;
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
                        ac.ConnectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
                        var vcapServicesString = Environment.GetEnvironmentVariable("VCAP_SERVICES");
                        if (vcapServicesString != null) {
                            var vcapServices = VcapServices.FromJson(vcapServicesString);
                            var credentials = vcapServices.UserProvided.First().Credentials;

                            ac.AwsSqsAccessKeyId = credentials.AwsSqsAccessKeyId;
                            ac.AwsSqsQueueUrl = credentials.AwsSqsQueueUrl;
                            ac.AwsSqsBodyQueueUrl = credentials.AwsSqsBodyQueueUrl;
                            if (string.IsNullOrWhiteSpace(credentials.AwsSqsRegion) == false) {
                                ac.AwsSqsRegion = credentials.AwsSqsRegion;
                            }
                            if (string.IsNullOrWhiteSpace(credentials.AwsSqsBodyRegion) == false) {
                                ac.AwsSqsBodyRegion = credentials.AwsSqsBodyRegion;
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
                            };
                    });
                   
                    services.AddSingleton<IHostedService, AppService>();
                    services.AddSingleton<IHostedService, AppBodyService>();
                    
                    services.AddTransient<EmailDeliveryNotificationProcessor>();
                    services.AddTransient<EmailComplaintNotificationProcessor>();
                    services.AddTransient<EmailBounceNotificationProcessor>();
                    services.AddTransient<IEmailBodyService, SaveEmailBodyService>();
                    services.AddTransient<IEmailService, SaveEmailNotificationService>();
                    services.AddTransient<IEmailLogProcessor, EmailBodyLogProcessor>();
                    services.AddTransient<Func<string, IEmailLogProcessor>>(sp => key => {
                        switch (key) {
                            case "Delivery":
                                return sp.GetService<EmailDeliveryNotificationProcessor>();
                            case "Complaint":
                                return sp.GetService<EmailComplaintNotificationProcessor>();
                            case "Bounce":
                                return sp.GetService<EmailBounceNotificationProcessor>();
                            default:
                                return null;
                        }
                    });
                    var serviceProvider = services.BuildServiceProvider();
                    var appConfigOptions = serviceProvider.GetService<IOptions<AppConfig>>();
                    var appConfig = appConfigOptions.Value;
                    services.AddEntityFrameworkNpgsql()
                            .AddDbContext<EmailLoggerContext>(options => {
                                options.UseNpgsql(appConfig.ConnectionString);
                            })
                            .BuildServiceProvider();
                });

            await builder.RunConsoleAsync();
        }
    }
}
