using Dta.Marketplace.Api.Business;
using Dta.Marketplace.Api.Business.Models;
using Dta.Marketplace.Api.Web.Controllers;
using Dta.Marketplace.Api.Web.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Dta.Marketplace.Api.Tests.Controllers.UsersControllerTests {
    [TestClass]
    public class GetById {
        [TestMethod]
        public async Task Test_It_Is_Possible_To_Get_User_As_Admin() {
            var userBusinessMock = new Mock<IUserBusiness>();
            userBusinessMock
                .Setup(m => m.GetByIdAsync(1))
                .ReturnsAsync(new UserModel {
                    Id = 1
                });

            var authorizationUtilMock = new Mock<IAuthorizationUtil>();
            authorizationUtilMock
                .Setup(a => a.IsUserInRole(It.IsAny<ClaimsPrincipal>(), It.IsAny<string>()))
                .Returns(true);

            authorizationUtilMock
                .Setup(a => a.IsUserTheSame(It.IsAny<ClaimsPrincipal>(), It.IsAny<int>()))
                .Returns(false);

            var usersController = new UsersController(userBusinessMock.Object, authorizationUtilMock.Object);

            var result = await usersController.GetByIdAsync(1) as ObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;

            Assert.IsInstanceOfType(okResult.Value, typeof(UserModel));
            var userModel = okResult.Value as UserModel;

            Assert.AreEqual(userModel.Id, 1);
        }

        [TestMethod]
        public async Task Test_It_Is_Possible_To_Get_User_As_Self() {
            var userBusinessMock = new Mock<IUserBusiness>();
            userBusinessMock
                .Setup(m => m.GetByIdAsync(1))
                .ReturnsAsync(new UserModel {
                    Id = 1
                });

            var authorizationUtilMock = new Mock<IAuthorizationUtil>();
            authorizationUtilMock
                .Setup(a => a.IsUserInRole(It.IsAny<ClaimsPrincipal>(), It.IsAny<string>()))
                .Returns(false);

            authorizationUtilMock
                .Setup(a => a.IsUserTheSame(It.IsAny<ClaimsPrincipal>(), It.IsAny<int>()))
                .Returns(true);

            var usersController = new UsersController(userBusinessMock.Object, authorizationUtilMock.Object);

            var result = await usersController.GetByIdAsync(1) as ObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;

            Assert.IsInstanceOfType(okResult.Value, typeof(UserModel));
            var userModel = okResult.Value as UserModel;

            Assert.AreEqual(userModel.Id, 1);
        }
        
        [TestMethod]
        public async Task Test_It_Is_Not_Possible_To_Get_User() {
            var userBusinessMock = new Mock<IUserBusiness>();
            userBusinessMock
                .Setup(m => m.GetByIdAsync(1))
                .ReturnsAsync(new UserModel {
                    Id = 1
                });

            var authorizationUtilMock = new Mock<IAuthorizationUtil>();
            authorizationUtilMock
                .Setup(a => a.IsUserInRole(It.IsAny<ClaimsPrincipal>(), It.IsAny<string>()))
                .Returns(false);

            authorizationUtilMock
                .Setup(a => a.IsUserTheSame(It.IsAny<ClaimsPrincipal>(), It.IsAny<int>()))
                .Returns(false);

            var usersController = new UsersController(userBusinessMock.Object, authorizationUtilMock.Object);

            var result = await usersController.GetByIdAsync(1) as ForbidResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ForbidResult));
        }
    }
}
