# Email Logger Notifier

This small application is used for polling an AWS SQS queue to log emails and the resulting SNS notifications sent by the Digital Marketplace.

## Environment Variables

```
export AWS_SQS_QUEUE_URL=[http://localhost:4576/queue/dta-marketplace-local-email-logger]
export AWS_SQS_SERVICE_URL=[http://localhost:4576]
export AWS_SQS_LONG_POLL_TIME_IN_SECONDS=[20]
export WORK_INTERVAL_IN_SECONDS=[60]
export SENTRY_DSN=[https://sentry]
```

```AWS_SQS_SERVICE_URL``` is only required for development using localstack.
```WORK_INTERVAL_IN_SECONDS``` and ```AWS_SQS_LONG_POLL_TIME_IN_SECONDS``` are optional.

## Requirements:
[.NET Core 2.2 SDK](https://dotnet.microsoft.com/download/dotnet-core/2.2)  
In VSCode [OmniSharp](http://www.omnisharp.net)

## Running:
```dotnet run```
