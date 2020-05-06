using Dta.Marketplace.Api.Business;
using Dta.Marketplace.Api.Business.Models;
using Dta.Marketplace.Api.Web.Controllers;
using Dta.Marketplace.Api.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Security.Claims;

namespace Dta.Marketplace.Api.Tests.Controllers.UsersControllerTests {
    [TestClass]
    public class GetById {
        [TestMethod]
        public void Test_It_Is_Possible_To_Get_User_As_Admin() {
            var userBusinessMock = new Mock<IUserBusiness>();
            userBusinessMock.Setup(m => m.GetById(1)).Returns(new UserModel {
                Id = 1
            });

            var userMock = new Mock<ClaimsPrincipal>();
            userMock.Setup(p => p.IsInRole(Roles.Admin)).Returns(true);
            userMock.SetupGet(p => p.Identity.Name).Returns("1");

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(ctx => ctx.User).Returns(userMock.Object);

            var actionContext = new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor());
            var controllerContext = new ControllerContext(actionContext);

            var usersController = new UsersController(userBusinessMock.Object) {
                ControllerContext = controllerContext
            };

            var result = usersController.GetById(1) as ObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;

            Assert.IsInstanceOfType(okResult.Value, typeof(UserModel));
            var userModel = okResult.Value as UserModel;

            Assert.AreEqual(userModel.Id, 1);
        }
    }
}
