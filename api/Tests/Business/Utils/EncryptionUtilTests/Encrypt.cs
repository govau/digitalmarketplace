using Dta.Marketplace.Api.Business.Utils;
using Dta.Marketplace.Api.Shared;
using Microsoft.Extensions.Options;
using Xunit;
using Moq;

namespace Dta.Marketplace.Api.Tests.Controllers.UsersControllerTests {
    public class Encrypt {
        public Encrypt() {
        }

        [Theory]
        [InlineData("foobar", "++FGjQkZ/G1ZrysNX9sanhVGWqy3MagQkbJhCKXyajI=")]
        [InlineData("barfoo", "XVFF0gdwrOZ9nFz3HufoiLWVXXdckbcesu/hsl2Bg6k=")]
        public void Can_Encrypt(string value, string expected) {
            var appSettingsMock = new Mock<IOptions<AppSettings>>();
            appSettingsMock.Setup(ac => ac.Value).Returns(new AppSettings {
                Salt = "secret"
            });
            var encryptionUtil = new EncryptionUtil(appSettingsMock.Object);
            var encrypted = encryptionUtil.Encrypt(value);
            Assert.Equal(expected, encrypted);
        }
    }
}
