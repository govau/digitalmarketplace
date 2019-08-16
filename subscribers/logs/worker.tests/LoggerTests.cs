using System;
using Amazon.SQS.Model;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace Dta.Marketplace.Subscribers.Logger.Worker.Tests {
    public class LoggerTests {
        [Fact]
        public async void Can_Log_Information_Message() {
            // Arrange
            var dbSet = new Mock<DbSet<LogEntry>>();
            using (var connection = new SqliteConnection("DataSource=:memory:")) {
                connection.Open();
                // In-memory database only exists while the connection is open
                var options = new DbContextOptionsBuilder<LoggerContext>()
                            .UseSqlite(connection)
                            .Options;

                // Create schema in the database
                using (var context = new LoggerContext(options)) {
                    context.Database.EnsureCreated();
                }

                var json = JsonConvert.SerializeObject(new {
                    foo = "bar",
                });

                // Act
                using (var context = new LoggerContext(options)) {
                    var messageProcessor = new MessageProcessor(context);
                    messageProcessor.Process(new Message {
                        Body = json,
                    });
                }

                // Assert
                using (var context = new LoggerContext(options)) {
                    Assert.Equal(1, await context.LogEntry.CountAsync());
                }
            }
        }

        [Fact]
        public void Will_Throw_Exception() {
            // Arrange
            var messageProcessor = new MessageProcessor(null);

            // Act & Assert
            Assert.Throws<NullReferenceException>(() => messageProcessor.Process(null));
        }
    }
}
