using System;
using Microsoft.Extensions.Logging;
using Amazon.SQS.Model;

namespace Dta.Marketplace.Subscribers.Logger.Worker{
public interface ILoggerAdapter<T> {
        void LogInformation(string message);

        void LogError (string message, Exception ex);

    }
}