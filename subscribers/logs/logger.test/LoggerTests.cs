using System;
using Xunit;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json;
using Dta.Marketplace.Subscribers.Logger.Worker;
using Amazon.SQS.Model;

using Microsoft.EntityFrameworkCore;

namespace Dta.Marketplace.Subscribers.Logger.Worker.Logger.Tests{

    public class LoggerTests
    {

         [Fact]
    public void Can_log_information_message() {
        // Arrange
        var logger = new Mock<ILoggerAdapter<AppService>>();
        var context = new Mock<ILoggerContext>();    
        var dbSet = new Mock<DbSet<LogEntry>>();  
        context.SetupGet(c => c.LogEntry).Returns(dbSet.Object);
        var json = JsonConvert.SerializeObject(new {
            foo = "bar"
        });

        // Act
        var messageProcessor = new MessageProcessor(logger.Object, context.Object);
        messageProcessor.Process(new Message {
             Body=json
        });

        // Assert
        logger.Verify(l => l.LogInformation(json), Times.Once);
        context.Verify(l => l.SaveChanges(), Times.Once);
        
      }
    

    [Fact]
    //this naming convention is allowed for unit testing Needs to be changed 
    public void Can_log_exception() {
        // Arrange
        var logger = new Mock<ILoggerAdapter<AppService>>();
        var context = new Mock<ILoggerContext>();    
        var dbSet = new Mock<DbSet<LogEntry>>();  
        context.SetupGet(c => c.LogEntry).Returns(dbSet.Object);
       // context.Setup(c => c.LogEntry).Returns(dbSet.Object);
        var nullJson = JsonConvert.SerializeObject(new {
            foo = ""
        });   
        
        //Act LogError
        var messageProcessor = new MessageProcessor(logger.Object, context.Object);
        messageProcessor.Process(null);

        //Assert LogError  
        logger.Verify( l => l.LogError(It.IsAny<string>(), It.IsAny<Exception>()), Times.Once);

    }
    }
}






