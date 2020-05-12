using System;

namespace Dta.Marketplace.Api.Business.Exceptions {
    public class SessionExpiredException: Exception {
        public SessionExpiredException() : base("Session has expired") { }
    }
}
