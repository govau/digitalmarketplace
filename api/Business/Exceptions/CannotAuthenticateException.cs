using System;

namespace Dta.Marketplace.Api.Business.Exceptions {
    public class CannotAuthenticateException : Exception {
        public CannotAuthenticateException() : base("Unable to login using the supplied details") { }
    }
}
