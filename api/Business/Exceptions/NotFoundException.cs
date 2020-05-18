using System;

namespace Dta.Marketplace.Api.Business.Exceptions {
    public class NotFoundException: Exception {
        public NotFoundException() : base("Cannot find") { }
    }
}
