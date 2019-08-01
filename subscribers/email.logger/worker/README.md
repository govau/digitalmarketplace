# Email Logger Notifier

This application is used to poll two AWS SQS queues. These queues contain information regarding emails sent from the Digital Marketplace. Two concurrent singletons run, one grabs the notification from the SES publishing event, the other grabs the body of the email sent from the API. The application saves these notifications to the database, and combines them when matching message_IDs are found.

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
