## Email Logger Notifier

This small application is used for the polling of two SQS queues. 
One contains the body of the email when sent from the API, the other contains SES event publishing notifications.
These are added to the database and merged together.

## Environment Variables

```
export AWS_SQS_REGION=us-west-2
export AWS_SQS_QUEUE_URL=https://us-west-2.queue.amazonaws.com/048212923711/dta-marketplace-production-email-logger
export AWS_SQS_BODY_REGION=ap-southeast-2
export AWS_SQS_BODY_QUEUE_URL=https://ap-southeast-2.queue.amazonaws.com/048212923711/dta-marketplace-production-email-body-logger
export AWS_SQS_LONG_POLL_TIME_IN_SECONDS=[20]
export CONNECTION_STRING=[Host=localhost;Port=5432;Database=logger;Username=postgres;Password=password]
export WORK_INTERVAL_IN_SECONDS=[60]
export SENTRY_DSN=[https://sentry]
```

```AWS_SQS_REGION``` and ```AWS_SQS_QUEUE_URL``` are for the SES event notifications.
```AWS_SQS_BODY_REGION``` and ```AWS_SQS_BODY_QUEUE_URL``` are for the SNS events containing the email body.
```WORK_INTERVAL_IN_SECONDS``` and ```AWS_SQS_LONG_POLL_TIME_IN_SECONDS``` are optional.
```AWS_SQS_SERVICE_URL``` is only required for development using localstack.


```
export AWS_SQS_SERVICE_URL=[http://localhost:4576]
export AWS_BODY_SQS_SERVICE_URL=[http://localhost:4576]
```

## Requirements:
[.NET Core 2.2 SDK](https://dotnet.microsoft.com/download/dotnet-core/2.2)  
In VSCode [OmniSharp](http://www.omnisharp.net)

## Running:
```dotnet run```
