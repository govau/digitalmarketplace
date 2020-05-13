using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Options;
using Dta.Marketplace.Api.Shared;

namespace Dta.Marketplace.Api.Business.Utils {
    public class EncryptionUtil : IEncryptionUtil {
        private readonly IOptions<AppSettings> _appSettings;
        public EncryptionUtil(IOptions<AppSettings> appSettings) {
            _appSettings = appSettings;
        }
        public string Encrypt(string value) {
            var salt = System.Text.Encoding.Unicode.GetBytes(_appSettings.Value.Salt);
            string encrypted = Convert.ToBase64String(
                KeyDerivation.Pbkdf2(
                    password: value,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8
                )
            );
            return encrypted;
        }
    }
}
