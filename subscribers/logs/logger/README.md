# Slack Notifier

This small application is used for polling an AWS SQS queue to send slack notifications.

## Environment Variables

```
export AWS_SQS_QUEUE_URL=[http://localhost:4576/queue/dta-marketplace-local-logger]
export AWS_SQS_SERVICE_URL=[http://localhost:4576]
export AWS_SQS_LONG_POLL_TIME_IN_SECONDS=[20]
export AWS_SQS_REGION=[ap-southeast-2]
export WORK_INTERVAL_IN_SECONDS=[60]
export SENTRY_DSN=[https://sentry]
export CONNCECTION_STRING=[Host=localhost;Port=5432;Database=logger;Username=postgres;Password=password]
```

```AWS_SQS_SERVICE_URL``` is only required for development using localstack.
```WORK_INTERVAL_IN_SECONDS``` and ```AWS_SQS_LONG_POLL_TIME_IN_SECONDS``` are optional.

## Requirements:
[.NET Core 2.2 SDK](https://dotnet.microsoft.com/download/dotnet-core/2.2)  
In VSCode [OmniSharp](http://www.omnisharp.net)

## Running:
```dotnet run```
