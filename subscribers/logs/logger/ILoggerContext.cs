using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace Dta.Marketplace.Subscribers.Logger.Worker {
    public interface ILoggerContext {

        DbSet<LogEntry> LogEntry { get; set; }
        int SaveChanges();
    }
}
