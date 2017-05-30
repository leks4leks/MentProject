using MentProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;

namespace MentTest
{
    [TestClass]
    public class RotingTesting
    {
        [ClassInitialize]
        public static void Init(TestContext context)
        {
            RouteConfig.RegisterAllRoutes(RouteTable.Routes);
        }

        [TestMethod]
        public void RouteDefaultUrl()
        {
            var context = new Mock<HttpContextBase>();
            context.Setup(item => item.Request.AppRelativeCurrentExecutionFilePath).Returns(@"~\");

            var routeData = RouteTable.Routes.GetRouteData(context.Object);

            Assert.AreEqual("LoginFromCache", routeData.Values["action"]);
            Assert.AreEqual("Account", routeData.Values["controller"]);
        }

        [TestMethod]
        public void RouteUsersUrl()
        {
            var context = new Mock<HttpContextBase>();
            context.Setup(item => item.Request.AppRelativeCurrentExecutionFilePath).Returns(@"~\users");

            var routeData = RouteTable.Routes.GetRouteData(context.Object);

            Assert.AreEqual("Index".ToUpper(), routeData.Values["action"].ToString().ToUpper());
            Assert.AreEqual("User", routeData.Values["controller"]);
        }

        [TestMethod]
        public void RouteUsersAndNameUrl()
        {
            var context = new Mock<HttpContextBase>();
            context.Setup(item => item.Request.AppRelativeCurrentExecutionFilePath).Returns(@"~/users/Name");

            var routeData = RouteTable.Routes.GetRouteData(context.Object);

            Assert.AreEqual("GetUserByName".ToUpper(), routeData.Values["action"].ToString().ToUpper());
            Assert.AreEqual("User", routeData.Values["controller"]);
        }

        [TestMethod]
        public void RouteUsersCreateUser()
        {
            var context = new Mock<HttpContextBase>();
            context.Setup(item => item.Request.AppRelativeCurrentExecutionFilePath).Returns(@"~/create-user");

            var routeData = RouteTable.Routes.GetRouteData(context.Object);

            Assert.AreEqual("Create".ToUpper(), routeData.Values["action"].ToString().ToUpper());
            Assert.AreEqual("User", routeData.Values["controller"]);
        }
    }
}
