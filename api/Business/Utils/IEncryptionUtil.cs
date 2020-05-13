namespace Dta.Marketplace.Api.Business.Utils {
    public interface IEncryptionUtil {
        string Encrypt(string value);
        string Encrypt(string value, bool randomSalt);
    }
}
