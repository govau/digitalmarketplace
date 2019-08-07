using System;
using Xunit;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json;
using Microsoft.Data.Sqlite;
using Dta.Marketplace.Subscribers.Logger.Worker;
using Amazon.SQS.Model;

using Microsoft.EntityFrameworkCore;

namespace Dta.Marketplace.Subscribers.Logger.Worker.Logger.Tests {

    public class LoggerTests {

        [Fact]
        public void Can_Log_Information_Message() {

            // Arrange
            var logger = new Mock<ILoggerAdapter<AppService>>();
            var dbSet = new Mock<DbSet<LogEntry>>();
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            try {
                //In-memory database only exists while the connection is open
                var options = new DbContextOptionsBuilder<LoggerContext>()
                            .UseSqlite(connection)
                            .Options;

                //create schema in the database 
                using (var context = new LoggerContext(options)) {
                    context.Database.EnsureCreated();
                }

                var json = JsonConvert.SerializeObject(new {
                    foo = "bar"
                });

                // Act
                using (var context = new LoggerContext(options)) {
                    var messageProcessor = new MessageProcessor(logger.Object, context);
                    messageProcessor.Process(new Message {
                        Body = json
                    });
                }

                // Assert
                using (var context = new LoggerContext(options)) {
                    logger.Verify(l => l.LogInformation(json), Times.Once);
                }
            } finally {
                connection.Close();
            }
        }


        [Fact]
        public void Can_Log_Exception() {
            // Arrange
            var logger = new Mock<ILoggerAdapter<AppService>>();
            var dbSet = new Mock<DbSet<LogEntry>>();
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            try {
                var options = new DbContextOptionsBuilder<LoggerContext>()
                            .UseSqlite(connection)
                            .Options;

                using (var context = new LoggerContext(options)) {
                    context.Database.EnsureCreated();
                }
                var json = JsonConvert.SerializeObject(new {
                    foo = ""
                });

                //Act
                using (var context = new LoggerContext(options)) {
                    var messageProcessor = new MessageProcessor(logger.Object, context);
                    messageProcessor.Process(null);
                }
                //Assert
                using (var context = new LoggerContext(options)) {
                    logger.Verify(l => l.LogError(It.IsAny<string>(), It.IsAny<Exception>()), Times.Once);
                }
            } finally {
                connection.Close();
            }
        }
    }
}
