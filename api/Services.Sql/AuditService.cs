using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text.Json;
using Dapper;
using Dta.Marketplace.Api.Services;
using Dta.Marketplace.Api.Services.Entities;

namespace Dta.Marketplace.Api.Services.Sql {
    public class AuditService : IAuditService {
        private readonly DigitalMarketplaceContext _context;
        public AuditService(DigitalMarketplaceContext context) {
            _context = context;
        }

        public async Task LogAuditEventAsync(string auditType, string user, string data, string dbObject, int objectId) {
            var auditEvent = new AuditEvent();
            auditEvent.Type = auditType;
            auditEvent.User = user;
            auditEvent.Data = data;
            auditEvent.ObjectType = dbObject;
            auditEvent.ObjectId = objectId;
            _context.Add(auditEvent);
            await _context.SaveChangesAsync();

        }
    }
}
