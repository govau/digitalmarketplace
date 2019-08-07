using System;
using Microsoft.Extensions.Logging;
using Amazon.SQS.Model;
using Microsoft.EntityFrameworkCore;
namespace Dta.Marketplace.Subscribers.Logger.Worker{
public class LoggerAdapter<T>: ILoggerAdapter<T>{
 private readonly ILogger<T> _logger;

 public LoggerAdapter(ILogger<T> logger){
     _logger= logger;
 }

    public void LogInformation(string message)
    {
        _logger.LogInformation(message);
    }

    public void LogError(string message,Exception ex)
    {
        _logger.LogError(message, ex);
    }
}
}