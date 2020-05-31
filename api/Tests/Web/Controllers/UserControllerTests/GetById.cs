using Dta.Marketplace.Api.Business;
using Dta.Marketplace.Api.Business.Models;
using Dta.Marketplace.Api.Web.Controllers;
using Dta.Marketplace.Api.Web.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Moq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Dta.Marketplace.Api.Tests.Controllers.UsersControllerTests {
    public class GetById {
        private Mock<IUserBusiness> _userBusinessMock;
        public GetById() {
            _userBusinessMock = new Mock<IUserBusiness>();
            _userBusinessMock
                .Setup(m => m.GetByIdAsync(1))
                .ReturnsAsync(new UserModel {
                    Id = 1
                });
        }

        [Theory]
        [InlineData(true, false)]
        [InlineData(false, true)]
        [InlineData(true, true)]
        public async Task Can_Get_User(bool userInAdminRole, bool userTheSame) {
            var authorizationUtilMock = new Mock<IAuthorizationUtil>();
            authorizationUtilMock
                .Setup(a => a.IsUserInRole(It.IsAny<ClaimsPrincipal>(), It.IsAny<string>()))
                .Returns(userInAdminRole);

            authorizationUtilMock
                .Setup(a => a.IsUserTheSame(It.IsAny<ClaimsPrincipal>(), It.IsAny<int>()))
                .Returns(userTheSame);

            var usersController = new UserController(_userBusinessMock.Object, authorizationUtilMock.Object);

            var result = await usersController.GetByIdAsync(1) as ObjectResult;
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);

            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;

            Assert.IsType<UserModel>(okResult.Value);
            var userModel = okResult.Value as UserModel;

            Assert.Equal(1, userModel.Id);
        }
        
        [Theory]
        [InlineData(false, false)]
        public async Task Cannot_Get_User(bool userInAdminRole, bool userTheSame) {
            var authorizationUtilMock = new Mock<IAuthorizationUtil>();
            authorizationUtilMock
                .Setup(a => a.IsUserInRole(It.IsAny<ClaimsPrincipal>(), It.IsAny<string>()))
                .Returns(userInAdminRole);

            authorizationUtilMock
                .Setup(a => a.IsUserTheSame(It.IsAny<ClaimsPrincipal>(), It.IsAny<int>()))
                .Returns(userTheSame);

            var usersController = new UserController(_userBusinessMock.Object, authorizationUtilMock.Object);

            var result = await usersController.GetByIdAsync(1) as ForbidResult;
            Assert.NotNull(result);
            Assert.IsType<ForbidResult>(result);
        }
    }
}
