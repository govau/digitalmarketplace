# Slack Notifier

This small application is used for polling an AWS SQS queue to send slack notifications.

## Environment Variables

```
export SUPPLIER_SLACK_URL=[https://api.slack.com/incoming-webhooks]
export BUYER_SLACK_URL=[https://api.slack.com/incoming-webhooks]
export USER_SLACK_URL=[https://api.slack.com/incoming-webhooks]
export AWS_SQS_QUEUE_URL=[http://localhost:4576/queue/dta-marketplace-local-slack]
export AWS_SQS_SERVICE_URL=[http://localhost:4576]
export AWS_SQS_LONG_POLL_TIME_IN_SECONDS=[20]
export WORK_INTERVAL_IN_SECONDS=[60]
export SENTRY_DSN=[https://sentry]
```

```AWS_SQS_SERVICE_URL``` is only required for development using localstack.
```WORK_INTERVAL_IN_SECONDS``` and ```AWS_SQS_LONG_POLL_TIME_IN_SECONDS``` are optional.

## Requirements:
[.NET Core SDK](https://dotnet.microsoft.com/download/dotnet-core)  
In VSCode [OmniSharp](http://www.omnisharp.net)

## Running:
```dotnet run```
